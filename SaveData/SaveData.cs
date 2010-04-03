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
    public struct SProfile
    {
        public static int CurrentVersion = 1;
        public int Version;
        public string Name;
        public int Money;
        public string CurrentStage;
        public string ChassisType;
        public string GeneratorType;
        public string ShieldType;
        public string WeaponPrimaryType;
        public int WeaponPrimaryLevel;
        public string WeaponSecondaryType;
        public int WeaponSecondaryLevel;
        public string WeaponSidekickLeftType;
        public int WeaponSidekickLeftLevel;
        public string WeaponSidekickRightType;
        public int WeaponSidekickRightLevel;
    }

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

        static CSaveData()
        {
            DefaultSaveData = new SSaveData() {
                CurrentProfile = "User",
                Profiles = new List<SProfile>() {
                    CreateDefaultProfile("User"),
                }
            };
            SaveData = DefaultSaveData;
#if XBOX360
            AccessMutex = new Mutex(false);
#else
            AccessMutex = new Mutex(false, "CSaveData.AccessMutex");
#endif
        }

        public static void Load()
        {
            CSaveData.Import(out SaveData, "profiles.xml");
        }

        public static void Save()
        {
            CSaveData.Export(SaveData, "profiles.xml");
        }

        // TODO: think of a better name for this (CreateProfilesXMLIfItDoesntExistAlready)
        public static void VerifyProfilesExist()
        {
            string fullpath = Path.Combine(StorageContainer.TitleLocation, "profiles.xml");

            bool exists = File.Exists(fullpath);
            if (!exists)
            {
                // save default data
                Save();
            }

            Load();
        }

        public static SProfile CreateDefaultProfile(string name)
        {
            return new SProfile()
            {
                Version = SProfile.CurrentVersion,
                Name = name,
                Money = 3000,
                CurrentStage = "Start",
                ChassisType = "BasicShip",
                GeneratorType = "BasicGenerator",
                ShieldType = "BasicShield",
                WeaponPrimaryType = "FrontLaser",
                WeaponPrimaryLevel = 0,
                WeaponSecondaryType = "",
                WeaponSecondaryLevel = 0,
                WeaponSidekickLeftType = "",
                WeaponSidekickLeftLevel = 0,
                WeaponSidekickRightType = "",
                WeaponSidekickRightLevel = 0
            };
        }

        public static SProfile GetProfile(string name)
        {
            return SaveData.Profiles.Where(profile => profile.Name == name).First();
        }

        public static SProfile GetCurrentProfile()
        {
            return SaveData.Profiles.Where(profile => profile.Name == SaveData.CurrentProfile).First();
        }

        public static void AddNewProfile(string name)
        {
            SProfile profile = CreateDefaultProfile(name);
            SaveData.Profiles.RemoveAll(existing => existing.Name == name);
            SaveData.Profiles.Add(profile);
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
            data = new SSaveData();
            data.Profiles = new List<SProfile>() { CreateDefaultProfile("Default") };
            data.CurrentProfile = "Default";
            return;
#endif

            AccessMutex.WaitOne();

            string fullpath = Path.Combine(StorageContainer.TitleLocation, filename);
            FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate, FileAccess.Read);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SSaveData));
                data = (SSaveData)serializer.Deserialize(stream);
                
                // TODO: cannot put this in a function because the out param must be assigned to
                // TODO: change SSaveData to a class
                // ---
                foreach (int index in Enumerable.Range(0, data.Profiles.Count))
                {
                    SProfile profile = data.Profiles[index];

                    if (profile.Version < SProfile.CurrentVersion)
                    {
                        // TODO: version migration
                        string name = data.Profiles[index].Name;
                        data.Profiles[index] = CreateDefaultProfile(name);
                    }
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
                stream.Close();
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
            return;
#endif

            string fullpath = Path.Combine(StorageContainer.TitleLocation, filename);
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
            }
        }
    }
}
