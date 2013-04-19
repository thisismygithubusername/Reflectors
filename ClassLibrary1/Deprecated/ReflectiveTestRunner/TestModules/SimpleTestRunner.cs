using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ClassLibrary1.GallioTestRunner.Runners;
using ClassLibrary1.ReflectiveTestRunner.TestModules.@abstract;
using ClassLibrary1.Reflectors;
using Gallio.Runtime.ConsoleSupport;
using Gallio.Runtime.Logging;
using Gallio.Runtime.UtilityCommands;
using Microsoft.CSharp;

namespace ClassLibrary1.ReflectiveTestRunner.TestModules
{
    public class SimpleTestRunner<T> where T : ITestEnvironment, new() 
    {

        public Dictionary<string, Type> FixtureCache { get { return fixtureTypeCache; } }
        private Dictionary<string, Type> fixtureTypeCache = new Dictionary<string, Type>();
        private AssemblySniffer AssemblySniffer { get; set; }
        private readonly Queue<ITest> _testsToRun = new Queue<ITest>();
        private readonly List<TestRun> _testsRan = new List<TestRun>();

        public SimpleTestRunner(string path)
        {
            DirectoryPath = path;
            GeneratateAssembly();
            InitAssemblySniffer();
            //SetAssemblyResolve(path);
            //CompileDependencies();
        }

        public void RunAllTest(string testName)
        {
            ExecuteAllTests( testName);
        }

        public Queue<ITest> TestsToRun
        {
            get { return _testsToRun; }
        }

        public List<TestRun> TestsRan
        {
            get { return _testsRan; }
        }

        public Assembly CurrentAssembly
        {
            get; set;
        }

        public string DirectoryPath
        {
            get; set;
        }

        public AppDomain TestDomain 
        { 
            get; set; 
        }

        public SimpleTestRunner<T> AddTest(ITest test)
        {
            AddTestToQueue(test);
            return this;
        }

        public SimpleTestRunner<T> AddTests(IEnumerable<ITest> tests)
        {
            AddTestsToQueue(tests);
            return this;
        }

        public SimpleTestRunner<T> RunTestsInQueue()
        {
            RunAllTests();
            return this;
        }

        public SimpleTestRunner<T> RunTest(ITest test)
        {
            RunSingleTest(test);
            return this;
        }

        public SimpleTestRunner<T> RunGallioTest(ITest test)
        {
            RunSomeGallioTest(test);
            return this;
        }

        private static void SetAssemblyResolve(string path)
        {
            //AssemblySniffer.LoadSniffedDlls();
            AppDomain.CurrentDomain.AssemblyResolve += new AssemblySniffer(path).CurrentDomainAssemblyResolve;
        }

        private void RunSingleTest(ITest test)
        {
            var testRun = new TestReflector<T>(test, CurrentAssembly, this).Run();
            TestsRan.Add(testRun);
        }

        private void RunSomeGallioTest(ITest test)
        {
            new TestReflector<T>(test, CurrentAssembly, this).RunSomeTest();
        }

        private void RunAllTests()
        {
            while (TestsToRun.Count > 0)
            {
                TestsRan.Add(new TestReflector<T>(_testsToRun.Dequeue(), CurrentAssembly, this).Run()); 
            }
        }

        private void InitAssemblySniffer()
        {
            AssemblySniffer = new AssemblySniffer(DirectoryPath);
            TestDomain = AppDomain.CreateDomain(AssemblySniffer.MainAssemblyName + "TestDomain");
            TestDomain.AssemblyResolve += new AssemblySniffer(DirectoryPath).CurrentDomainAssemblyResolve;
        }

        private ILogger CreateLogger()
        {
            return new RichConsoleLogger(NativeConsole.Instance);
            
        }

        private void AddTestToQueue(ITest test)
        {
            TestsToRun.Enqueue(test);
        }

        private void ExecuteAllTests(string testName)
        {
            new SimpleGallioRunner(DirectoryPath).RunSingleTest(testName);
        }

        private void AddTestsToQueue(IEnumerable<ITest> tests)
        {
            foreach (var test in tests)
            {
                TestsToRun.Enqueue(test);
            }
            
        }
        private void CompileDependencies()
        {
            var results = new RuntimeCompiler(AssemblySniffer) { DllPath = DirectoryPath }.CompileCode();
            var assemb = results.Output;
            Console.WriteLine(assemb);
        }

        private void GeneratateAssembly()
        {
            ProxyDomain pd = new ProxyDomain();
            CurrentAssembly = pd.GetAssembly(DirectoryPath);
            //CurrentAssembly = Assembly.LoadFile(DirectoryPath);
        }

        public class ProxyDomain : MarshalByRefObject
        {
            public Assembly GetAssembly(string assemblyPath)
            {
                try
                {
                    return Assembly.LoadFrom(assemblyPath);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.ToString());
                }
            }
        }

    }
}
