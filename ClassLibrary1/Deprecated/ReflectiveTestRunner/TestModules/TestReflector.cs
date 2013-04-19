using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using ClassLibrary1.ReflectiveTestRunner.Reflectors;
using ClassLibrary1.ReflectiveTestRunner.TestModules.@abstract;
using ClassLibrary1.Reflectors;
using Gallio.Framework.Utilities;
using Gallio.Runtime;
using Gallio.Runtime.ConsoleSupport;
using Gallio.Runtime.Logging;

namespace ClassLibrary1.ReflectiveTestRunner.TestModules
{
    public class TestReflector<T> where T : ITestEnvironment, new() 
    {
        public TestReflector(ITest test, Assembly assembly, SimpleTestRunner<T> runner ) 
        {
            Test = test;
            EnvironmentAssembly = assembly;
            StdOutReference = Console.Out;
            TestOuputHijacker = new StringWriter();
            SetupHijacker = new StringWriter();
            TearDownHijacker = new StringWriter();
            Runner = runner;
            FixtureTypes = Runner.FixtureCache;
        }

        public SimpleTestRunner<T> Runner
        {
            get; set;
        } 

        public Assembly EnvironmentAssembly
        {
            get; set;
        }

        public ITest Test
        {
            get; set;
        }

        public T TestEnvironment
        {
            get; private set;
        }

        public TestRun Run()
        {
            return TryRunTest();
        }

        public void RunSomeTest()
        {
            const string GallioDirectory = @"C:\Program Files\Gallio\bin\";
            var runner = new SampleRunner();
            var setup = new Gallio.Runtime.RuntimeSetup();
            setup.AddPluginDirectory(GallioDirectory);
            runner.AddAssembly(EnvironmentAssembly);
            var logger = CreateLogger();
            RuntimeBootstrap.Initialize(setup, logger);
            runner.AddMethod(FixtureType, Test.TestName);
            runner.Run();
        }

        private ILogger CreateLogger()
        {
            return new RichConsoleLogger(NativeConsole.Instance);

        }
        private TestRun TryRunTest()
        {
            return RedirectOutputToSetupHijacker()
                .CreateTestFixtureInstance()
                .RunEnvironmentSetup()
                .LogSetupOutput()
                .RedirectOutputToTestHijacker()
                .InvokeTest()
                .LogTestOutput()
                .RedirectOutputToTearDownHijacker()
                .RunEnvironmentTearDown()
                .LogTearDownOutput()
                .RedirectOutputToConsole()
                .PackageTestRun();
        }
        private Type FixtureType
        {
            get {
                return FixtureTypes.ContainsKey(Test.FixtureName) ? FixtureTypes[Test.FixtureName] : GetTypeAndCache();
            }
        }

        private Type GetTypeAndCache()
        {
            FixtureTypes[Test.FixtureName] = EnvironmentReflector.GetFixtureType(Runner.TestDomain,EnvironmentAssembly, Test.FixtureName);
            return FixtureTypes[Test.FixtureName];
        }
        private TestReflector<T> RedirectOutputToTestHijacker()
        {
            Console.SetOut(TestOuputHijacker);
            return this;
        }

        private TestReflector<T> RedirectOutputToSetupHijacker()
        {
            Console.SetOut(SetupHijacker);
            return this;
        }

        private TestReflector<T> RedirectOutputToTearDownHijacker()
        {
            Console.SetOut(TearDownHijacker);
            return this;
        }

        private TestReflector<T> RedirectOutputToConsole()
        {
            TestOuputHijacker.Close();
            Console.SetOut(StdOutReference);
            return this;
        } 

        private TestReflector<T> CreateTestFixtureInstance()
        {
            TestFixtureInstance = EnvironmentReflector.CreateTestFixtureInstanceWithAppdomain(Runner.TestDomain,
                                                                                              FixtureType,
                                                                                              EnvironmentAssembly,
                                                                                              Test.FixtureName);
            return this;
        }

        private TestReflector<T> RunEnvironmentSetup()
        {
            TestEnvironment = new T();
            TestEnvironment.Setup(TestFixtureInstance);
            return this;
        }

        private TestReflector<T> RunEnvironmentTearDown()
        {
            TestEnvironment.TearDown(TestFixtureInstance);
            return this;
        }

        private TestReflector<T> LogSetupOutput()
        {
            StolenSetupOutput = SetupHijacker.ToString();
            TestOuputHijacker.Close();
            return this;
        }

        private TestReflector<T> LogTestOutput()
        {
            StolenTestOutput = TestOuputHijacker.ToString();
            TestOuputHijacker.Close();
            return this;
        } 

        private TestReflector<T> LogTearDownOutput()
        {
            StolenTearDownOutput = TearDownHijacker.ToString();
            TestOuputHijacker.Flush();
            return this;
        }

        private TestReflector<T> InvokeTest()
        {
            string status = Success;
            Exception failureException = null;
            try
            {
                BaseReflector.ExecuteInstanceMethodFromInstance(TestFixtureInstance, Test.TestName);
            }
            catch (Exception e)
            {
               failureException = e.InnerException;
               status = Failure;
            }
            PostTestInfo = new Tuple<string, Exception>(status, failureException);
            return this;
        }

        private TestRun PackageTestRun()
        {
            return new TestRun
                {
                    Test = this.Test,
                    Status = PostTestInfo.Item1,
                    TestOutput = StolenTestOutput,
                    SetupOutput = StolenSetupOutput,
                    TearDowmOutput = StolenTearDownOutput,
                    Exception = PostTestInfo.Item2
                };
        }

        private void LoadInstanceTypes()
        {
            //EnvironmentReflector.CreateTestFixtureInstanceWithAppdomain(AppDomain.CurrentDomain, EnvironmentAssembly, Test.FixtureName);
        }

        private Dictionary<string, Type> FixtureTypes { get; set; }  
        private object TestFixtureInstance { get; set; }
        private Tuple<string, Exception> PostTestInfo { get; set; }
        private string StolenTestOutput { get; set; }
        private string StolenSetupOutput { get; set; }
        private string StolenTearDownOutput { get; set; }
        private TextWriter StdOutReference { get; set; }
        private TextWriter TestOuputHijacker  { get; set;}
        private TextWriter SetupHijacker { get; set; }
        private TextWriter TearDownHijacker { get; set; }
        private const string Success = "success";
        private const string Failure = "failure";

    }
}
