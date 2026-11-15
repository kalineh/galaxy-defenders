"""Copy original assets referenced by the currently playable authored stages."""
import re
import shutil
from pathlib import Path
from import_stages import ObjectReader, ROOT


def copy(source, destination):
    destination.parent.mkdir(parents=True, exist_ok=True)
    shutil.copyfile(source, destination)


for number in (1, 2):
    source = (ROOT / 'legacy/StageDefinitions' / f'Stage{number}.cs').read_text(encoding='utf-8-sig')
    events = [ObjectReader(body).value() for body in re.findall(r'stage\.AddElement\(0,\s*(.*?)\);', source, re.S)]
    music = {re.search(r'stage.MusicName = "([^"]+)"', source)[1]}
    enemies = {'Boss1Chunk'} if number == 1 else set()
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
copy(ROOT / 'legacy/Content/Textures/Background/Water.png', ROOT / 'assets/textures/Water.png')
for source_folder, destination_folder in [('Weapons', 'weapons'), ('UI/Shop', 'shop')]:
    for source in (ROOT / 'legacy/Content/Textures' / source_folder).glob('*.png'):
        copy(source, ROOT / 'assets/textures' / destination_folder / source.name)
print('Copied original assets for Stage 1 and Stage 2')
