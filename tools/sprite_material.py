"""Bake gameplay materials in the coordinate system of the original sprites."""
from functools import cache
from io import BytesIO
from pathlib import Path

from PIL import Image, ImageChops, ImageDraw, ImageFilter, ImageOps
from resvg_py import svg_to_bytes


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


@cache
def cast_library():
    return (Path(__file__).resolve().parents[1]/"art/materials/cast-iron.svg").read_text(encoding="utf-8")


def drawn_casting(name, alpha):
    # The drawing supplies material only. The existing alpha owns the silhouette,
    # pivot and animation frame boundaries in both resolutions of the atlas.
    box = alpha.point(lambda a: 255 if a >= 128 else 0).getbbox()
    w, h = box[2]-box[0], box[3]-box[1]
    svg = cast_library().replace("</svg>", f'<use xlink:href="#{name}"/></svg>')
    samples = 4*max(w,h)
    data = svg_to_bytes(svg_string=svg, width=samples, height=samples)
    paint = Image.open(BytesIO(data)).convert("RGB").resize((w,h),Image.Resampling.LANCZOS)
    rgb = Image.new("RGB",alpha.size,(58,55,55))
    rgb.paste(paint,box)
    return rgb


def shifted(mask, dx, dy):
    result = Image.new("L",mask.size)
    result.paste(mask,(dx,dy))
    return result


def cast_plate(base, mask, paint, thickness):
    # A broad face, a short vertical step and the contact shadow at its foot.
    shade = shifted(mask,0,thickness+1).filter(ImageFilter.GaussianBlur(thickness*0.28))
    base.paste((37,36,38),mask=shade.point(lambda v: v*0.75))
    base.paste(paint.point(lambda v: v*0.69),mask=shifted(mask,0,thickness))
    base.paste(paint,mask=mask)
    top = ImageChops.subtract(mask,shifted(mask,0,max(1,thickness//2)))
    return ImageChops.add(base,top.point(lambda v: v*0.065).convert("RGB"))


def cast_material(sprite, path, scale, building):
    alpha = sprite.getchannel("A")
    name = path.stem
    casting = name.removesuffix("Dead")
    wreck = casting != name or "Destroyed" in path.parts
    red = sprite.getchannel("R")
    if casting in {
        "Ball", "BigBall", "ShootBall", "Airship", "MissilePod",
        "MissileTriple", "MissileStorm", "Building2", "SmallBuilding1",
    }:
        body = drawn_casting(casting,alpha)
        if wreck:
            # The original wreck marks exposed metal in dark gray. Carry those
            # chips through the replacement paint and darken the surviving faces.
            body = body.point(lambda v: v*0.77)
            body.paste((54,51,53),mask=red.point(lambda r: (130-r)*8))
        return body
    rgb = sprite.convert("RGB")
    # The source's graphite joints, armor and pale roof pieces already describe
    # separate parts. Cast those shapes at different heights at the original size.
    iron = ImageOps.colorize(rgb.convert("L"),"#222428","#646265")
    face = ImageChops.multiply(alpha,red.point(lambda r: (r-124)*7))
    if building:
        body = ImageOps.colorize(red,"#504f52","#a19e9e")
        base = cast_plate(iron,face,body,2*scale)
        roof = ImageChops.multiply(alpha,red.point(lambda r: (r-185)*9))
        paint = ImageOps.colorize(red,"#77767a","#c3c0bf")
        step = 2 if "Destroyed" in path.parts else 4
        return cast_plate(base,roof,paint,step*scale)
    # Retain the original magenta of the bonus ship and warm orange enemy paint.
    paint = rgb.point([int(v*0.94) for v in range(256)] +
                      [max(0,int(v*0.89)-10) for v in range(256)] +
                      [max(0,int(v*0.93)-10) for v in range(256)])
    wide = inset(face,3*scale)
    side = ImageChops.subtract(face,wide)
    paint = ImageChops.subtract(paint,side.point(lambda v: v*0.045).convert("RGB"))
    return cast_plate(iron,face,paint,(2 if name.endswith("Dead") else 3)*scale)


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
    organic = name in {"Glob","BlackHole","Asteroid","Cracks","FenceBeam","CutterBeam"}
    if not organic and not ship and path.as_posix() != "player/Sidekick.svg":
        cast = cast_material(sprite,path,scale,building)
        cast.putalpha(alpha)
        return cast
    rgb = sprite.convert("RGB")
    # Separate raised painted faces from darker chassis, recesses and outlines.
    face = ImageChops.multiply(alpha, rgb.getchannel("R").point(lambda v: (v-137)*12))
    height = Image.blend(alpha,face,0.57)
    soft = height.filter(ImageFilter.GaussianBlur(1.8*scale))
    crest = ImageChops.subtract(height,soft)
    recess = ImageChops.subtract(soft,height)
    # Leave room on near-white and saturated orange for the bevel highlight.
    rgb = rgb.point(lambda v: v*0.955)
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
