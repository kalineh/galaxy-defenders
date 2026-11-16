"""Translate the checked-in editor-generated C# stage data, without guessing fields.

No C# execution or runtime parser: build inputs remain small, plain C tables.
Run with --check to verify the generated file exactly matches the legacy source.
"""
import argparse
import hashlib
import json
from pathlib import Path
import re
import struct

ROOT = Path(__file__).resolve().parents[1]
TOKEN = re.compile(r'\s*("(?:\\.|[^"\\])*"|-?\d+(?:\.\d+)?f?|[A-Za-z_][\w.]*|[(){},=])')


class ObjectReader:
    def __init__(self, source):
        self.tokens = []
        position = 0
        while source[position:].strip():
            match = TOKEN.match(source, position)
            if not match:
                raise ValueError(f"Unsupported stage syntax: {source[position:position + 80]!r}")
            self.tokens.append(match[1])
            position = match.end()
        self.cursor = 0

    def take(self, expected=None):
        token = self.tokens[self.cursor]
        self.cursor += 1
        if expected is not None and token != expected:
            raise ValueError(f"Expected {expected!r}, got {token!r}")
        return token

    def value(self):
        token = self.take()
        if token == 'new':
            result = {'type': self.take()}
            self.take('('); self.take(')'); self.take('{')
            while self.tokens[self.cursor] != '}':
                key = self.take(); self.take('=')
                if key in result:
                    raise ValueError(f"Duplicate field {key}")
                result[key] = self.value()
                self.take(',')
            self.take('}')
            return result
        if token == 'typeof':
            self.take('('); result = self.take(); self.take(')')
            return result
        if token == 'null':
            return None
        if token in ('true', 'false'):
            return token == 'true'
        if token.startswith('"'):
            return json.loads(token)
        if re.fullmatch(r'-?\d+(?:\.\d+)?f?', token):
            return float(token.rstrip('f'))
        raise ValueError(f"Unsupported value {token}")


def vector(value):
    if set(value) != {'type', 'X', 'Y'} or value['type'] != 'Microsoft.Xna.Framework.Vector2':
        raise ValueError(f"Unexpected vector: {value}")
    return value['X'], value['Y']


def number(value):
    return f'{float(value):.9g}' + ('.0f' if float(value).is_integer() else 'f')


def quoted(value):
    return json.dumps(value, ensure_ascii=True)


KINDS = {
    'Decoration': ('STAGE_DECORATION', {'TextureName', 'DepthOffset', 'Rotation'}),
    'Building': ('STAGE_BUILDING', {'TextureName', 'Coins', 'Powerup'}),
    'SpawnerEnemy': ('STAGE_ENEMY', {'Type', 'SpawnPosition', 'MoverPresetName', 'MoverSpeedMultiplier',
                                  'MoverTransitionMultiplier', 'CustomElement', 'NoDropCoins', 'Powerup'}),
    'CameraStop': ('STAGE_CAMERA_STOP', set()),
    'StageFinish': ('STAGE_FINISH', set()),
    'MusicChanger': ('STAGE_MUSIC', {'MusicName'}),
    'HealthBar': ('STAGE_HEALTH_BAR', set()),
    'SecretEntry': ('STAGE_SECRET', {'SecretStage', 'Expired'}),
}


def event_row(event):
    kind = event['type'].removeprefix('Galaxy.CStageElement')
    enum, fields = KINDS[kind]
    if set(event) != fields | {'type', 'Position'}:
        raise ValueError(f"Unexpected {kind} fields: {set(event)}")
    x, y = vector(event['Position'])
    output = [f'.kind={enum}', f'.x={number(x)}', f'.y={number(y)}']
    if kind == 'SpawnerEnemy':
        spawn = event['SpawnPosition']
        if set(spawn) != {'type', 'Position'} or spawn['type'] != 'Galaxy.CSpawnPositionFixed' or event['CustomElement'] is not None:
            raise ValueError(f"Unsupported spawn: {event}")
        sx, sy = vector(spawn['Position'])
        output += [f'.name={quoted(event["Type"].removeprefix("Galaxy.C"))}',
                   f'.spawn_x={number(sx)}', f'.spawn_y={number(sy)}',
                   f'.mover={quoted(event["MoverPresetName"])}',
                   f'.speed={number(event["MoverSpeedMultiplier"])}',
                   f'.transition={number(event["MoverTransitionMultiplier"])}',
                   f'.no_coins={str(event["NoDropCoins"]).lower()}', f'.powerup={str(event["Powerup"]).lower()}']
    elif kind in ('Decoration', 'Building'):
        output += [f'.name={quoted(event["TextureName"])}']
        folder = 'Decoration' if kind == 'Decoration' else 'Static'
        png = ROOT / 'legacy' / 'Content' / 'Textures' / folder / (event['TextureName'] + '.png')
        width, height = struct.unpack('>II', png.read_bytes()[16:24]) if event['TextureName'] else (0, 0)
        output += [f'.width={width}', f'.height={height}']
        if kind == 'Decoration':
            output += [f'.rotation={number(event["Rotation"])}', f'.depth={number(event["DepthOffset"])}']
        else:
            if not event['Coins'].is_integer():
                raise ValueError('Non-integer coin count')
            output += [f'.coins={int(event["Coins"])}', f'.powerup={str(event["Powerup"]).lower()}']
    elif kind == 'MusicChanger':
        output += [f'.name={quoted(event["MusicName"])}']
    elif kind == 'SecretEntry':
        output += [f'.name={quoted(event["SecretStage"])}']
        output += [f'.expired={str(event["Expired"]).lower()}']
    return '    {' + ', '.join(output) + '},'


def generate():
    names = [f'Stage{i}' for i in range(1, 13)] + [f'BonusStage{i}' for i in range(1, 8)] + [f'OldStage{i}' for i in range(1, 13)]
    lines = ['/* Generated by tools/import_stages.py. Do not edit. */']
    definitions = []
    count = 0
    for name in names:
        path = ROOT / 'legacy' / 'StageDefinitions' / f'{name}.cs'
        source = path.read_text(encoding='utf-8-sig')
        bodies = re.findall(r'stage\.AddElement\(0,\s*(.*?)\);', source, re.S)
        if len(bodies) != source.count('stage.AddElement(') or not bodies:
            raise ValueError(f'{name}: unsupported AddElement call')
        events = []
        for body in bodies:
            reader = ObjectReader(body)
            event = reader.value()
            if name.startswith('OldStage'):
                kind = event['type'].removeprefix('Galaxy.CStageElement')
                # Unspecified auto-properties use C# zero/false defaults.
                for key in {'Decoration': ('Rotation', 'DepthOffset'), 'Building': ('Coins',)}.get(kind, ()):
                    event.setdefault(key, 0.0)
                if kind == 'SpawnerEnemy': event.setdefault('NoDropCoins', False)
            events.append(event)
            if reader.cursor != len(reader.tokens):
                raise ValueError(f'{name}: trailing event syntax')
        metadata = {}
        for key in ('DisplayName', 'BackgroundSceneryName', 'ForegroundSceneryName', 'MusicName'):
            matches = re.findall(r'stage\.' + key + r' = ("(?:\\.|[^"\\])*");', source)
            if not matches and key == 'DisplayName' and name.startswith('OldStage'):
                matches = ['""']
            if len(matches) != 1:
                raise ValueError(f'{name}: missing/duplicate {key}')
            metadata[key] = json.loads(matches[0])
        speeds = re.findall(r'stage.ScrollSpeed = (-?[\d.]+)f;', source)
        if len(speeds) != 1 or float(speeds[0]) <= 0:
            raise ValueError(f'{name}: invalid scroll speed')
        finish = [e for e in events if e['type'] == 'Galaxy.CStageElementStageFinish']
        if len(finish) != 1:
            raise ValueError(f'{name}: expected one finish marker')
        lines += [f'/* {path.relative_to(ROOT).as_posix()} SHA256 {hashlib.sha256(path.read_bytes()).hexdigest()} */',
                  f'static const StageEvent events_{name}[] = {{']
        lines += [event_row(event) for event in events]
        lines += ['};', '']
        definitions.append('    {' + ', '.join([quoted(name), quoted(metadata['DisplayName']),
            quoted(metadata['BackgroundSceneryName']), quoted(metadata['ForegroundSceneryName']), quoted(metadata['MusicName']),
            number(float(speeds[0])), number(-vector(finish[0]['Position'])[1]), f'events_{name}', str(len(events))]) + '},')
        count += len(events)
    lines += ['const StageDefinition stage_definitions[STAGE_COUNT] = {', *definitions, '};', '']
    return '\n'.join(lines), count


if __name__ == '__main__':
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--check', action='store_true')
    args = parser.parse_args()
    output, count = generate()
    path = ROOT / 'src' / 'stage_data.inc'
    if args.check:
        if not path.exists() or path.read_text(encoding='utf-8') != output:
            raise SystemExit('Stage data is stale; run python tools/import_stages.py')
    else:
        path.write_text(output, encoding='utf-8', newline='\n')
    print(f'PASS: 19 campaign + 12 archived stages, {count} events, every field accounted for')
