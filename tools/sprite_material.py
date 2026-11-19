"""Bake shallow relief on the original sprite layers at their gameplay scale."""
from PIL import Image, ImageChops, ImageDraw, ImageFilter


def inset(mask, pixels):
    # Erode at logical-pixel scale, with a slightly softened inner contour.
    return mask.filter(ImageFilter.BoxBlur(pixels)).point(lambda v: max(0, (v-243)*22))


def hull_details(rgb, face, scale, seed, building):
    safe = inset(face, 4*scale)
    groove = ImageChops.subtract(safe, inset(face, 5*scale))
    rgb = ImageChops.subtract(rgb, groove.point(lambda v: v*0.075).convert("RGB"))
    draw = ImageDraw.Draw(rgb)
    pitch = (41 if building else 53)*scale
    for y in range((13+seed%13)*scale, rgb.height-6*scale, pitch):
        for x in range((9+(seed//13+y//pitch*9)%18)*scale, rgb.width-9*scale, pitch):
            # The complete recess must be on one raised face clear of its seam.
            if safe.crop((x-4*scale,y-3*scale,x+9*scale,y+5*scale)).getextrema()[0] < 250:
                continue
            base = rgb.getpixel((x,y))
            dark = tuple(int(c*0.72) for c in base)
            lip = tuple(min(255,c+8) for c in base)
            if (x//pitch+y//pitch+seed)%3 == 0:
                for i in (0, 3, 6):
                    draw.line((x+i*scale,y-2*scale,x+i*scale,y+3*scale),fill=dark,width=scale)
                    draw.point((x+i*scale,y-3*scale),fill=lip)
            else:
                for dx in (0, 6*scale):
                    draw.ellipse((x+dx-scale,y-scale,x+dx+scale,y+scale),fill=dark)
                    draw.point((x+dx,y),fill=lip)
    return rgb


def ship_details(rgb, alpha, scale, name):
    # Ships face right in the source. Place the canopy and rear spar within
    # the existing hull so every chassis keeps its own cutouts and silhouette.
    left,top,right,bottom = alpha.getbbox()
    w, h = right-left, bottom-top
    center = (top+bottom)/2
    long_canopy = name in {"Rookie","Interceptor","Lightning","Ace"}
    layer = rgb.copy()
    pen = ImageDraw.Draw(layer)
    def hull(points, tone):
        pen.polygon([(left+x*w,center+y*h) for x,y in points], fill=(tone,)*3)
    for sign in (1,-1):
        hull([(0.08,0.38*sign),(.46,.19*sign),(.68,.075*sign),(.43,.15*sign)], 190)
        hull([(.11,.34*sign),(.35,.23*sign),(.13,.29*sign)], 243)
    nose = .78 if long_canopy else .66
    hull([(.30,0),(.42,-.10),(nose,0),(.42,.10)], 151)
    hull([(.37,0),(.44,-.065),(nose-.07,0),(.44,.054)], 209)
    return Image.composite(layer,rgb,inset(alpha,2*scale))


def finish_sprite(sprite, path, scale, ships):
    name = path.stem
    building = path.parts[:2] == ("stage","Static")
    ship = len(path.parts)==1 and name in ships
    if not building and not ship and path.as_posix() != "player/Sidekick.svg":
        if len(path.parts)!=1 or path.suffix!=".svg" or name.startswith("Medal") or name in {
            "BigBonus","Bonus","Powerup","TriangleParticle","EnemyShot","Laser"
        }:
            return sprite

    alpha = sprite.getchannel("A")
    rgb = sprite.convert("RGB")
    # Separate raised painted faces from darker chassis, recesses and outlines.
    face = ImageChops.multiply(alpha, rgb.getchannel("R").point(lambda v: (v-137)*12))
    height = Image.blend(alpha,face,0.57)
    soft = height.filter(ImageFilter.GaussianBlur(1.8*scale))
    crest = ImageChops.subtract(height,soft)
    recess = ImageChops.subtract(soft,height)
    # Leave room on near-white and saturated orange for the bevel highlight.
    rgb = rgb.point(lambda v: v*0.955)
    organic = name in {"Glob","BlackHole","Asteroid","Cracks","FenceBeam","CutterBeam"}
    if ship:
        rgb = ship_details(rgb,alpha,scale,name)
    elif not organic:
        rgb = hull_details(rgb,face,scale,sum(name.encode()),building)
    rgb = ImageChops.subtract(rgb, recess.point(lambda v: v*0.50).convert("RGB"))
    rgb = ImageChops.add(rgb, crest.point(lambda v: v*0.39).convert("RGB"))
    if organic:
        broad = height.filter(ImageFilter.GaussianBlur(5*scale))
        rim = ImageChops.subtract(height,broad)
        rgb = ImageChops.add(rgb,rim.point(lambda v: v*0.12).convert("RGB"))
    rgb.putalpha(alpha)
    return rgb
