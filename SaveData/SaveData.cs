//
// Profiles.cs
//

using System;
using System.Threading;
using System.IO;
using System.Xml;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;

namespace Galaxy
{
    [Serializable]
    public struct SProfile
    {
        public string Name;
        public string Ship;
        public string WeaponPrimaryType;
        public int WeaponPrimaryLevel;
        public string WeaponSecondaryType;
        public int WeaponSecondaryLevel;
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
                    CreateDefaultProfile(),
                }
            };
            SaveData = DefaultSaveData;
            AccessMutex = new Mutex(false, "CSaveData.AccessMutex");
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

        public static SProfile CreateDefaultProfile()
        {
            return new SProfile()
            {
                Name = "User",
                Ship = "Default",
                WeaponPrimaryType = "Laser",
                WeaponPrimaryLevel = 0,
                WeaponSecondaryType = "Empty",
                WeaponSecondaryLevel = 0,
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
            SProfile profile = CreateDefaultProfile();
            profile.Name = name;
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
            AccessMutex.WaitOne();

            string fullpath = Path.Combine(StorageContainer.TitleLocation, filename);
            FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate, FileAccess.Read);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SSaveData));
                data = (SSaveData)serializer.Deserialize(stream);
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
            string fullpath = Path.Combine(StorageContainer.TitleLocation, filename);
            FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate, FileAccess.Write);
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
