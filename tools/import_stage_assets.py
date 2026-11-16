"""Copy original assets referenced by all authored main and bonus stages."""
import re
import shutil
from pathlib import Path
from import_stages import ObjectReader, ROOT


def copy(source, destination):
    destination.parent.mkdir(parents=True, exist_ok=True)
    shutil.copyfile(source, destination)


stages = [f'Stage{n}' for n in range(1, 13)] + [f'BonusStage{n}' for n in range(1, 8)]
for stage in stages:
    source = (ROOT / 'legacy/StageDefinitions' / f'{stage}.cs').read_text(encoding='utf-8-sig')
    events = [ObjectReader(body).value() for body in re.findall(r'stage\.AddElement\(0,\s*(.*?)\);', source, re.S)]
    music = {re.search(r'stage.MusicName = "([^"]+)"', source)[1]}
    enemies = set()
    for event in events:
        kind = event['type'].removeprefix('Galaxy.CStageElement')
        if kind == 'SpawnerEnemy':
            enemies.add(event['Type'].removeprefix('Galaxy.C'))
        elif kind == 'MusicChanger':
            music.add(event['MusicName'])
        elif kind in ('Decoration', 'Building'):
            folder = 'Decoration' if kind == 'Decoration' else 'Static'
            name = event['TextureName'] + '.png'
            copy(ROOT / 'legacy/Content/Textures' / folder / name, ROOT / 'assets/textures/stage' / folder / name)
            if kind == 'Building':
                copy(ROOT / 'legacy/Content/Textures/Static/Destroyed' / name, ROOT / 'assets/textures/stage/Static/Destroyed' / name)
    for name in enemies:
        copy(ROOT / 'legacy/Content/Textures/Enemy' / (name + '.png'), ROOT / 'assets/textures' / (name + '.png'))
    for name in music:
        copy(ROOT / 'legacy/Content/Music' / (name + '.wav'), ROOT / 'assets/audio' / (name + '.wav'))
for folder in ('Background', 'Sky'):
    for source in (ROOT / 'legacy/Content/Textures' / folder).glob('*.png'):
        copy(source, ROOT / 'assets/textures' / source.name)
copy(ROOT / 'legacy/Content/Music/fading-memories.wav', ROOT / 'assets/audio/fading-memories.wav')
for name in ('Bonus', 'BigBonus', 'Powerup'):
    copy(ROOT / 'legacy/Content/Textures/Entity' / (name + '.png'), ROOT / 'assets/textures' / (name + '.png'))
copy(ROOT / 'legacy/Content/SE/BonusGet.wav', ROOT / 'assets/audio/BonusGet.wav')
for source_folder, destination_folder in [('Weapons', 'weapons'), ('UI/Shop', 'shop')]:
    for source in (ROOT / 'legacy/Content/Textures' / source_folder).glob('*.png'):
        copy(source, ROOT / 'assets/textures' / destination_folder / source.name)
for source in (ROOT / 'legacy/Content/Textures/Enemy').glob('*.png'):
    copy(source, ROOT / 'assets/textures' / source.name)
print('Copied original assets for all 12 main and 7 bonus stages')
