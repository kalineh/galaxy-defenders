/* Stage 8 presentation only. The original event positions and simulation stay intact.
   Rasterize the UNION once in stage coordinates, so grain and exposed bevels never
   restart at authored tile joins or swim as the camera scrolls. No frame allocations. */
#define DECK_CHUNK_HEIGHT 512
#define DECK_MAX_CHUNKS 40

typedef struct {
    Texture2D deck[DECK_MAX_CHUNKS], sea;
    int left, top, width, height, count;
    bool attempted, ready;
} Stage8Art;

static bool stage8_deck_event(const StageEvent *e) {
    return e->kind == STAGE_DECORATION && !strcmp(e->name,"DarkBlue1");
}

static void stage8_art_unload(Stage8Art *art) {
    for (int i=0; i<art->count; ++i) if (IsTextureValid(art->deck[i])) UnloadTexture(art->deck[i]);
    if (IsTextureValid(art->sea)) UnloadTexture(art->sea);
    *art = (Stage8Art){0};
}

/* Distance to the exposed hull in thirds of a pixel. The mask has an empty
   one-pixel border. Diagonal steps carry the inset trim around reentrant corners. */
static void stage8_edge_distances(unsigned char *mask, int w, int h) {
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

static unsigned stage8_cell_hash(int x, int y) {
    unsigned n=(unsigned)x*0x9e3779b1u ^ (unsigned)y*0x85ebca77u;
    n ^= n>>16; n *= 0x7feb352du;
    return n^(n>>15);
}

/* Atlas column and half extents of each fitting, including a little bare metal. */
typedef struct { int column, half_w, half_h; } Stage8Fitting;
static const Stage8Fitting stage8_fittings[] = {
    {0,24,29}, {1,29,27}, {2,29,15}, {3,28,8},
    {6,20,10}, {7,21,20}, {8,25,12}
};

static bool stage8_clear_socket(const Image *sheet, int x, int y, int hw, int hh) {
    const Color *p=(const Color *)sheet->data+y*sheet->width+x;
    for (int dy=-hh; dy<=hh; ++dy)
        for (int dx=-hw; dx<=hw; ++dx)
            if (p[dy*sheet->width+dx].r!=p->r) return false;
    return true;
}

/* Visit one quadrant of a world-aligned 256px plate. The four atlas variants
   share their edge sockets. A cell can also seat one whole hardware fitting. */
static void stage8_paint_cell(Color *out, int start, int rows, const Stage8Art *art,
        const unsigned char *distance, const Image *sheet, const Image *grain, int ox, int oy) {
    int w=art->width, wx=art->left+ox, wy=art->top+oy;
    int tile_x=(wx+32768)/256, tile_y=(wy+32768)/256;
    unsigned tile=stage8_cell_hash(tile_x,tile_y)%4;
    const Color *plates=sheet->data, *brush=grain->data;
    int bank=(tile_x^tile_y)&1;
    int u0=bank*512+(tile%2)*256+(wx&255), v0=(tile/2)*256+(wy&255);
    unsigned socket=stage8_cell_hash(wx/128,wy/128);
    /* More fasteners than hazard paint, with the occasional wide access cover. */
    static const int picks[]={0,1,2,3,4,4,5,6,6};
    Stage8Fitting fitting=stage8_fittings[picks[(socket>>8)%9]];
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
            unsigned offset=stage8_cell_hash((int)(socket>>1),attempt);
            int dx=32+(offset&63), dy=32+((offset>>8)&63);
            int x=ox+dx, y=oy+dy;
            if (x<0 || x>=w || y<0 || y>=art->height) continue;
            if (distance[y*w+x]>=clearance && stage8_clear_socket(sheet,u0+dx,v0+dy,hw,hh)) {
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
                int tone=depth<=4 ? 36 : depth<=8 ? 78 : depth<=22 ? 65 : depth<=25 ? 85 : 34;
                /* Expansion joints cross the rail only on a straight run. */
                if (((dx==0 && (gx&127)<2) || (dy==0 && (gy&127)<2)) && depth>4 && depth<25) tone=38;
                c=(Color){tone+light,tone+light+2,tone+light+11,255};
            }
            int u=((gx+gy/2)%grain->width+grain->width)%grain->width;
            int variation=((int)brush[v*grain->width+u].r-128)/8;
            out[(y-start)*w+x]=(Color){c.r+variation,c.g+variation,c.b+variation,255};
        }
    }
}

static bool stage8_on_deck(const unsigned char *mask, int w, int h, int x, int y) {
    return x>=0 && x<w && y>=0 && y<h && mask[y*w+x];
}

/* One of the four quadrants differs at a true corner. Mirror the matching
   fitting onto that vertex, including its overhang into the next render strip. */
static void stage8_corner_caps(Color *out, int start, int rows, int w, int h,
        const unsigned char *mask, const Image *sheet) {
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
        if (stage8_on_deck(mask,w,h,x+24,y-25)!=ne || stage8_on_deck(mask,w,h,x-25,y-25)!=nw ||
            stage8_on_deck(mask,w,h,x+24,y+24)!=se || stage8_on_deck(mask,w,h,x-25,y+24)!=sw) continue;
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

static bool stage8_art_load(Stage8Art *art, const char *grain_path, const char *sea_path, const char *sheet_path) {
    art->attempted = true;
    Image grain = LoadImage(grain_path);
    if (!IsImageValid(grain)) return false;
    Image sheet=LoadImage(sheet_path);
    if (!IsImageValid(sheet) || sheet.width!=1024 || sheet.height!=576) {
        TraceLog(LOG_ERROR,"Invalid Stage 8 plating sheet");
        UnloadImage(sheet); UnloadImage(grain); return false;
    }
    ImageFormat(&grain,PIXELFORMAT_UNCOMPRESSED_R8G8B8A8);
    ImageFormat(&sheet,PIXELFORMAT_UNCOMPRESSED_R8G8B8A8);
    const StageDefinition *stage = &stage_definitions[7];
    int left=0, right=0, top=0, bottom=0;
    for (int i=0; i<stage->event_count; ++i) {
        const StageEvent *e = &stage->events[i];
        if (!stage8_deck_event(e)) continue;
        /* Match draw_stage_prop's two-pixel apron exactly, with pixel-center coverage. */
        float half=(e->width+2)*0.5f;
        int x0=(int)ceilf(e->x-half-0.5f), x1=(int)ceilf(e->x+half-0.5f);
        int y0=(int)ceilf(e->y-half-0.5f), y1=(int)ceilf(e->y+half-0.5f);
        if (x0<left) left=x0;
        if (x1>right) right=x1;
        if (y0<top) top=y0;
        if (y1>bottom) bottom=y1;
    }
    --left; --top; ++right; ++bottom;
    int w=right-left, h=bottom-top;
    if (w<=2 || h<=2 || (h+DECK_CHUNK_HEIGHT-1)/DECK_CHUNK_HEIGHT>DECK_MAX_CHUNKS) {
        UnloadImage(sheet); UnloadImage(grain); return false;
    }
    unsigned char *mask = calloc((size_t)w*h,1);
    if (!mask) { UnloadImage(sheet); UnloadImage(grain); return false; }
    for (int i=0; i<stage->event_count; ++i) {
        const StageEvent *e=&stage->events[i];
        if (!stage8_deck_event(e)) continue;
        float half=(e->width+2)*0.5f;
        int x0=(int)ceilf(e->x-half-0.5f)-left, x1=(int)ceilf(e->x+half-0.5f)-left;
        int y0=(int)ceilf(e->y-half-0.5f)-top, y1=(int)ceilf(e->y+half-0.5f)-top;
        for (int y=y0; y<y1; ++y) memset(mask+y*w+x0,255,(size_t)(x1-x0));
    }
    art->left=left; art->top=top; art->width=w; art->height=h;
    stage8_edge_distances(mask,w,h);
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
                stage8_paint_cell(image.data,first,end-first,art,mask,&sheet,&grain,ox,oy);
        stage8_corner_caps(image.data,first,end-first,w,h,mask,&sheet);
        Texture2D texture=LoadTextureFromImage(image);
        UnloadImage(image);
        art->deck[art->count++]=texture;
        if (!IsTextureValid(texture)) { valid=false; break; }
        /* Edge samples must not wrap to the opposite end of this 512-row strip. */
        SetTextureWrap(texture,TEXTURE_WRAP_CLAMP);
        SetTextureFilter(texture,TEXTURE_FILTER_BILINEAR);
    }
    free(mask); UnloadImage(sheet); UnloadImage(grain);
    art->sea=LoadTexture(sea_path);
    if (IsTextureValid(art->sea)) SetTextureFilter(art->sea,TEXTURE_FILTER_BILINEAR);
    art->ready=valid;
    if (!valid) TraceLog(LOG_ERROR,"Stage 8 modern deck unavailable; using Classic art");
    return valid;
}

static void stage8_draw_distant(float cam_x, float cam_y, float scale, float parallax, int offset, Color color) {
    const StageDefinition *stage=&stage_definitions[7];
    /* Reuse the authored groups as deliberately unadorned, hazed silhouettes.
       Wrap whole stage strips, not individual platforms, so groups stay coherent. */
    float period=19000*scale;
    float scroll=fmodf(-cam_y*parallax,period);
    for (int repeat=-1; repeat<4; ++repeat) {
        float base=scroll+repeat*period;
        if (base>GAME_HEIGHT+period) continue;
        for (int i=0; i<stage->event_count; ++i) {
            const StageEvent *e=&stage->events[i];
            if (!stage8_deck_event(e)) continue;
            float size=(e->width+2)*scale;
            float x=ARENA_X+GAME_WIDTH*0.5f+offset+e->x*scale-cam_x*parallax;
            float y=base+e->y*scale;
            if (y+size<0 || y-size>GAME_HEIGHT) continue;
            DrawRectangleRec((Rectangle){x-size/2,y-size/2,size,size},color);
        }
    }
}

static void stage8_draw_background(const Stage8Art *art, float cam_x, float cam_y) {
    DrawRectangle(ARENA_X,0,GAME_WIDTH,GAME_HEIGHT,(Color){17,32,43,255});
    if (IsTextureValid(art->sea)) {
        float size=1024, y=fmodf(-cam_y*0.035f,size)-size;
        for (; y<GAME_HEIGHT; y+=size)
            DrawTexturePro(art->sea,(Rectangle){0,0,art->sea.width,art->sea.height},
                (Rectangle){ARENA_X,y,size,size},(Vector2){0},0,(Color){135,155,165,255});
        DrawRectangle(ARENA_X,0,GAME_WIDTH,GAME_HEIGHT,(Color){17,32,43,95});
    }
    stage8_draw_distant(cam_x,cam_y,0.05f,0.045f,-265,(Color){29,46,55,255});
    stage8_draw_distant(cam_x,cam_y,0.05f,0.045f,285,(Color){29,46,55,255});
    stage8_draw_distant(cam_x,cam_y,0.28f,0.18f,80,(Color){35,54,63,255});
}

static void stage8_draw_deck(const Stage8Art *art, float cam_x, float cam_y) {
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
