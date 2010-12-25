//
// StageDefinitionSerializable.cs
//

using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using System.ComponentModel;

namespace Galaxy
{
    [Serializable]
    public class CStageDefinitionSerializable
    {
        public struct SStageElements
        {
            public int time;
            public List<CStageElement> data;
        }

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public float ScrollSpeed { get; set; }
        public string BackgroundSceneryName { get; set; }
        public string ForegroundSceneryName { get; set; }
        public string MusicName { get; set; }
        public List<SStageElements> Elements { get; set; }

        public CStageDefinitionSerializable()
        {
            Elements = new List<SStageElements>();    
        }

        public void ConvertFromDefinition(CStageDefinition definition)
        {
            Name = definition.Name;
            DisplayName = definition.Name;
            ScrollSpeed = definition.ScrollSpeed;
            BackgroundSceneryName = definition.BackgroundSceneryName;
            ForegroundSceneryName = definition.ForegroundSceneryName;
            MusicName = definition.MusicName;
            Elements = new List<SStageElements>();

            foreach (KeyValuePair<int, List<CStageElement>> kv in definition.Elements)
            {
                // TODO: copy kv.Value list to seperate list
                Elements.Add(new SStageElements() { time = kv.Key, data = kv.Value });
            }
        }

        public CStageDefinition ConvertToDefinition()
        {
            CStageDefinition definition = new CStageDefinition(Name);
            definition.Name = Name;
            definition.DisplayName = Name;
            definition.ScrollSpeed = ScrollSpeed;
            definition.BackgroundSceneryName = BackgroundSceneryName;
            definition.ForegroundSceneryName = ForegroundSceneryName;
            definition.MusicName = MusicName;
            foreach (SStageElements elements in Elements)
            {
                definition.Elements[elements.time] = elements.data;    
            }
            return definition;
        }
    }
}


