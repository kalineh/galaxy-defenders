"""Bake gameplay materials in the coordinate system of the original sprites."""
from functools import cache
from io import BytesIO
from pathlib import Path

from PIL import Image, ImageChops, ImageDraw, ImageFilter, ImageOps
from resvg_py import svg_to_bytes


def inset(mask, pixels):
    # Erode at logical-pixel scale, with a slightly softened inner contour.
    return mask.filter(ImageFilter.BoxBlur(pixels)).point(lambda v: max(0, (v-243)*22))


def hull_details(rgb, face, scale, seed):
    safe = inset(face, 4*scale)
    groove = ImageChops.subtract(safe, inset(face, 5*scale))
    rgb = ImageChops.subtract(rgb, groove.point(lambda v: v*0.075).convert("RGB"))
    draw = ImageDraw.Draw(rgb)
    pitch = 53*scale
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


@cache
def material_swatch(material):
    # Sample the actual SVG paint so kit parts and procedurally shaded faces
    # cannot acquire different orange or graphite ramps.
    rect = f'<rect width="100" height="100" fill="url(#{material})"/>'
    svg = cast_library().replace("</svg>", rect + "</svg>")
    data = svg_to_bytes(svg_string=svg, width=100, height=100)
    return Image.open(BytesIO(data)).convert("RGB")


def material_face(material, alpha):
    bounds = alpha.getbbox()
    width, height = bounds[2]-bounds[0], bounds[3]-bounds[1]
    swatch = material_swatch(material)
    face = Image.new("RGB", alpha.size, swatch.getpixel((50,50)))
    face.paste(swatch.resize((width,height),Image.Resampling.BILINEAR), bounds)
    return face


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


def armor_face(sprite, scale, name):
    alpha = sprite.getchannel("A")
    red, green, _ = sprite.convert("RGB").split()
    # Gray is the lower casting. Intermediate colors in the vector source
    # are antialiasing between that casting and the painted shell.
    coverage = ImageChops.subtract(red, green).filter(ImageFilter.GaussianBlur(scale*0.45))
    coverage = coverage.point(lambda v: (v-18)*255/95)
    shell = ImageChops.multiply(alpha,coverage)
    bounds = alpha.getbbox()
    span = min(bounds[2]-bounds[0],bounds[3]-bounds[1])
    step = max(scale, min(6*scale, round(span*0.045)))
    iron = material_face("iron", alpha)
    if name == "BlackHole":
        iron = ImageOps.grayscale(sprite).point(lambda v: min(v,22)).convert("RGB")
    else:
        # Free-standing graphite parts (boss covers and weights) have a narrow
        # outside lip. Interior gray areas only receive the shell's contact shadow.
        rim = ImageChops.subtract(alpha,shifted(alpha,0,max(1,step//2)))
        iron = ImageChops.add(iron,rim.point(lambda v: v*0.08).convert("RGB"))
    paint = material_face("paint", alpha)
    if name == "BonusShip":
        paint = ImageOps.colorize(paint.getchannel("R"), "#492044", "#e46cd7")
    # A contact shadow inside every well reads even when the enemy rotates.
    contact = shell.filter(ImageFilter.GaussianBlur(step))
    iron.paste((36,36,38),mask=contact)
    lower = ImageChops.subtract(shell,shifted(shell,0,-step))
    paint.paste((172,64,41) if name != "BonusShip" else (152,68,145),mask=lower)
    upper = ImageChops.subtract(shell,shifted(shell,0,max(1,step//3)))
    paint = ImageChops.add(paint,upper.point(lambda v: v*0.055).convert("RGB"))
    return Image.composite(paint,iron,shell)


def projectile_material(sprite, name, scale):
    alpha = sprite.getchannel("A")
    if name in {"EnemyMissile", "EnemyShot"}:
        return armor_face(sprite,scale,name)
    # Energy has a luminous core. Physical player ordnance has a dipped
    # trailing bevel. Neutral values take the owning pilot's tint at draw time.
    if name in {"Blade","Boomerang","Bomblet","Missile","DrunkMissile","SeekBomb","Vulcan"}:
        bounds = alpha.getbbox()
        span = min(bounds[2]-bounds[0],bounds[3]-bounds[1])
        step = max(scale,min(3*scale,round(span*0.075)))
        lip = ImageChops.subtract(alpha,shifted(alpha,0,-step))
        return ImageOps.colorize(lip, "#f8f8f8", "#909090")
    core = alpha.filter(ImageFilter.BoxBlur(2*scale)).point(lambda v: (v-80)*255/175)
    if name.startswith("Enemy") or name in {"CutterBeam","FenceBeam"}:
        return ImageOps.colorize(core, "#ef5935", "#ff8b54")
    return ImageOps.colorize(core, "#b4b4b4", "#ffffff")


def cast_material(sprite, path, scale, building):
    alpha = sprite.getchannel("A")
    name = path.stem
    casting = name.removesuffix("Dead")
    wreck = casting != name or "Destroyed" in path.parts
    red = sprite.getchannel("R")
    if name == "Asteroid":
        stone = material_face("roof",alpha)
        edge = ImageChops.subtract(alpha,alpha.filter(ImageFilter.GaussianBlur(3*scale)))
        return ImageChops.subtract(stone,edge.point(lambda v: v*0.36).convert("RGB"))
    if name == "Cracks":
        return material_face("iron",alpha)
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
    if building:
        iron = material_face("iron",alpha)
        face = ImageChops.multiply(alpha,red.point(lambda r: (r-124)*7))
        body = ImageOps.colorize(red,"#514e50","#89878a")
        base = cast_plate(iron,face,body,2*scale)
        roof = ImageChops.multiply(alpha,red.point(lambda r: (r-185)*9))
        paint = material_face("roof",alpha)
        step = 2 if "Destroyed" in path.parts else 4
        return cast_plate(base,roof,paint,step*scale)
    if name == "Spike":
        # Both animation frames need the complete face gradient.
        half = sprite.width//2
        body = Image.new("RGB",sprite.size)
        for x in (0,half):
            frame = sprite.crop((x,0,x+half,sprite.height))
            body.paste(armor_face(frame,scale,name),(x,0))
        return body
    body = armor_face(sprite,scale,name)
    return body.point(lambda v: v*0.77) if wreck else body


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
    if path.parts[0] == "weapons" or name in {"EnemyShot","Laser","FenceBeam","CutterBeam"}:
        material = projectile_material(sprite,name,scale)
        material.putalpha(sprite.getchannel("A"))
        return material
    building = path.parts[:2] == ("stage","Static")
    ship = len(path.parts)==1 and name in ships
    if not building and not ship and path.as_posix() != "player/Sidekick.svg":
        if len(path.parts)!=1 or path.suffix!=".svg" or name.startswith("Medal") or name in {
            "BigBonus","Bonus","Powerup","TriangleParticle"
        }:
            return sprite

    alpha = sprite.getchannel("A")
    if not ship and path.as_posix() != "player/Sidekick.svg":
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
    else:
        rgb = hull_details(rgb,face,scale,sum(name.encode()))
    rgb = ImageChops.subtract(rgb, recess.point(lambda v: v*0.50).convert("RGB"))
    rgb = ImageChops.add(rgb, crest.point(lambda v: v*0.39).convert("RGB"))
    rgb.putalpha(alpha)
    return rgb
