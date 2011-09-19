using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using NAudio;
using NAudio.Midi;
using NAudio.Wave;

namespace PreprocessFFT
{
    public class Midi
    {
        MidiFile midi;
        List<IEnumerator<MidiEvent>> cursor_events;
        double cursor_ms;
        double tempo;
        double us_per_quarter_note;

        public Midi(string filename)
        {
            midi = new MidiFile(filename, false);     

            Console.WriteLine("MidiFile: {0}", filename);
            Console.WriteLine("Format: {0}", midi.FileFormat);
            Console.WriteLine("Delta Ticks Per Quarter Note: {0}", midi.DeltaTicksPerQuarterNote);
            Console.WriteLine("Tracks: {0}", midi.Tracks);
            Console.WriteLine("Events.StartAbsoluteTime: {0}", midi.Events.StartAbsoluteTime);

            //Debug.Assert(midi.Events.StartAbsoluteTime == 0);
            //Debug.Assert(midi.DeltaTicksPerQuarterNote == 120);
            //Debug.Assert(midi.Tracks == 1);

            tempo = 120.0f; // midi spec default?

                    //double seconds = e.AbsoluteTime / last_tempo;
                    //double milliseconds = seconds * 1000.0f;
                    //last_seconds = seconds;

            cursor_events = new List<IEnumerator<MidiEvent>>();
            for (int i = 0; i < midi.Tracks; ++i)
            {
                IEnumerator<MidiEvent> event_ = midi.Events[i].GetEnumerator();
                event_.MoveNext();
                cursor_events.Add(event_);
            }
        }

        private double EventTimeInMilliseconds(long t)
        {
            //double ms = (double)t / tempo;
            //double ticks_per_ms = (us_per_quarter_note / 1000.0) / (double)midi.DeltaTicksPerQuarterNote;
            //double ms = t * ticks_per_ms;
            //return ms;
            //double s = (double)t / midi.DeltaTicksPerQuarterNote;
            //return s * 1000.0;

            // again?
            //double t_in_us = (double)t / (double)us_per_quarter_note;
            //double t_in_ms = t_in_us * midi.DeltaTicksPerQuarterNote;
            //return t_in_ms / 100000.0f;
            //return t_in_ms;

            // try again
            double t_in_us = (double)t / (double)midi.DeltaTicksPerQuarterNote;
            double t_in_ms = t_in_us * us_per_quarter_note;
            return t_in_ms / 1000.0;
            //return t_in_ms;

            // almost working
            //double clocks_per_second = tempo * (double)midi.DeltaTicksPerQuarterNote / 60.0f;
            //double s = (double)t / clocks_per_second;
            //double ms = s * 1000.0f;
            //return ms;

            //BPM x PPQ = # (ticks/min)
            //(ticks/min) * 1/60000 = # ticks/millisecond
            //timestamp / 0.704 = # ms
        }

        public bool PlayMilliseconds(double ms)
        {
            // play events from current event to cursor ms

            bool played = false;

            cursor_ms += ms;

            foreach (IEnumerator<MidiEvent> event_iterator in cursor_events)
            {
                while (event_iterator.Current != null)
                {
                    MidiEvent e = event_iterator.Current;
                    long at = e.AbsoluteTime;
                    double ems = EventTimeInMilliseconds(at);

                    if (ems > cursor_ms)
                        break;

                    int c = e.Channel;
                    MidiCommandCode code = e.CommandCode;
                    int d = e.DeltaTime;

                    if (e is TempoEvent)
                    {
                        tempo = (e as TempoEvent).Tempo;
                        us_per_quarter_note = (e as TempoEvent).MicrosecondsPerQuarterNote;
                    }

                    if (code == MidiCommandCode.NoteOn)
                    {
                        NoteOnEvent ne = e as NoteOnEvent;
                        if (ne.OffEvent != null)
                        {
                            double offtime = EventTimeInMilliseconds(ne.OffEvent.AbsoluteTime);
                            double playtime = offtime - ems;
                            Console.WriteLine("{0}: {1}", ems, (e as NoteEvent).NoteName);
                            Console.Beep(400, (int)Math.Max(16, playtime));
                            played = true;
                        }
                    }

                    event_iterator.MoveNext();
                }
            }

            return played;
        }
    }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Song song;
        VisualizationData visData;
        Texture2D pixel;
        List<List<float>> history;
        List<VertexBuffer> vertices;
        MidiFile midiFile;
        SpriteFont spriteFont;
        SoundEffect soundEffect;
        Midi midi;

        public struct MidiData
        {
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //foreach (string s in GetFilenames())
                //DumpMidi(s);

            //midiFile = new MidiFile("../../../Content/gmdrum.mid", false);
            //midi = new Midi("../../../Content/gmdrum.mid");
            midi = new Midi("../../../Content/Afraid_of_Darkness.mid");
        }

        public void DumpMidi(string filename)
        {
            MidiFile midi = new MidiFile(filename, false);

            Console.WriteLine("File: {0}", filename);
            Console.WriteLine("Format: {0}", midi.FileFormat);
            Console.WriteLine("Delta Ticks Per Quarter Note: {0}", midi.DeltaTicksPerQuarterNote);
            Console.WriteLine("Tracks: {0}", midi.Tracks);
            Console.WriteLine("Events.StartAbsoluteTime: {0}", midi.Events.StartAbsoluteTime);

            Debug.Assert(midi.Events.StartAbsoluteTime == 0);
            Debug.Assert(midi.DeltaTicksPerQuarterNote == 120);
            Debug.Assert(midi.Tracks == 1);

            // 0 SequenceTrackName 1-[X]-0
            // 0 Copyright RushJet1 & RushJet1 & Converted by nsf2midi
            // 0 SetTempo 30bpm (2000000)
            // 0 Sysex: 5 bytes
            // 05 7E 7F 09 01 

            // The set-tempo meta-event (type 0x51) provides the value microseconds per quarter note,
            // a 24-bit (3-byte) unsigned integer. This value in conjunction with the division value
            // in the header chunk (division = ticks per quarter note) determines how to translate a
            // delta time in ticks to a time in seconds. If the set-tempo meta-event is not included
            // in the standard MIDI file, the value defaults to 500,000 microseconds per quarter note
            // (or 0.5 seconds per quarter note).

            for (int track = 0; track < midi.Tracks; ++track)
            {
                foreach (MidiEvent e in midi.Events[track])
                {
                    long at = e.AbsoluteTime;
                    int c = e.Channel;
                    MidiCommandCode code = e.CommandCode;
                    int d = e.DeltaTime;
                    //Console.WriteLine(e.ToString());
                    Console.WriteLine("{0}: ch {1}: {2} (delta: {3}", at, c, code, d);
                }
            }
        }

        public string[] GetFilenames()
        {
            return Directory.GetFiles("../../../Content/", "*.mid");
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();

            base.Initialize();

            MediaPlayer.IsVisualizationEnabled = true;
            visData = new VisualizationData();

            spriteBatch = new SpriteBatch(GraphicsDevice);

            history = new List<List<float>>();
            for (int i = 0; i < 256; ++i)
                history.Add(new List<float>());

            vertices = new List<VertexBuffer>();
            for (int i = 0; i < 256; ++i)
                vertices.Add(new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 256, BufferUsage.WriteOnly));
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //song = Content.Load<Song>("Alkali_Earth");

            pixel = Content.Load<Texture2D>("Pixel");

            spriteFont = Content.Load<SpriteFont>("Debug");
            soundEffect = Content.Load<SoundEffect>("gmdrum");
            soundEffect.Play();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            MediaPlayer.Volume = 0.005f;

            //if (MediaPlayer.State == MediaState.Stopped)
                //MediaPlayer.Play(song);

            //MediaPlayer.GetVisualizationData(visData);

            base.Update(gameTime);
        }

        protected void DrawLineGraph()
        {
            for (int i = 0; i < 256; ++i)
            {
                float frequency = visData.Frequencies[i];
                history[i].Add(frequency);
            }

            Vector2 position = new Vector2(0.0f, 0.0f);
            float width = 1280.0f;
            float height = 768.0f;
            float amplify = 16.0f;
            int samples = 32;
            for (int i = 0; i < samples; ++i)
            {
                int index = i;
                VertexPositionColor[] vertex_data = new VertexPositionColor[history[index].Count];
                for (int j = 0; j < history[index].Count; ++j)
                {
                    float h = height / (float)samples;
                    float x = j % width * 2.0f;
                    float y = (
                        (h * history[index * 4 + 0][j] * amplify) +
                        (h * history[index * 4 + 1][j] * amplify) +
                        (h * history[index * 4 + 2][j] * amplify) +
                        (h * history[index * 4 + 3][j] * amplify)) / 4.0f;
                    //spriteBatch.Draw(pixel, position + new Vector2(x, h * i + y), new Color((float)i / (float)samples, 0.0f, 0.0f, 1.0f));
                    vertex_data[j].Position = new Vector3(x, h * i + y, 0.0f);
                    vertex_data[j].Color = Color.White;
                }
                //vertices[i].SetData(vertex_data, 0, samples);

                Matrix viewMatrix = Matrix.CreateLookAt(
                    new Vector3(0.0f, 0.0f, 1.0f),
                    Vector3.Zero,
                    Vector3.Up
                    );

                Matrix projectionMatrix = Matrix.CreateOrthographicOffCenter(
                    0,
                    (float)GraphicsDevice.Viewport.Width,
                    (float)GraphicsDevice.Viewport.Height,
                    0,
                    1.0f, 1000.0f);


                VertexDeclaration declaration = new VertexDeclaration(GraphicsDevice, VertexPositionColor.VertexElements);
                BasicEffect basic = new BasicEffect(GraphicsDevice, null);
                basic.Begin();
                basic.World = Matrix.Identity;
                basic.View = viewMatrix;
                basic.Projection = projectionMatrix;
                foreach (EffectPass pass in basic.CurrentTechnique.Passes)
                {
                    pass.Begin();
                    GraphicsDevice.VertexDeclaration = declaration;
                    if (history[index].Count > 2)
                        GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, vertex_data, 0, history[index].Count - 1);
                    pass.End();
                }
                basic.End();
                //GraphicsDevice.DrawPrimitives(PrimitiveType.LineStrip, 0, 32);
            }
        }

        protected void DrawFrequencies()
        {
            float height = 800.0f;

            int bar_width = 2;
            int spacing = 1;

            for (int i = 0; i < 256; i++)
            {
                float sample = visData.Samples[i];
                float frequency = visData.Frequencies[i];

                int y = (int)(frequency * height);
                Rectangle r = new Rectangle(i * bar_width + i * spacing, (int)height - y, bar_width, y);
                spriteBatch.Draw(pixel, r, Color.White);
            }

            for (int i = 0; i < 256; i++)
            {
                float sample = (visData.Samples[i] + 1.0f) * 0.5f;
                float frequency = visData.Frequencies[i];

                int y = (int)(sample * height);
                Rectangle r = new Rectangle(i * bar_width + i * spacing, (int)height - y / 2, bar_width, y);
                spriteBatch.Draw(pixel, r, Color.Blue);
            }

            bar_width = 8;
            spacing = 16;
            for (int i = 0; i < 16; ++i)
            {
                float frequency = 0.0f;
                for (int j = 0; j < 16; ++j)
                    frequency += visData.Frequencies[(i * 16 + j) % 256];
                frequency /= 16.0f;

                int y = (int)(frequency * height);
                Rectangle r = new Rectangle(i * bar_width + i * spacing, (int)height - y, bar_width, y);
                spriteBatch.Draw(pixel, r, new Color(Color.Red, 180));
            }
        }

        protected void DrawChaos()
        {
            float height = 800.0f;

            int bar_width = 32;
            int spacing = 16;

            for (int i = 0; i < 16; ++i)
            {
                float average = 0.0f;
                for (int j = 0; j < 16; ++j)
                {
                    int index = (i * 16 + j) % 256;
                    average += visData.Frequencies[index];
                }
                average /= 16.0f;

                float chaos = 0.0f;
                for (int j = 0; j < 16; ++j)
                {
                    int index = (i * 16 + j) % 256;
                    //chaos += (float)Math.Abs(average - visData.Frequencies[index]);
                    chaos += (float)Math.Max(0.0f, visData.Frequencies[index] - average);
                }
                chaos /= 4.0f;

                int y = (int)(chaos * height);
                Rectangle r = new Rectangle(i * bar_width + i * spacing, (int)height - y, bar_width, y);
                spriteBatch.Draw(pixel, r, new Color(Color.Green, 180));
            }
        }

        protected void DrawChaos2()
        {
            float height = 800.0f;

            int bar_width = 16;
            int spacing = 16;

            for (int i = 0; i < 32; ++i)
            {
                float value = 8.0f;
                for (int j = 0; j < 8; ++j)
                {
                    int index = (i * 8 + j) % 256;
                    float f = visData.Frequencies[index];
                    if (f > 0.2f)
                        value *= f;
                }

                int y = (int)(value * height);
                Rectangle r = new Rectangle(i * bar_width + i * spacing, (int)height - y, bar_width, y);
                spriteBatch.Draw(pixel, r, new Color(Color.Black, 255));
            }
        }

        protected void DrawLocalMaximum()
        {
            float height = 800.0f;

            int bar_width = 16;
            int spacing = 16;

            for (int i = 0; i < 32; ++i)
            {
                float maximum = 0.0f;
                for (int j = 0; j < 8; ++j)
                {
                    int index = (i * 8 + j) % 256;
                    maximum = (float)Math.Max(maximum, visData.Frequencies[index]);
                }

                int y = (int)(maximum * height);
                Rectangle r = new Rectangle(i * bar_width + i * spacing, (int)height - y, bar_width, y);
                spriteBatch.Draw(pixel, r, new Color(Color.Yellow, 40));
            }
        }

        private int[] lastMidiTimers = new int[256];
        private double last_tempo = 1.0f;
        private double last_mpqn = 1.0f;
        private double last_seconds = 0.0f;
        protected void DrawMidi(int ms, MidiFile midi)
        {
            //int ms = (int)gameTime.TotalGameTime.TotalMilliseconds / 1000;
            ms /= 60;


            int[] pitches = new int[256];
            bool[] channels = new bool[256];

            // default pitch
            for (int i = 0; i < 256; ++i)
                pitches[i] = 8192;

            bool played = false;
            for (int track = 0; track < midi.Tracks; ++track)
            {
                int last = lastMidiTimers[track];
                for (int i = 0; i < midi.Events[track].Count; ++i)
                //foreach (MidiEvent e in midi.Events[track])
                {
                    MidiEvent e = midi.Events[track][i];
                    long at = e.AbsoluteTime;

                    //double tempo = 30.0f;
                    //double at_ms = 

                    int c = e.Channel;
                    MidiCommandCode code = e.CommandCode;
                    int d = e.DeltaTime;



                    // [  1 min    60 sec   1 beat     Z clocks ]
                    // | ------- * ------ * -------- * -------- | = seconds
                    // [ X beats   1 min    Y clocks       1    ]

                    // So, in the above conversion, X is the tempo, Y is the division,
                    // and Z is the number of clocks from the incoming event. You can
                    // see how all of the units cancel out, giving you a value in seconds.
                    // The condensed version of that conversion is therefore:

                    if (e is TempoEvent)
                    {
                        TempoEvent te = e as TempoEvent;
                        //double bpm = 1.0 / te.Tempo:
                        //double spm = 60.0 / 1.0;
                        //double bpc = 1.0 / 

                        //double seconds = (60.0 * e.AbsoluteTime) / (te.Tempo * te.MicrosecondsPerQuarterNote);
                        last_tempo = te.Tempo;
                        last_mpqn = te.MicrosecondsPerQuarterNote;
                    }



                    // (60 * clocks) / (tempo * division) = seconds
                    //Console.WriteLine(e.ToString());
                    //Console.WriteLine("{0}: ch {1}: {2} (delta: {3}", at, c, code, d);

                    if (code == MidiCommandCode.NoteOn)
                    {
                        channels[c] = true;
                        pitches[c] = (e as NoteOnEvent).Velocity;
                    }
                    if (code == MidiCommandCode.NoteOff)
                        channels[c] = false;
                    //if (code == MidiCommandCode.PitchWheelChange)
                        //pitches[c] = ((PitchWheelChangeEvent)e).Pitch;
                    //if (code == MidiCommandCode.PatchChange)
                        //patches[c] = ((PatchChangeEvent)e).Patch;

                    //dsoundOut.Play();

                    double seconds = e.AbsoluteTime / last_tempo;
                    double milliseconds = seconds * 1000.0f;
                    last_seconds = seconds;

                    if (seconds == ms)
                    {
                        if (channels[c] && !played)
                        {
                            int freq = pitches[c] * 8;
                            int time = d;
                            freq = Math.Max(37, freq);
                            freq = Math.Min(32767, freq);
                            time = Math.Max(1, time);
                            Console.Beep(freq, 32);

                            //played = true;
                        }
                    }

                    if (milliseconds > ms)
                    {
                        lastMidiTimers[track] = i;
                        break;
                    }
                }
            }

            float height = 800.0f;
            int bar_width = 32;
            int spacing = 2;

            for (int i = 0; i < 16; i++)
            {
                // Pitch Wheel Value 0 is minimum, 0x2000 (8192) is default, 0x4000 (16384) is maximum

                float sample = channels[i] ? 1.0f : 0.0f;//visData.Samples[i];
                float frequency = pitches[i] / 16384.0f;//visData.Frequencies[i];

                int y = (int)(frequency * height);
                Rectangle r = new Rectangle(i * bar_width + i * spacing, (int)height - y, bar_width, y);
                spriteBatch.Draw(pixel, r, Color.White);
                y = (int)(sample * height);
                r = new Rectangle(i * bar_width + i * spacing, (int)height - y, bar_width, y);
                spriteBatch.Draw(pixel, r, new Color(Color.Red, 80));
            }

            spriteBatch.DrawString(spriteFont, String.Format("{0}ms", last_seconds), new Vector2(10.0f, 10.0f), Color.White);
        }

        bool played_this_frame;
        void DrawPlayMidi()
        {
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            //DrawFrequencies();
            //DrawChaos();
            //DrawLocalMaximum();
            //DrawChaos2();
            //DrawMidi((int)gameTime.TotalGameTime.TotalMilliseconds, midiFile);

            midi.PlayMilliseconds(gameTime.ElapsedGameTime.Ticks / 10000.0);
            //played_this_frame = midi.PlayMilliseconds(16.77777);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
