using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;

namespace ClassLibrary1.ReflectiveTestRunner
{
    public class RuntimeCompiler
    {
        public RuntimeCompiler(AssemblySniffer assemblySnifer)
        {
            AssemblySniffer = assemblySnifer;
        }

        public CompilerResults CompileCode()
        {
            
            var compilerParams = new CompilerParameters
            {
                GenerateInMemory = false,
                TreatWarningsAsErrors = false,
                GenerateExecutable = true,
                CompilerOptions = option,
            };

            string[] references = AssemblySniffer.FoundDependencies.ToArray();
                
            compilerParams.ReferencedAssemblies.AddRange(references);
            var provider = new CSharpCodeProvider();
            return provider.CompileAssemblyFromSource(compilerParams, Options.Values.ToArray());
        }

        public string option
        {
            get { return "/optimize /lib:" + AssemblySniffer.DirectoryPath; }
        }

        public List<string> AssemReferences { get; set; } 
        public AssemblySniffer AssemblySniffer { get; set; }
        public string DllPath { get; set; }
        public Dictionary<string, string> Options
        {
            get { return new Dictionary<string, string> {{"CompilerVersion", "v4.5"}}; }
        }

        private string ReplaceOptionUrlWith()
        {
            Console.WriteLine("PATHXSDFD: " + AssemblySniffer.DirectoryPath);
            return option.Replace("{}", AssemblySniffer.DirectoryPath);
        }
    }
}
