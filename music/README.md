# Synth soundtrack

All campaign, boss, bonus, title/shop, pause, game-over and ending music now uses
the C synth album. Legacy recordings remain analysis references and are never
loaded for playback. SFX remain the original assets.

## Developer album

Open the console and enter `debug.music`. Up/Down selects and immediately plays
one of 19 tracks. Left/Right seeks by 5% of its duration, Enter restarts, Escape
closes the album and restores the current game's synth music. The album freezes
gameplay and its background music while open and uses the bundled console font. It is not a player menu.

`debug.music 1..19 [0..1]` also auditions directly; `debug.music off` restores the
game soundtrack. Volume follows music/master settings. Headless/muted runs do
not initialize audio. Seeking is immediate through pre-rendered PCM streams.

## Scores and build

Edit `album.json`, then run `python tools/compile_music.py`. It contains each
track's title, source mapping, tempo, motifs, chord cycle and section arrangement.
The 19 arrangements are first drafts sharing a synth palette and compositional
vocabulary; they are not transcriptions or listening-approved final compositions.
Candidate section cuts come from the analysis; original recording lengths are
preserved to the nearest 48 kHz sample. Stage data and music-switch events are
unchanged. See ../MUSIC_TIMING.md for exact anchors versus tentative musical cues.

Native/render builds compile the C renderer and cache WAVs in build/music, then
copy them beside the executable under assets/synth. No generated WAV is committed.
Initial builds take longer to render the album; subsequent builds reuse it.
The same 24-voice, nine-patch C synth produces all tracks. Bass/kick gain and mix
drive are stronger than the first prototype. FM, pulse, bell/pad voices, drums,
filtering, vibrato and stereo delay remain editable in src/synth.c.

`./scripts/render_music.sh` renders track 1 standalone. The built renderer also
accepts `OUTPUT.wav TRACK_ID`. Audio renders are checked for finite samples,
headroom and voice stealing. ./test.sh checks sample-identical seek/chunk output,
loop endpoints, console parsing and game-state isolation. Musical quality and
live device playback still require human listening; automated checks are silent.

## Focused identity pass: tracks 3 and 7

The first album compiler imposed a repeating eighth-note root/third/fifth pulse
arpeggio on every track. Track 3 began F-A-C and track 7 F#-A-C#, at 160/154 BPM,
with the same pulse/pad/bell opening. That template, plus related lead motifs,
made different tracks sound like transpositions of one piece.

Track 3 now uses an explicit syncopated FM phrase with fifth/octave leaps and a
fuller rhythmic opening. Track 7 uses low bell calls, semitone tension, longer
rests and a stripped opening. Both have newly written lead motifs. Their section
boundaries and durations are unchanged. The optional arp_pattern/arp_patch/
arp_register fields let composition specify notes, rhythm and sound directly.
The other seventeen tracks are unchanged in this pass and still use the shared
arpeggio fallback; further identity work is not claimed complete.
