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

static bool stage8_occupied(const unsigned char *mask, int w, int h, int x, int y) {
    return x >= 0 && x < w && y >= 0 && y < h && mask[y*w+x];
}

static bool stage8_art_load(Stage8Art *art, const char *grain_path, const char *sea_path) {
    art->attempted = true;
    Image grain = LoadImage(grain_path);
    if (!IsImageValid(grain)) return false;
    Color *grain_pixels = LoadImageColors(grain);
    if (!grain_pixels) { UnloadImage(grain); return false; }
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
    int w=right-left, h=bottom-top;
    if (w<=0 || h<=0 || (h+DECK_CHUNK_HEIGHT-1)/DECK_CHUNK_HEIGHT>DECK_MAX_CHUNKS) {
        UnloadImageColors(grain_pixels); UnloadImage(grain); return false;
    }
    unsigned char *mask = calloc((size_t)w*h,1);
    if (!mask) { UnloadImageColors(grain_pixels); UnloadImage(grain); return false; }
    for (int i=0; i<stage->event_count; ++i) {
        const StageEvent *e=&stage->events[i];
        if (!stage8_deck_event(e)) continue;
        float half=(e->width+2)*0.5f;
        int x0=(int)ceilf(e->x-half-0.5f)-left, x1=(int)ceilf(e->x+half-0.5f)-left;
        int y0=(int)ceilf(e->y-half-0.5f)-top, y1=(int)ceilf(e->y+half-0.5f)-top;
        for (int y=y0; y<y1; ++y) memset(mask+y*w+x0,1,(size_t)(x1-x0));
    }
    art->left=left; art->top=top; art->width=w; art->height=h;
    bool valid=true;
    for (int start=0; start<h; start+=DECK_CHUNK_HEIGHT) {
        int rows=h-start<DECK_CHUNK_HEIGHT ? h-start : DECK_CHUNK_HEIGHT;
        Image image=GenImageColor(w,rows,BLANK);
        if (!image.data) { valid=false; break; }
        Color *pixels=image.data;
        for (int y=0; y<rows; ++y) for (int x=0; x<w; ++x) {
            int gy=start+y;
            if (!mask[gy*w+x]) continue;
            int u=((x+left)%grain.width+grain.width)%grain.width;
            int v=((gy+top)%grain.height+grain.height)%grain.height;
            Color g=grain_pixels[v*grain.width+u];
            float variation=((g.r+g.g+g.b)/3.0f-128)*0.12f;
            float shade=0;
            /* Only exposed UNION edges receive bevels, including concave corners. */
            if (!stage8_occupied(mask,w,h,x,gy-1) || !stage8_occupied(mask,w,h,x-1,gy)) shade=33;
            else if (!stage8_occupied(mask,w,h,x,gy+1) || !stage8_occupied(mask,w,h,x+1,gy)) shade=-22;
            else if (!stage8_occupied(mask,w,h,x,gy-4) || !stage8_occupied(mask,w,h,x-4,gy)) shade=17;
            else if (!stage8_occupied(mask,w,h,x,gy+4) || !stage8_occupied(mask,w,h,x+4,gy)) shade=-13;
            pixels[y*w+x]=(Color){(unsigned char)(52+variation+shade),(unsigned char)(66+variation+shade),(unsigned char)(73+variation+shade),255};
        }
        Texture2D texture=LoadTextureFromImage(image);
        UnloadImage(image);
        art->deck[art->count++]=texture;
        if (!IsTextureValid(texture)) { valid=false; break; }
        /* Point sampling prevents bleed at chunk borders; native MSAA handles geometry. */
        SetTextureFilter(texture,TEXTURE_FILTER_POINT);
    }
    free(mask); UnloadImageColors(grain_pixels); UnloadImage(grain);
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
    for (int i=0; i<art->count; ++i) {
        float y=GAME_HEIGHT*0.5f+art->top+i*DECK_CHUNK_HEIGHT-cam_y;
        Texture2D texture=art->deck[i];
        if (y+texture.height<0 || y>GAME_HEIGHT) continue;
        DrawTexturePro(texture,(Rectangle){0,0,texture.width,texture.height},
            (Rectangle){x,y,texture.width,texture.height},(Vector2){0},0,WHITE);
    }
}
