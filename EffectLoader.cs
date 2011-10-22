//
// ShaderReload.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


#if XBOX360

namespace Galaxy
{
    public class CEffectLoader
    {
        public static Effect Load(GraphicsDevice device, string shader_name)
        {
            return null;
        }
    }
}

#else

using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

namespace Galaxy
{
    public class CEffectLoader
    {
        public static Effect Load(GraphicsDevice device, string shader_name)
        {
            try
            {
                string filename = String.Format("../../../Content/Effects/{0}.fx", shader_name);

                string contents = System.IO.File.ReadAllText(filename);
                EffectContent effect_content = new EffectContent()
                {
                    Identity = new ContentIdentity() { SourceFilename = filename },
                    EffectCode = contents,
                };

                EffectProcessor processor = new EffectProcessor();

                //processor.Defines = "DEBUG;TEXTURES=2";
                processor.DebugMode = EffectProcessorDebugMode.Debug;

                EffectLoaderInternal.CProcessorContext context = new EffectLoaderInternal.CProcessorContext();
                CompiledEffectContent compiled_effect_content = processor.Process(effect_content, context);

                byte[] effect_code = compiled_effect_content.GetEffectCode();
                Effect result = new Effect(device, effect_code);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("CEffectLoader.Load(): error: {0}", e.ToString());
            }

            return null;
        }
    }

    namespace EffectLoaderInternal
    {
        public class CNullLogger
            : ContentBuildLogger
        {
            public override void LogMessage(string message, params object[] messageArgs) { }
            public override void LogImportantMessage(string message, params object[] messageArgs) { }
            public override void LogWarning(string helpLink, ContentIdentity contentIdentity, string message, params object[] messageArgs) { }
        }

        class CImporterContext
            : ContentImporterContext
        {
            public override string IntermediateDirectory { get { return string.Empty; } }
            public override string OutputDirectory { get { return string.Empty; } }

            public override ContentBuildLogger Logger { get { return logger; } }
            ContentBuildLogger logger = new CNullLogger();

            public override void AddDependency(string filename) { }
        }

        class CProcessorContext
            : ContentProcessorContext
        {
            public override TargetPlatform TargetPlatform { get { return TargetPlatform.Windows; } }
            public override GraphicsProfile TargetProfile { get { return GraphicsProfile.HiDef; } }
            public override string BuildConfiguration { get { return string.Empty; } }
            public override string IntermediateDirectory { get { return string.Empty; } }
            public override string OutputDirectory { get { return string.Empty; } }
            public override string OutputFilename { get { return string.Empty; } }

            public override OpaqueDataDictionary Parameters { get { return parameters; } }
            OpaqueDataDictionary parameters = new OpaqueDataDictionary();

            public override ContentBuildLogger Logger { get { return logger; } }
            ContentBuildLogger logger = new CNullLogger();

            public override void AddDependency(string filename) { }
            public override void AddOutputFile(string filename) { }

            public override TOutput Convert<TInput, TOutput>(TInput input, string processorName, OpaqueDataDictionary processorParameters) { throw new NotImplementedException(); }
            public override TOutput BuildAndLoadAsset<TInput, TOutput>(ExternalReference<TInput> sourceAsset, string processorName, OpaqueDataDictionary processorParameters, string importerName) { throw new NotImplementedException(); }
            public override ExternalReference<TOutput> BuildAsset<TInput, TOutput>(ExternalReference<TInput> sourceAsset, string processorName, OpaqueDataDictionary processorParameters, string importerName, string assetName) { throw new NotImplementedException(); }
        }
    }
}

#endif