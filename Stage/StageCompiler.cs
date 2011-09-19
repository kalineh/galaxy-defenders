//
// StageCompiler.cs
//

using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using Microsoft.CSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    // TODO: this is all fail
    // TODO: * need to load assemblies from references, or run in existing application 'space' somehow
    // TODO: * runtime compiler seems to not support collection initializers (is C# 2.0?)
    // TODO: xml? :(

    [Obsolete("because it is made of fail")]
    public class CStageCompiler
    {
        private static Assembly BuildAssembly(params string[] filenames)
        {
            Microsoft.CSharp.CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerResults results = provider.CompileAssemblyFromFile(
                new CompilerParameters() {
                    ReferencedAssemblies = { 
                        Assembly.GetAssembly(typeof(CEntity)).GetName().EscapedCodeBase.Substring("File:///".Length),
                        Assembly.GetAssembly(typeof(Vector2)).GetName().EscapedCodeBase.Substring("File:///".Length),
                    },
                    GenerateExecutable = false,
                    GenerateInMemory = true,
                },
                filenames);

            if (results.Errors.Count > 0)
            {
                foreach (CompilerError error in results.Errors)
                {
                    Console.WriteLine(String.Format("code:{0}: {1}", error.Line, error.ErrorText));
                }

                //throw new CompilationFailedException(); // XNA4
                throw new Exception("Compilation failed");
            }

            return results.CompiledAssembly;
        }

        public static CStageDefinition LoadStage(string name)
        {
            string cwd = Directory.GetCurrentDirectory();
            string base_ = cwd.Substring(0, cwd.LastIndexOf("\\bin\\"));
            if (cwd.Contains("StageEditor"))
                base_ = cwd.Substring(0, cwd.LastIndexOf("\\StageEditor\\bin\\"));
            string stage_filename = base_ + "\\StageDefinitions\\" + name + ".cs";

            Assembly assembly = BuildAssembly(stage_filename);
            Type stage_type = assembly.GetTypes()[0];
            MethodInfo generate_method = stage_type.GetMethod("GenerateDefinition");
            CStageDefinition result = generate_method.Invoke(null, null) as CStageDefinition;
            return result;
        }
    }
}
