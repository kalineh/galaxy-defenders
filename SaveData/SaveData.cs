//
// Profiles.cs
//

using System;
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

        static CSaveData()
        {
            DefaultSaveData = new SSaveData() {
                CurrentProfile = "User",
                Profiles = new List<SProfile>() {
                    new SProfile() {
                        Name = "User",
                        Ship = "Default",
                        WeaponPrimaryType = "Laser",
                        WeaponPrimaryLevel = 0,
                        WeaponSecondaryType = "Empty",
                        WeaponSecondaryLevel = 0,
                    },
                }
            };
            SaveData = DefaultSaveData;
        }

        public static void Load()
        {
            CSaveData.Import(out SaveData, "profiles.xml");
        }

        public static void Save()
        {
            CSaveData.Export(SaveData, "profiles.xml");
        }

        public static void CreateDefaultProfileIfNecessary()
        {
            Load();
            Save();
            Load();
        }

        public static SProfile GetProfile(string user)
        {
            return SaveData.Profiles.Where(profile => profile.Name == user).First();
        }

        public static SProfile GetCurrentProfile()
        {
            return SaveData.Profiles.Where(profile => profile.Name == SaveData.CurrentProfile).First();
        }

        private static void Import(out SSaveData data, string filename)
        {
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
