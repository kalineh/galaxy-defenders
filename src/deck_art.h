/* Modern deck presentation. The original event positions and simulation stay intact.
   Rasterize the UNION once in stage coordinates, so grain and exposed bevels never
   restart at authored tile joins or swim as the camera scrolls. No frame allocations. */
#define DECK_CHUNK_HEIGHT 512
#define DECK_MAX_CHUNKS 40

enum DeckFamily { DECK_NONE, DECK_NAVAL, DECK_STATION, DECK_SERVICE };
typedef struct {
    Texture2D deck[DECK_MAX_CHUNKS], sea, distant;
    int left, top, width, height, count;
    enum DeckFamily family;
    bool frost;
    bool attempted, ready;
} DeckArt;

static enum DeckFamily deck_family(int stage) {
    switch (stage) {
        case 3: case 6: return DECK_STATION;
        case 7: case 10: return DECK_NAVAL;
        case 9: return DECK_SERVICE;
        default: return DECK_NONE;
    }
}

static bool deck_event(const StageEvent *e) {
    return e->kind == STAGE_DECORATION && (!strcmp(e->name,"DarkBlue1") || !strcmp(e->name,"Block1") ||
        !strcmp(e->name,"DarkBlue3") || !strncmp(e->name,"Grass",5));
}

/* Match the sprite's two-pixel apron, rotated about its center. DarkBlue3
   has one opaque rail at source rows 58..89 in a 147-square sprite. */
static Rectangle deck_bounds(const StageEvent *e) {
    float w=e->width+2, h=w*e->height/e->width;
    float x=e->x, y=e->y;
    if (!strcmp(e->name,"DarkBlue3")) {
        h=32*w/147;
        if (e->rotation==90) x-=0.5f*w/147;
        else y+=0.5f*w/147;
    }
    if (e->rotation==90) { float temp=w; w=h; h=temp; }
    int left=(int)ceilf(x-w/2-0.5f), top=(int)ceilf(y-h/2-0.5f);
    return (Rectangle){left,top,ceilf(x+w/2-0.5f)-left,ceilf(y+h/2-0.5f)-top};
}

static void deck_art_unload(DeckArt *art) {
    for (int i=0; i<art->count; ++i) if (IsTextureValid(art->deck[i])) UnloadTexture(art->deck[i]);
    if (IsTextureValid(art->sea)) UnloadTexture(art->sea);
    if (IsTextureValid(art->distant)) UnloadTexture(art->distant);
    *art = (DeckArt){0};
}

/* Distance to the exposed hull in thirds of a pixel. The mask has an empty
   one-pixel border. Diagonal steps carry the inset trim around reentrant corners. */
static void deck_edge_distances(unsigned char *mask, int w, int h) {
    for (int step=1; step>=-1; step-=2) {
        int first=step>0 ? 1 : h-2, end=step>0 ? h-1 : 0;
        for (int y=first; y!=end; y+=step) {
            for (int x=step>0 ? 1 : w-2; x!=(step>0 ? w-1 : 0); x+=step) {
                unsigned char *p=mask+y*w+x;
                if (!*p) continue;
                const unsigned char *row=p-step*w;
                int a=p[-step]+3, b=row[0]+3, c=row[-1]+4, d=row[1]+4;
                int straight=a<b ? a : b, diagonal=c<d ? c : d;
                int nearest=straight<diagonal ? straight : diagonal;
                if (nearest<*p) *p=(unsigned char)nearest;
            }
        }
    }
}

static unsigned deck_cell_hash(int x, int y) {
    unsigned n=(unsigned)x*0x9e3779b1u ^ (unsigned)y*0x85ebca77u;
    n ^= n>>16; n *= 0x7feb352du;
    return n^(n>>15);
}

/* Atlas column and half extents of each fitting, including a little bare metal. */
typedef struct { int column, half_w, half_h; } DeckFitting;
static const DeckFitting deck_fittings[] = {
    {0,24,29}, {1,29,27}, {2,29,15}, {3,28,8},
    {6,20,10}, {7,21,20}, {8,25,12}
};

static bool deck_clear_socket(const Image *sheet, int x, int y, int hw, int hh) {
    const Color *p=(const Color *)sheet->data+y*sheet->width+x;
    for (int dy=-hh; dy<=hh; ++dy)
        for (int dx=-hw; dx<=hw; ++dx)
            if (p[dy*sheet->width+dx].r!=p->r) return false;
    return true;
}

/* Visit one quadrant of a world-aligned 256px plate. The four atlas variants
   share their edge sockets. A cell can also seat one whole hardware fitting. */
static void deck_paint_cell(Color *out, int start, int rows, const DeckArt *art,
        const unsigned char *distance, const Image *sheet, const Image *grain, int ox, int oy) {
    int w=art->width, wx=art->left+ox, wy=art->top+oy;
    int tile_x=(wx+32768)/256, tile_y=(wy+32768)/256;
    unsigned tile=deck_cell_hash(tile_x,tile_y)%4;
    const Color *plates=sheet->data, *brush=grain->data;
    int bank=(tile_x^tile_y)&1;
    int u0=bank*512+(tile%2)*256+(wx&255), v0=(tile/2)*256+(wy&255);
    if (art->family!=DECK_NAVAL) {
        u0=(art->family==DECK_SERVICE ? 512 : 0)+(tile&1)*256+(wx&255);
        v0=576+(wy&255);
    }
    unsigned socket=deck_cell_hash(wx/128,wy/128);
    /* More fasteners than hazard paint, with the occasional wide access cover. */
    static const int picks[]={0,1,2,3,4,4,5,6,6};
    DeckFitting fitting=deck_fittings[picks[(socket>>8)%9]];
    int turn=(socket>>18)&3;
    int hw=(turn&1) ? fitting.half_h : fitting.half_w;
    int hh=(turn&1) ? fitting.half_w : fitting.half_h;
    int cx=0, cy=0;
    bool placed=false;
    if (socket%4!=0) {
        /* The distance field uses 3 for an axial step and 4 for a diagonal.
           Leave sixteen pixels to the rim, whatever the fitting's orientation. */
        int clearance=48+3*(hw>hh ? hw : hh)+(hw<hh ? hw : hh);
        for (int attempt=0; attempt<12; ++attempt) {
            unsigned offset=deck_cell_hash((int)(socket>>1),attempt);
            int dx=32+(offset&63), dy=32+((offset>>8)&63);
            int x=ox+dx, y=oy+dy;
            if (x<0 || x>=w || y<0 || y>=art->height) continue;
            if (distance[y*w+x]>=clearance && deck_clear_socket(sheet,u0+dx,v0+dy,hw,hh)) {
                cx=x; cy=y; placed=true;
                break;
            }
        }
    }
    int bottom=oy+128<start+rows ? oy+128 : start+rows;
    int right=ox+128<w ? ox+128 : w;
    for (int y=oy>start ? oy : start; y<bottom; ++y) {
        int py=y-oy, gy=y+art->top;
        int v=(gy%grain->height+grain->height)%grain->height;
        for (int x=ox>0 ? ox : 0; x<right; ++x) {
            int idx=y*w+x, depth=distance[idx];
            if (!depth) continue;
            int px=x-ox, gx=x+art->left;
            Color c=plates[(v0+py)*sheet->width+u0+px];
            if (placed && abs(x-cx)<=hw && abs(y-cy)<=hh) {
                int u=x-cx+32, v=y-cy+32;
                if (turn&1) { int tmp=u; u=v; v=63-tmp; }
                if (turn&2) { u=63-u; v=63-v; }
                Color f=plates[(512+v)*sheet->width+fitting.column*64+u];
                c=ColorAlphaBlend(c,f,WHITE);
            }
            if (depth<=32) {
                int dx=distance[idx+1]-distance[idx-1], dy=distance[idx+w]-distance[idx-w];
                int light=(dx+dy)/2;
                int tone=depth<=3 ? 38 : depth<=8 ? 77 : depth<=18 ? 72 : depth<=23 ? 67 : depth<=26 ? 84 : 41;
                /* Expansion joints cross the rail only on a straight run. */
                if (((dx==0 && (gx&127)<2) || (dy==0 && (gy&127)<2)) && depth>4 && depth<25) tone=38;
                c=(Color){tone+light,tone+light+2,tone+light+11,255};
            }
            int u=((gx+gy/2)%grain->width+grain->width)%grain->width;
            int variation=((int)brush[v*grain->width+u].r-128)/8;
            /* Broad, stationary reflected light catches the raised paint most.
               The station has a dry finish. The rain deck has a wider gloss. */
            int field_x=((gx+gy/2)&511)+((gy&256) ? 512 : 0);
            Color field=plates[(832+(gy&255))*sheet->width+field_x];
            int sheen=art->family==DECK_SERVICE ? 20 : art->family==DECK_NAVAL && !art->frost ? 6 : 1;
            variation+=sheen*field.g*(c.r+20)/(255*85);
            if (depth>32 && depth<51) variation-=(51-depth)/3;
            c=(Color){c.r+variation,c.g+variation,c.b+variation,255};
            if (art->frost) {
                /* The same world-space deposit crosses plate and strip boundaries.
                   Grooves and the inner foot of the rail collect a little more. */
                int cover=(int)field.r-127;
                if ((depth>25 && depth<52) || c.r<46) cover+=15;
                if (cover>0) {
                    int mix=cover*4;
                    if (mix>194) mix=194;
                    c=ColorAlphaBlend(c,(Color){169,180,191,mix},WHITE);
                }
            }
            out[(y-start)*w+x]=c;
        }
    }
}

static bool deck_on_deck(const unsigned char *mask, int w, int h, int x, int y) {
    return x>=0 && x<w && y>=0 && y<h && mask[y*w+x];
}

/* Place premultiplied under-deck art in empty pixels only. Local u runs
   along the edge, local v points out into open space. */
static void deck_underpart(Color *out, int start, int rows, int w, const unsigned char *mask,
        const Image *sheet, int col, int x, int y, int ux, int uy, int vx, int vy) {
    const Color *pixels=sheet->data;
    for (int v=-30; v<30; ++v) for (int u=-30; u<30; ++u) {
        int px=x+ux*u+vx*v, py=y+uy*u+vy*v;
        if (px<0 || px>=w || py<start || py>=start+rows || mask[py*w+px]) continue;
        Color c=pixels[(544+v)*sheet->width+col*64+32+u];
        if (!c.a) continue;
        out[(py-start)*w+px]=(Color){c.r*c.a/255,c.g*c.a/255,c.b*c.a/255,c.a};
    }
}

static void deck_edge_parts(Color *out, int start, int rows, const DeckArt *art,
        const unsigned char *mask, const Image *sheet) {
    int w=art->width,h=art->height;
    for (int axis=0; axis<2; ++axis) {
        int tx=axis==0, ty=axis==1;
        int top=start>30 ? start-30 : 0;
        int y0=top+(ty ? ((61-art->top-top)&127) : 0);
        int x0=tx ? ((39-art->left)&127) : 0;
        for (int y=y0; y<h && y<start+rows+30; y+=ty ? 128 : 1)
            for (int x=x0; x<w; x+=tx ? 128 : 1) {
                if (mask[y*w+x]!=3) continue;
                unsigned hash=deck_cell_hash(art->left+x,art->top+y);
                if (hash%4!=0) continue;
                int side=deck_on_deck(mask,w,h,x+ty,y+tx) ? -1 : 1;
                int nx=ty*side, ny=tx*side;
                bool clear=true;
                for (int along=-28; along<=28 && clear; along+=4) {
                    int ax=x+along*tx, ay=y+along*ty;
                    clear=deck_on_deck(mask,w,h,ax,ay) && !deck_on_deck(mask,w,h,ax+nx,ay+ny)
                        && !deck_on_deck(mask,w,h,ax+22*nx,ay+22*ny);
                }
                if (clear) deck_underpart(out,start,rows,w,mask,sheet,9+((hash>>8)&1),x,y,tx,ty,nx,ny);
            }
    }
}

/* One of the four quadrants differs at a true corner. Mirror the matching
   fitting onto that vertex, including its overhang into the next render strip. */
static void deck_corner_caps(Color *out, int start, int rows, const DeckArt *art,
        const unsigned char *mask, const Image *sheet) {
    int w=art->width,h=art->height;
    const Color *pixels=sheet->data;
    int top=start>32 ? start-32 : 1;
    for (int y=top; y<h && y<start+rows+32; ++y) for (int x=1; x<w; ++x) {
        int i=y*w+x;
        if (!!mask[i]==!!mask[i-1] && !!mask[i-w]==!!mask[i-w-1]) continue;
        bool ne=mask[i-w]!=0, nw=mask[i-w-1]!=0, se=mask[i]!=0, sw=mask[i-1]!=0;
        int count=ne+nw+se+sw;
        if (count!=1 && count!=3) continue;
        bool convex=count==1;
        int sx=(ne==convex || se==convex) ? 1 : -1;
        int sy=(ne==convex || nw==convex) ? 1 : -1;
        /* Tiny steps between nearly aligned platforms are too short for a cap. */
        if (deck_on_deck(mask,w,h,x+24,y-25)!=ne || deck_on_deck(mask,w,h,x-25,y-25)!=nw ||
            deck_on_deck(mask,w,h,x+24,y+24)!=se || deck_on_deck(mask,w,h,x-25,y+24)!=sw) continue;
        if (!convex && deck_cell_hash(x+art->left,y+art->top)%5==0)
            deck_underpart(out,start,rows,w,mask,sheet,11,x,y,sx,0,0,sy);
        for (int v=-25; v<=25; ++v) {
            int dy=y+(sy>0 ? v : -v-1);
            if (dy<start || dy>=start+rows) continue;
            for (int u=-25; u<=25; ++u) {
                int dx=x+(sx>0 ? u : -u-1);
                if (dx<0 || dx>=w) continue;
                Color cap=pixels[(544+v)*sheet->width+(convex ? 352 : 288)+u];
                Color *dest=out+(dy-start)*w+dx;
                if (cap.a && dest->a) *dest=ColorAlphaBlend(*dest,cap,WHITE);
            }
        }
    }
}

static bool deck_art_load(DeckArt *art, int index, const char *grain_path, const char *sea_path, const char *sheet_path) {
    art->attempted = true;
    art->family=deck_family(index);
    art->frost=index==6 || index==10;
    Image grain = LoadImage(grain_path);
    if (!IsImageValid(grain)) return false;
    Image sheet=LoadImage(sheet_path);
    if (!IsImageValid(sheet) || sheet.width!=1024 || sheet.height!=1088) {
        TraceLog(LOG_ERROR,"Invalid deck plating sheet");
        UnloadImage(sheet); UnloadImage(grain); return false;
    }
    ImageFormat(&grain,PIXELFORMAT_UNCOMPRESSED_R8G8B8A8);
    ImageFormat(&sheet,PIXELFORMAT_UNCOMPRESSED_R8G8B8A8);
    const StageDefinition *stage = &stage_definitions[index];
    int left=0, right=0, top=0, bottom=0;
    for (int i=0; i<stage->event_count; ++i) {
        const StageEvent *e = &stage->events[i];
        if (!deck_event(e)) continue;
        Rectangle r=deck_bounds(e);
        int x0=r.x, x1=r.x+r.width, y0=r.y, y1=r.y+r.height;
        if (x0<left) left=x0;
        if (x1>right) right=x1;
        if (y0<top) top=y0;
        if (y1>bottom) bottom=y1;
    }
    left-=32; top-=32; right+=32; bottom+=32;
    int w=right-left, h=bottom-top;
    if (w<=2 || h<=2 || (h+DECK_CHUNK_HEIGHT-1)/DECK_CHUNK_HEIGHT>DECK_MAX_CHUNKS) {
        UnloadImage(sheet); UnloadImage(grain); return false;
    }
    unsigned char *mask = calloc((size_t)w*h,1);
    if (!mask) { UnloadImage(sheet); UnloadImage(grain); return false; }
    for (int i=0; i<stage->event_count; ++i) {
        const StageEvent *e=&stage->events[i];
        if (!deck_event(e)) continue;
        Rectangle r=deck_bounds(e);
        int x0=r.x-left, x1=x0+r.width, y0=r.y-top, y1=y0+r.height;
        for (int y=y0; y<y1; ++y) memset(mask+y*w+x0,255,(size_t)(x1-x0));
    }
    art->left=left; art->top=top; art->width=w; art->height=h;
    deck_edge_distances(mask,w,h);
    bool valid=true;
    for (int start=0; start<h; start+=DECK_CHUNK_HEIGHT) {
        int rows=h-start<DECK_CHUNK_HEIGHT ? h-start : DECK_CHUNK_HEIGHT;
        /* Filtering across a strip boundary needs the actual adjacent deck rows. */
        int first=start-(start>0);
        int end=start+rows+(start+rows<h);
        Image image=GenImageColor(w,end-first,BLANK);
        if (!image.data) { valid=false; break; }
        int first_x=-(int)((unsigned)left&127u), first_y=first-(int)((unsigned)(first+top)&127u);
        for (int oy=first_y; oy<end; oy+=128)
            for (int ox=first_x; ox<w; ox+=128)
                deck_paint_cell(image.data,first,end-first,art,mask,&sheet,&grain,ox,oy);
        deck_edge_parts(image.data,first,end-first,art,mask,&sheet);
        deck_corner_caps(image.data,first,end-first,art,mask,&sheet);
        Texture2D texture=LoadTextureFromImage(image);
        UnloadImage(image);
        art->deck[art->count++]=texture;
        if (!IsTextureValid(texture)) { valid=false; break; }
        /* Edge samples must not wrap to the opposite end of this 512-row strip. */
        SetTextureWrap(texture,TEXTURE_WRAP_CLAMP);
        SetTextureFilter(texture,TEXTURE_FILTER_BILINEAR);
    }
    free(mask); UnloadImage(sheet); UnloadImage(grain);
    if (index==7) {
        art->sea=LoadTexture(sea_path);
        if (IsTextureValid(art->sea)) SetTextureFilter(art->sea,TEXTURE_FILTER_BILINEAR);
        /* Blur the union once at 1/16 scale, so overlapping rectangles become
           one hazy shape. The same small mask serves both parallax distances. */
        Image haze=GenImageColor((w+15)/16+8,(h+15)/16+8,BLANK);
        if (haze.data) {
            for (int i=0; i<stage->event_count; ++i) {
                const StageEvent *e=&stage->events[i];
                if (!deck_event(e)) continue;
                int size=(e->width+17)/16;
                ImageDrawRectangle(&haze,4+(int)((e->x-e->width/2-left)/16),
                    4+(int)((e->y-e->width/2-top)/16),size,size,WHITE);
            }
            ImageBlurGaussian(&haze,2);
            art->distant=LoadTextureFromImage(haze);
            UnloadImage(haze);
            if (IsTextureValid(art->distant)) {
                SetTextureWrap(art->distant,TEXTURE_WRAP_CLAMP);
                SetTextureFilter(art->distant,TEXTURE_FILTER_BILINEAR);
            }
        }
    }
    art->ready=valid;
    if (!valid) TraceLog(LOG_ERROR,"Stage %d modern deck unavailable; using Classic art",index+1);
    return valid;
}

static void stage8_draw_distant(const DeckArt *art, float cam_x, float cam_y, float scale,
        float parallax, int offset, Color tint) {
    Texture2D tex=art->distant;
    if (!IsTextureValid(tex)) return;
    float period=19000*scale, height=tex.height*16*scale;
    float x=ARENA_X+GAME_WIDTH*0.5f+offset+(art->left-64)*scale-cam_x*parallax;
    float y=fmodf((art->top-64)*scale-cam_y*parallax,period);
    if (y>0) y-=period;
    for (; y<GAME_HEIGHT; y+=period) {
        if (y+height<=0) continue;
        DrawTexturePro(tex,(Rectangle){0,0,tex.width,tex.height},
            (Rectangle){x,y,tex.width*16*scale,height},(Vector2){0},0,tint);
    }
}

static void stage8_draw_background(const DeckArt *art, float cam_x, float cam_y) {
    DrawRectangle(ARENA_X,0,GAME_WIDTH,GAME_HEIGHT,(Color){17,32,43,255});
    if (IsTextureValid(art->sea)) {
        float size=1024, y=fmodf(-cam_y*0.035f,size)-size;
        for (; y<GAME_HEIGHT; y+=size)
            DrawTexturePro(art->sea,(Rectangle){0,0,art->sea.width,art->sea.height},
                (Rectangle){ARENA_X,y,size,size},(Vector2){0},0,(Color){135,155,165,255});
        DrawRectangle(ARENA_X,0,GAME_WIDTH,GAME_HEIGHT,(Color){17,32,43,95});
    }
    Color far={93,111,119,14}, middle={95,115,121,26};
    stage8_draw_distant(art,cam_x,cam_y,0.048f,0.030f,-265,far);
    stage8_draw_distant(art,cam_x,cam_y,0.048f,0.030f,285,far);
    stage8_draw_distant(art,cam_x,cam_y,0.16f,0.085f,80,middle);
}

static void deck_draw(const DeckArt *art, float cam_x, float cam_y) {
    float x=ARENA_X+GAME_WIDTH*0.5f+art->left-cam_x;
    /* Opaque paint on transparent black is already premultiplied. Preserve that
       coverage at the silhouette when a camera step is smaller than a texel. */
    BeginBlendMode(BLEND_ALPHA_PREMULTIPLY);
    for (int i=0; i<art->count; ++i) {
        float y=GAME_HEIGHT*0.5f+art->top+i*DECK_CHUNK_HEIGHT-cam_y;
        Texture2D texture=art->deck[i];
        int rows=texture.height-(i>0)-(i+1<art->count);
        if (y+rows<0 || y>GAME_HEIGHT) continue;
        DrawTexturePro(texture,(Rectangle){0,i>0,texture.width,rows},
            (Rectangle){x,y,texture.width,rows},(Vector2){0},0,WHITE);
    }
    EndBlendMode();
}
