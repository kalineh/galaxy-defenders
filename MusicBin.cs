//
// MusicBin.cs
//

using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CMusicBin
    {
        public struct BinaryFrame
        {
            public byte channel0;
            public byte channel1;
            public byte channel2;
            public byte channel3;
            public byte channel4;
            public byte channel5;
            public byte channel6;
            public byte channel7;
        };

        public CGalaxy Game { get; private set; }
        public string CurrentFile { get; set; }
        public BinaryFrame[] CurrentBinary { get; set; }
        public int FrameCount { get; set; }
        public int DriftCounter { get; set; }
        public int DriftCompensationFrames { get; set; }

        public CMusicBin(CGalaxy game)
        {
            Game = game;
        }

        public void OpenMusic(string filename)
        {
#if XBOX360
            return;
#endif

            CurrentFile = filename;
            FrameCount = 0;

            // PC offset
            // presumably 1 frame for the initial buffering in audio engine
            FrameCount = 16;
            DriftCompensationFrames = 60;

            // NOTE: test on 360
            //Debug.Assert(false);

            if (File.Exists(filename) == false)
                return;

            // TODO: block read
            using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                CurrentBinary = new BinaryFrame[stream.Length];
                for (int i = 0; i < stream.Length; ++i)
                {
                    CurrentBinary[i] = new BinaryFrame()
                    {
                        channel0 = (byte)stream.ReadByte(),
                        channel1 = (byte)stream.ReadByte(),
                        channel2 = (byte)stream.ReadByte(),
                        channel3 = (byte)stream.ReadByte(),
                        channel4 = (byte)stream.ReadByte(),
                        channel5 = (byte)stream.ReadByte(),
                        channel6 = (byte)stream.ReadByte(),
                        channel7 = (byte)stream.ReadByte(),
                    };
                }
            }
        }

        public void Tick()
        {
#if XBOX360
            return;
#endif
            FrameCount++;
            DriftCounter++;

            if (DriftCounter >= DriftCompensationFrames)
            {
                FrameCount++;
                DriftCounter = 0;
            }
        }

        public byte GetChannelData(int channel)
        {
#if XBOX360
            return 0;
#endif
            if (CurrentBinary == null)
                return (byte)0;

            if (FrameCount >= CurrentBinary.Length)
                return (byte)0;

            switch (channel)
            {
                case 0: return CurrentBinary[FrameCount].channel0;
                case 1: return CurrentBinary[FrameCount].channel1;
                case 2: return CurrentBinary[FrameCount].channel2;
                case 3: return CurrentBinary[FrameCount].channel3;
                case 4: return CurrentBinary[FrameCount].channel4;
                case 5: return CurrentBinary[FrameCount].channel5;
                case 6: return CurrentBinary[FrameCount].channel6;
                case 7: return CurrentBinary[FrameCount].channel7;
            }

            return (byte)0;
        }
    }
}

