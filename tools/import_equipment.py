"""Evaluate original equipment definitions with the installed .NET SDK.

No packages, XNA installation, or native runtime dependency. Generated plain C
tables are checked in; this offline tool is only needed when definitions change.
"""
import argparse
import json
import os
from pathlib import Path
import re
import subprocess

ROOT = Path(__file__).resolve().parents[1]
OUT = ROOT / 'build/headless/equipment-export'


def block(source, marker):
    start = source.index(marker)
    opening = source.index('{', start)
    depth = 1
    end = opening + 1
    while depth:
        depth += (source[end] == '{') - (source[end] == '}')
        end += 1
    return source[start:end]


def export():
    OUT.mkdir(parents=True, exist_ok=True)
    declarations = []
    for file, names in [
        ('Weapon/WeaponDefinitions.cs', ['WeaponDefinitions']),
        ('Ship/ShipDefinitions.cs', ['ChassisDefinitions', 'GeneratorDefinitions', 'ShieldDefinitions']),
        ('Ship/ShipParts.cs', ['CChassisPart', 'CGeneratorPart', 'CShieldPart', 'CWeaponPart']),
    ]:
        source = (ROOT / 'legacy' / file).read_text(encoding='utf-8-sig')
        declarations.extend(block(source, 'public class ' + name) for name in names)
    for path in sorted((ROOT / 'legacy/Projectile').glob('*.cs')):
        source = path.read_text(encoding='utf-8-sig')
        for name in re.findall(r'public struct (\w+CustomData)', source):
            declarations.append(block(source, 'public struct ' + name))
    stubs = '''
using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Xna.Framework;
namespace Microsoft.Xna.Framework {
    public struct Vector2 {
        public float X, Y;
        public Vector2(float x, float y) { X=x; Y=y; }
        public static Vector2 Zero => new(0,0);
        public static Vector2 UnitX => new(1,0);
        public static Vector2 UnitY => new(0,1);
        public static Vector2 operator *(Vector2 v, float f) => new(v.X*f,v.Y*f);
    }
    public static class MathHelper {
        public static float ToRadians(float f) => f * (float)Math.PI / 180;
    }
}
namespace Galaxy {
    public static class Time { public static float ToSeconds(int n) => n/60.0f; }
'''
    program = '''
class Program {
    static void Main() {
        Console.Write(JsonSerializer.Serialize(new {
            weapons=WeaponDefinitions.Items, chassis=ChassisDefinitions.Items,
            generators=GeneratorDefinitions.Items, shields=ShieldDefinitions.Items
        }, new JsonSerializerOptions { IncludeFields=true }));
    }
}
}
'''
    (OUT / 'Program.cs').write_text(stubs + '\n'.join(declarations) + program)
    (OUT / 'export.csproj').write_text('<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup>'
        '<OutputType>Exe</OutputType><TargetFramework>net10.0</TargetFramework>'
        '<RestoreSources></RestoreSources></PropertyGroup></Project>')
    (OUT / 'NuGet.Config').write_text('<configuration><packageSources><clear /></packageSources></configuration>')
    flags = subprocess.CREATE_NO_WINDOW if os.name == 'nt' else 0
    environment = os.environ.copy()
    if os.name == 'nt' and not environment.get('APPDATA'):
        environment['APPDATA'] = str(Path.home() / 'AppData/Roaming')
    if os.name == 'nt':
        drive = Path.home().anchor
        environment.setdefault('ProgramData', str(Path(drive) / 'ProgramData'))
        environment.setdefault('ProgramFiles(x86)', str(Path(drive) / 'Program Files (x86)'))
        environment.setdefault('ProgramFiles', str(Path(drive) / 'Program Files'))
    build = subprocess.run(['dotnet', 'build', str(OUT / 'export.csproj'), '--nologo', '-v:q',
        '-p:RestoreConfigFile=' + str(OUT / 'NuGet.Config'), '-p:RestorePackagesPath=' + str(OUT / 'packages')],
        capture_output=True, text=True, creationflags=flags, env=environment)
    if build.returncode:
        raise SystemExit(build.stdout + build.stderr)
    return json.loads(subprocess.check_output(['dotnet', str(OUT / 'bin/Debug/net10.0/export.dll')], creationflags=flags))


def ident(name):
    return re.sub(r'(?<=[a-z])(?=[A-Z])', '_', name).replace(' ', '_').upper()


def number(value):
    if value is None:
        return '0'
    if isinstance(value, bool):
        return str(value).lower()
    return format(value, '.9g')


def expand_sidekicks(weapons):
    """Build five native marks from each original wing weapon's emitters."""
    for name in ('MiniShot', 'ChargeShot', 'Blade', 'MissileVolley', 'MissileLauncher', 'LaserShot'):
        weapon = weapons[name]
        original, = weapon['Data']
        levels = []
        for mark in range(5):
            gun = original[0].copy()
            level = [gun]
            if name == 'MissileVolley':
                pairs = (1, 2, 3, 4, 4)[mark]
                level = [e.copy() for e in original[4-pairs:4+pairs]]
                for rocket in level:
                    rocket['Damage'] = 0.31 if mark == 4 else 0.22
                    if mark == 4:
                        rocket['CustomData'] = {'ScaleOverride': 1.25}
            elif name == 'Blade':
                gun.update(Damage=(0.25, 0.4, 0.45, 0.48, 0.55)[mark],
                           ReloadTime=(0.4, 0.4, 0.4, 0.32, 0.32)[mark],
                           CustomData={'ScaleOverride': (0.72, 1, 1.35, 1.35, 1.45)[mark],
                                       'Lifetime': 18 if mark == 4 else 9})
            elif name == 'ChargeShot':
                gun.update(Damage=(2, 2.5, 3, 3.25, 3.4)[mark],
                           CustomData={'ChargeFrames': (60, 60, 54, 42, 42)[mark],
                                       'Bounces': int(mark == 4)})
            elif name == 'LaserShot':
                gun.update(Damage=(0.1, 0.16, 0.14, 0.2, 0.22)[mark],
                           CustomData={'Bounces': int(mark == 4)})
                if mark >= 2:
                    level = [dict(gun, Offset={'X': 0, 'Y': y}) for y in (-5, 5)]
            elif name == 'MiniShot':
                gun.update(Damage=(0.025, 0.035, 0.035, 0.04, 0.045)[mark])
                burst = (1, 1, 2, 2, 3)[mark]
                period = (5, 5, 8, 7, 10)[mark]
                level = [dict(gun, AutoDischarge=1+2*i, ReloadTime=(period-2*i-0.25)/60)
                         for i in range(burst)]
            elif name == 'MissileLauncher':
                gun.update(Damage=(0.2, 0.26, 0.32, 0.38, 0.38)[mark],
                           CustomData={'Radius': (0, 32, 46, 62, 62)[mark]})
                if mark == 4:
                    level.append(dict(gun, AutoDischarge=7, ReloadTime=gun['ReloadTime']-0.1))
            levels.append(level)
        weapon['Data'] = levels
    weapons['Blade']['BasePrice'] = 3600
    weapons['MiniShot']['BasePrice'] = 2800
    weapons['MissileVolley']['BasePrice'] = 1100
    for name in ('MissileVolley', 'MissileLauncher', 'LaserShot'):
        weapons[name]['DisplayName'] = re.sub(r'([a-z])([A-Z])', r'\1 \2', name)


def generate(data):
    expand_sidekicks(data['weapons'])
    names = list(data['weapons'])
    header = ['/* Generated by tools/import_equipment.py; do not edit. */',
        '#ifndef GALAXY_EQUIPMENT_DATA_H', '#define GALAXY_EQUIPMENT_DATA_H',
        'typedef enum { ' + ', '.join('WEAPON_' + ident(n) for n in names) + ', WEAPON_COUNT } WeaponKind;',
        '#define GENERATOR_COUNT ' + str(len(data['generators'])),
        '#define SHIELD_COUNT ' + str(len(data['shields'])), '#endif', '']
    lines = ['/* Generated by tools/import_equipment.py from legacy definitions plus native tuning; do not edit. */']
    fields = {
        'ReloadTime': 'reload', 'Speed': 'speed', 'Damage': 'damage',
        'KickbackForce': 'kickback', 'Rotation': 'rotation', 'Energy': 'energy',
        'ChargeSpeed': 'charge_speed', 'AutoDischarge': 'auto_discharge',
        'ToggleWeapon': 'toggle', 'ToggleEnergyDrain': 'drain',
        'RandomRotation': 'random_rotation', 'RandomReloadTime': 'random_reload',
        'ShowSidekick': 'show_sidekick',
    }
    custom = {n: re.sub(r'(?<=[a-z])(?=[A-Z])', '_', n).lower() for n in [
        'Lifetime', 'Friction', 'SprayAngle', 'StartRotation', 'FireRotationSpeed',
        'AntiCameraSpeed', 'ScaleOverride', 'Bounces', 'Width', 'Radius', 'ChargeFrames']}
    for name, definition in data['weapons'].items():
        for level, emitters in enumerate(definition['Data']):
            lines.append(f'static const WeaponEmitter emit_{name}_{level}[] = {{')
            for e in emitters:
                assert set(e) == set(fields) | {'Offset', 'CustomData'}, set(e)
                if name.endswith('Focus'):
                    e = e.copy()
                    # Native balance: a stronger, more expensive Shift attack.
                    # Correct the legacy 0.75 outlier between 0.05 and 0.10.
                    if name == 'BeamFocus' and level == 1:
                        e['Damage'] = 0.075
                    e['Damage'] *= 1.25
                    for key in ('Energy', 'ToggleEnergyDrain'):
                        e[key] *= 1.5
                    if name == 'PlasmaFocus':
                        # Leave a little headroom for one shot on the Stage 4 Booster.
                        e['Energy'] = 5.75
                elif name == 'Plasma':
                    e = dict(e, Damage=0.17)
                values = [f'.{field}={number(e[key])}' for key, field in fields.items() if e[key]]
                values += [f'.offset_x={number(e["Offset"]["X"])}', f'.offset_y={number(e["Offset"]["Y"])}']
                for key, value in (e['CustomData'] or {}).items():
                    values.append(f'.{custom[key]}={number(value)}')
                lines.append('    {' + ', '.join(values) + '},')
            lines.append('};')
    lines.append('const WeaponDefinition game_weapons[WEAPON_COUNT] = {')
    for name, definition in data['weapons'].items():
        levels = ', '.join('{%d, emit_%s_%d}' % (len(e), name, i) for i, e in enumerate(definition['Data']))
        lines.append('    [WEAPON_%s] = {%s, %s, %s, %d, %d, {%s}},' % (
            ident(name), json.dumps(name), json.dumps(definition['DisplayName'] or name),
            json.dumps(definition['Sound'] or ''), definition['BasePrice'], len(definition['Data']), levels))
    lines.append('};')
    for key, ctype, fields in [
        ('generators', 'Generator', ['Price','Energy','Regen','RegenOnCollide','RegenOnCoin']),
        ('shields', 'Shield', ['Price','Shield','EnergyDrain','Efficiency']),
    ]:
        lines.append(f'const {ctype} game_{key}[] = {{')
        for name, item in data[key].items():
            values = [json.dumps(name), json.dumps(Path(item['IconName']).name)]
            values += [number(item[f]) for f in fields]
            lines.append('    {' + ', '.join(values) + '},')
        lines.append('};')
    return {'src/equipment_data.h': '\n'.join(header), 'src/equipment_data.inc': '\n'.join(lines) + '\n'}


if __name__ == '__main__':
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--check', action='store_true')
    args = parser.parse_args()
    data = export()
    for path, content in generate(data).items():
        target = ROOT / path
        if args.check:
            if not target.exists() or target.read_text(encoding='utf-8') != content:
                raise SystemExit(f'Stale generated equipment: {path}')
        else:
            target.write_text(content, encoding='utf-8', newline='\r\n')
    print(f'Imported {len(data["weapons"])} weapon variants, {len(data["generators"])} generators, {len(data["shields"])} shields')
