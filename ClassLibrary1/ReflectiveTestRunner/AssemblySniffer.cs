using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.ReflectiveTestRunner
{
    public class AssemblySniffer
    {
        public AssemblySniffer(string assemblyPath)
        {
            AssemblyPath = assemblyPath;
            GenerateAssemblyDirectoryPath();
            FindDllsInDirectory();
            //PrintAssemblies(AppDomain.CurrentDomain);
        }

        public AppDomain CurrentAppDomain
        {
            get { return AppDomain.CurrentDomain; }
        }

        public void Sniff()
        {
            FindDllsInDirectory();
        }

        public void LoadSniffedDlls()
        {
            Load();
        }

        public void UnloadDependencies()
        {
            Unload();
        }

        public List<string> FoundDependencies
        {
            get; set;
        }

        public string DirectoryPath
        {
            get; set;
        }

        public string MainAssemblyDll
        {
            get; set;
        }

        public Assembly CurrentDomainAssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly[] currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var resolutionPath = DirectoryPath;
            foreach (Assembly assembly in currentAssemblies)
            {
                if (assembly.FullName == args.Name)
                {
                    return assembly;
                }
            }

            return FindAssembliesInDirectory(args.Name, resolutionPath);
        }

        private void GenerateAssemblyDirectoryPath()
        {
            var spiltPath = AssemblyPath.Split('/');
            var path = "";

            for (int i = 0; i < spiltPath.Length - 1; i++)
            {
                path += spiltPath[i] + "/";
            }
            MainAssemblyDll = spiltPath[spiltPath.Count()-1];
            Console.WriteLine(path);
            Console.WriteLine(MainAssemblyDll);
            DirectoryPath = path;
        }

        private void Load()
        {
            foreach (var foundDependency in FoundDependencies)
            {
                Console.WriteLine(DirectoryPath + foundDependency);
                try
                {
                    var assembly = Assembly.LoadFrom(DirectoryPath + foundDependency);
                    AppDomain.CurrentDomain.Load(assembly.GetName());
                    AppDomain.CurrentDomain.ExecuteAssembly(assembly.FullName);
                }
                catch (Exception)
                {
                    Console.WriteLine("CouldNotLoad( " + foundDependency + " )");
                }
            }

            PrintAssemblies(CurrentAppDomain);
        }

        private void Unload()
        {
            
        }

        public void  FindDllsInDirectory()
        {
            var allFilenames = Directory.EnumerateFiles(DirectoryPath).Select(Path.GetFileName);
            FoundDependencies = allFilenames.Where(fn => Path.GetExtension(fn) == Dll).ToList();
            //foreach (var file in FoundDependencies) { Console.WriteLine(file); }
        }

        private static Assembly FindAssembliesInDirectory(string assemblyName, string directory)
        {
            foreach (string file in Directory.GetFiles(directory))
            {
                Assembly assembly;

                if (TryLoadAssemblyFromFile(file, assemblyName, out assembly))
                    return assembly;
            }

            return null;
        }

        private static bool TryLoadAssemblyFromFile(string file, string assemblyName, out Assembly assembly)
        {
            try
            {
                // Convert the filename into an absolute file name for 
                // use with LoadFile. 
                file = new FileInfo(file).FullName;

                if (AssemblyName.GetAssemblyName(file).FullName == assemblyName)
                {
                    assembly = Assembly.LoadFile(file);
                    return true;
                }
            }
            catch
            {
                /* Do Nothing */
            }
            assembly = null;
            return false;
        }

        private static void PrintAssemblies(AppDomain appDomain)
        {
            Console.WriteLine("*************************************");
            foreach (var assembly in appDomain.GetAssemblies())
            {
                Console.WriteLine(assembly.FullName);
                Console.WriteLine();
            }
        }

        private const string Dll = ".dll";
        private string AssemblyPath { get; set; }

    }
}
