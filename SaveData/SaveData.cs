//
// Profiles.cs
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Storage;

namespace Galaxy
{
    [Serializable]
    public struct SProfileGameData
    {
        public string Stage;
        public bool InProgress;
        public int Difficulty;
        public SProfilePilotState[] Pilots;
        public int[] StageScores;
        public int[] StageMedals;
        public int RandomPartsSeed;
    }

    [Serializable]
    public struct SProfilePilotState
    {
        public string Pilot;
        public bool AbilityUnlocked0;
        public bool AbilityUnlocked1;
        public bool AbilityUnlocked2;
        public int Score;
        public int Money;
        public string ChassisType;
        public string GeneratorType;
        public string ShieldType;
        public string WeaponPrimaryType;
        public int WeaponPrimaryLevel;
        public string WeaponSecondaryType;
        public int WeaponSecondaryLevel;
        public string WeaponSidekickType;
        public int WeaponSidekickLevel;

        public static SProfilePilotState MakeDefaultPilot(int index)
        {
            return new SProfilePilotState()
            {
                Pilot = index == 0 ? "Kazuki" : "Rabbit",
                AbilityUnlocked0 = false,
                AbilityUnlocked1 = false,
                AbilityUnlocked2 = false,
                Score = 0,
                Money = 0,
                ChassisType = "Rookie",
                GeneratorType = "Standard Generator",
                ShieldType = "Light Shield",
                WeaponPrimaryType = "FrontLaser",
                WeaponPrimaryLevel = 2,
                WeaponSecondaryType = "",
                WeaponSecondaryLevel = 0,
                WeaponSidekickType = "",
                WeaponSidekickLevel = 0,
            };
        }
    }

    // profile options
    [Serializable]
    public struct SProfileOptionsData
    {
        public float SFXVolume;
        public float MusicVolume;
        public float UserScale;
        //public int Language;
    }

    // single xbox user profile
    [Serializable]
    public struct SProfile
    {
        public static int CurrentVersion = 2;
        public int Version;
        public string Name;
        public bool[] HasClearedGame; // per-pilot
        public SProfileGameData[] Game;
        public SProfileOptionsData Options;
    }

    // all profiles save data
    [Serializable]
    public struct SSaveData
    {
        public List<SProfile> Profiles;
        public string CurrentProfile;
    }

    public class CSaveData
    {
        private static SSaveData DefaultSaveData;
        public static SSaveData SaveData;
        public static Mutex AccessMutex;
        public static Thread SaveThread;
        public static bool SaveRequestFlag;
        public static bool SaveIconVisible;
        private static bool SaveThreadRunning;

        static CSaveData()
        {
            DefaultSaveData = new SSaveData() {
                CurrentProfile = "galaxy",
                Profiles = new List<SProfile>() {
                    CreateDefaultProfile("galaxy"),
                }
            };
            SaveData = DefaultSaveData;
#if XBOX360
            AccessMutex = new Mutex(false);
#else
            AccessMutex = new Mutex(false, "CSaveData.AccessMutex");
#endif
        }

        public static void StartSaveThread()
        {
            SaveThreadRunning = true;
            ThreadStart start = new ThreadStart(SaveThreadLoop);
            SaveThread = new Thread(start);
            SaveThread.Start();
        }

        public static void StopSaveThread()
        {
            SaveThreadRunning = false;
        }

        public static void SaveThreadLoop()
        {
#if XBOX360
            int[] threads = new int[] { 3 };
            SaveThread.SetProcessorAffinity(threads);
#endif

            while (SaveThreadRunning)
            {
                if (!SaveRequestFlag)
                {
                    Thread.Sleep(1000);
                    continue; 
                }

                SSaveData copy;
                lock (AccessMutex)
                {
                    copy = CSaveData.SaveData;
                    SaveRequestFlag = false;
                }

                SaveIconVisible = true;
                Save();
#if !XBOX360
                // simulate slow saving on pc
                Thread.Sleep(2000);
#endif
                SaveIconVisible = false;
            }
        }

        public static void Load()
        {
            CSaveData.Import(out SaveData, "profiles.xml");
        }

        private static void Save()
        {
            CSaveData.Export(SaveData, "profiles.xml");
        }

        public static void SaveRequest()
        {
            lock (AccessMutex)
            {
                SaveRequestFlag = true;
            }
        }

        public static SProfile CreateDefaultProfile(string name)
        {
            return new SProfile()
            {
                Version = SProfile.CurrentVersion,
                Name = name,
                HasClearedGame = new bool[] { false, false, false, false },
                Game = new SProfileGameData[2] {
                    new SProfileGameData() {
                        Stage = "Start",
                        Difficulty = 1,
                        Pilots = new SProfilePilotState[] {
                            SProfilePilotState.MakeDefaultPilot(0),
                            SProfilePilotState.MakeDefaultPilot(1),
                        },
                        StageScores = new int[] { 0,0,0,0,0,0,0,0,0,0,0,0 },
                        StageMedals = new int[] { 0,0,0,0,0,0,0,0,0,0,0,0 },
                        RandomPartsSeed = 0,
                    },
                    new SProfileGameData() {
                        Stage = "Start",
                        Difficulty = 1,
                        Pilots = new SProfilePilotState[] {
                            SProfilePilotState.MakeDefaultPilot(0),
                            SProfilePilotState.MakeDefaultPilot(1),
                        },
                        StageScores = new int[] { 0,0,0,0,0,0,0,0,0,0,0,0 },
                        StageMedals = new int[] { 0,0,0,0,0,0,0,0,0,0,0,0 },
                        RandomPartsSeed = 0,
                    },
                },
                Options = new SProfileOptionsData() {
                    SFXVolume = 1.0f,
                    MusicVolume = 1.0f,
                    UserScale = 1.0f,
                },
            };
        }

        public static SProfile GetProfile(string name)
        {
            foreach (SProfile profile in SaveData.Profiles)
            {
                if (profile.Name == name)
                    return profile;
            }

            return new SProfile();
        }

        public static SProfile GetCurrentProfile()
        {
            foreach (SProfile profile in SaveData.Profiles)
            {
                if (profile.Name == SaveData.CurrentProfile)
                    return profile;
            }

            return new SProfile();
        }

        public static SProfileGameData GetCurrentGameData(CGalaxy game)
        {
            foreach (SProfile profile in SaveData.Profiles)
            {
                if (profile.Name == SaveData.CurrentProfile)
                {
                    return profile.Game[game.PlayersInGame - 1];
                }
            }

            return new SProfileGameData();
        }

        public static void AddNewProfile(string name)
        {
            foreach (SProfile profile in SaveData.Profiles)
            {
                if (profile.Name == name) 
                    return;
            }

            SProfile new_profile = CreateDefaultProfile(name);
            SaveData.Profiles.RemoveAll(existing => existing.Name == name);
            SaveData.Profiles.Add(new_profile);
        }

        public static void SetCurrentProfile(string name)
        {
            SaveData.CurrentProfile = name;
        }

        public static void SetCurrentProfileData(SProfile replacement)
        {
            int index = SaveData.Profiles.FindIndex(profile => profile.Name == replacement.Name);
            if (index != -1)
            {
                SaveData.Profiles[index] = replacement;
            }
        }

        public static int CalculateTotalMoney(SProfilePilotState pilot)
        {
            // TODO: cache/faster version
            int item_value =
                CWeaponFactory.GetTotalPriceForLevel(pilot.WeaponPrimaryType, pilot.WeaponPrimaryLevel) +
                CWeaponFactory.GetTotalPriceForLevel(pilot.WeaponSecondaryType, pilot.WeaponSecondaryLevel) +
                CWeaponFactory.GetTotalPriceForLevel(pilot.WeaponSidekickType, pilot.WeaponSidekickLevel) +
                ChassisDefinitions.GetPart(pilot.ChassisType).Price +
                GeneratorDefinitions.GetPart(pilot.GeneratorType).Price +
                ShieldDefinitions.GetPart(pilot.ShieldType).Price;

            int skill_value = 
                (pilot.AbilityUnlocked0 ? CAbility.AbilityPrice : 0) +
                (pilot.AbilityUnlocked1 ? CAbility.AbilityPrice : 0) +
                (pilot.AbilityUnlocked2 ? CAbility.AbilityPrice : 0);

            return item_value + skill_value + pilot.Money;
        }

        private static void Import(out SSaveData data, string filename)
        {
            lock (AccessMutex)
            {
                ImportImpl(out data, filename);
            }
        }

        private static void ImportImpl(out SSaveData data, string filename)
        {
#if XBOX360
            if (GuideUtil.StorageDevice == null || GuideUtil.StorageDevice.IsConnected == false)
            {
                data = DefaultSaveData;
                return;
            }

            StorageContainer container = GuideUtil.StorageDevice.OpenContainer("galaxy");
            string fullpath = Path.Combine(container.Path, "profiles.xml");
#else
            string fullpath = Path.Combine(StorageContainer.TitleLocation, filename);
#endif

            AccessMutex.WaitOne();

            FileStream stream = null;
            try
            {
                stream = File.Open(fullpath, FileMode.Open, FileAccess.Read);
                XmlSerializer serializer = new XmlSerializer(typeof(SSaveData));
                data = (SSaveData)serializer.Deserialize(stream);
                
                // TODO: cannot put this in a function because the out param must be assigned to
                // TODO: change SSaveData to a class
                // ---
                for (int index = 0; index < data.Profiles.Count; ++index)
                {
                    SProfile profile = data.Profiles[index];

                    if (profile.Version < SProfile.CurrentVersion)
                    {
                        // TODO: version migration
                        string name = data.Profiles[index].Name;
                        data.Profiles[index] = CreateDefaultProfile(name);
                    }

                    // NOTE: avoid old save data having empty scores
                    data.Profiles[index].Game[0].StageScores = data.Profiles[index].Game[0].StageScores ?? new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    data.Profiles[index].Game[0].StageMedals = data.Profiles[index].Game[0].StageMedals ?? new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    data.Profiles[index].Game[1].StageScores = data.Profiles[index].Game[1].StageScores ?? new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    data.Profiles[index].Game[1].StageMedals = data.Profiles[index].Game[1].StageMedals ?? new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                }
                // ---

            }
            catch (Exception exception)
            {
                data = DefaultSaveData;
                Console.WriteLine(exception.ToString());
            }
            finally
            {
                if (stream != null)
                    stream.Close();   

#if XBOX360
                container.Dispose();
#endif
            }

            AccessMutex.ReleaseMutex();
        }

        private static void Export(SSaveData data, string filename)
        {
            lock (AccessMutex)
            {
                ExportImpl(data, filename);
            }
        }

        private static void ExportImpl(SSaveData data, string filename)
        {
#if XBOX360
            if (GuideUtil.StorageDevice == null || GuideUtil.StorageDevice.IsConnected == false)
                return;

            StorageContainer container = GuideUtil.StorageDevice.OpenContainer("galaxy");
            string fullpath = Path.Combine(container.Path, "profiles.xml");
#else
            string fullpath = Path.Combine(StorageContainer.TitleLocation, filename);
#endif

            FileStream stream = File.Open(fullpath, FileMode.Create, FileAccess.Write);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SSaveData));
                serializer.Serialize(stream, data);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            finally
            {
                stream.Close();

#if XBOX360
                container.Dispose();
#endif
            }
        }
    }
}
