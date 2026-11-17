"""Build a disposable album listening worksheet from the current rendered scores."""
import html
import json
from pathlib import Path
from urllib.parse import quote
import wave

ROOT = Path(__file__).resolve().parents[1]
OUT = ROOT / 'artifacts/native-port/album-listening.html'


def main():
    scores = json.loads((ROOT / 'music/album.json').read_text())
    cards = []
    for number, score in enumerate(scores, 1):
        with wave.open(str(ROOT / f'build/music/{number}.wav')) as wav:
            duration = wav.getnframes() / wav.getframerate()
        assert abs(duration - score['sections'][-1]['end']) < 1 / 48000
        cuts = ' '.join(f'<button data-track="{number}" data-time="{s["start"]}">'
                        f'{s["start"]:g}s</button>' for s in score['sections'])
        first_lead = next((s['start'] for s in score['sections'] if s['lead']), 0)
        # This musical phrase jump supplements the exact orchestration cuts.
        development = min(duration - 1, max(first_lead, 8 * 240 / score['bpm']))
        source = quote(score['reference_duration'] + '.wav', safe='')
        cards.append(f'''<article>
<h2>{number:02} &middot; {html.escape(score['name'])}</h2>
<p>{html.escape(score['identity'])}</p>
<audio id="track-{number}" controls preload="none" src="../../build/music/{number}.wav"></audio>
<div class="cuts">Section cuts: {cuts}
<button data-track="{number}" data-time="{development}">Development</button></div>
<p class="meta">{duration:.3f}s &middot; {score['bpm']} BPM &middot;
{html.escape(score['instruments']['lead']['name'].replace('_', ' '))} /
{html.escape(score['instruments']['arp']['name'].replace('_', ' '))}</p>
<details><summary>Original reference: {html.escape(score['reference_duration'])}</summary>
<p>MIDI timing is not synchronized to this recording. Compare character, not note copying.</p>
<audio controls preload="none" src="../../legacy/Content/Music/{source}"></audio></details>
</article>''')
    document = '''<!doctype html><html lang="en"><meta charset="utf-8">
<meta name="viewport" content="width=device-width,initial-scale=1">
<title>Galaxy Defenders — Album listening</title>
<style>
body{margin:32px auto;max-width:1000px;padding:0 20px;background:#101724;color:#e4eaf5;font:16px/1.5 system-ui}
h1{font-size:30px}h2{font-size:21px;margin-top:0}article{background:#1b2638;border:1px solid #35465f;padding:22px;margin:20px 0;border-radius:10px}
audio{width:100%;margin:8px 0}.meta,summary{color:#b6c8df}.cuts{margin-top:10px}
button{background:#304968;color:#fff;border:1px solid #7894b5;padding:6px 10px;margin:3px;border-radius:4px;cursor:pointer}
a{color:#9acbff}details{margin-top:14px}
</style><h1>Galaxy Defenders · Album listening</h1>
<p>Track 9 is the accepted working reference. The other 18 have new compositions
and instrument balances. All protected timing and layer switches are retained.</p>
<p>Nothing autoplays. Check melody, bass support, groove, balance and fatigue.
Section buttons start the selected new track at its exact cut; only one player runs at a time.
Use <code>debug.music</code> in the game for the developer album.</p>
''' + '\n'.join(cards) + '''
<script>
const players = [...document.querySelectorAll('audio')];
players.forEach(player => player.addEventListener('play', () => {
    players.forEach(other => { if (other !== player) other.pause(); });
}));
document.querySelectorAll('button[data-track]').forEach(button => {
    button.addEventListener('click', () => {
        const player = document.getElementById('track-' + button.dataset.track);
        player.currentTime = Number(button.dataset.time);
        player.play().catch(() => {});
    });
});
</script></html>'''
    OUT.parent.mkdir(parents=True, exist_ok=True)
    OUT.write_bytes(document.encode('utf-8'))
    print(OUT)


if __name__ == '__main__':
    main()
