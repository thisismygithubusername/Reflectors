using System;
using System.IO;
using System.Reflection;
using System.Text;
using ClassLibrary1.ReflectiveTestRunner.Reflectors;
using ClassLibrary1.ReflectiveTestRunner.TestModules.@abstract;
using ClassLibrary1.Reflectors;

namespace ClassLibrary1.ReflectiveTestRunner.TestModules
{
    public class TestReflector<T> where T : ITestEnvironment, new() 
    {
        public TestReflector(ITest test, Assembly assembly) 
        {
            Test = test;
            EnvironmentAssembly = assembly;
            TestEnvironment = new T();
            StdOutReference = Console.Out;
            TestOuputHijacker = new StringWriter();
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

        private TestRun TryRunTest()
        {
            return CreateTestFixtureInstance()
                .RedirectOutputToHijacker()
                .RunEnvironmentSetup()
                .InvokeTest()
                .RunEnvironmentTearDown()
                .RedirectOutputToConsole()
                .PackageTestRun();
        }

        private TestReflector<T> RedirectOutputToHijacker()
        {
            Console.SetOut(TestOuputHijacker);
            return this;
        }

        private TestReflector<T> RedirectOutputToConsole()
        {
            HijackedTestOutput = TestOuputHijacker.ToString();
            TestOuputHijacker.Close();
            Console.SetOut(StdOutReference);
            return this;
        } 

        private TestReflector<T> CreateTestFixtureInstance()
        {
            TestFixtureInstance = EnvironmentReflector.CreateTestFixtureInstanceWithActivator(EnvironmentAssembly, Test.FixtureName);
            return this;
        }

        private TestReflector<T> RunEnvironmentSetup()
        {
            TestEnvironment.Setup(TestFixtureInstance);
            return this;
        }

        private TestReflector<T> RunEnvironmentTearDown()
        {
            TestEnvironment.TearDown(TestFixtureInstance);
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
               failureException = e;
               status = Failure;
            }
            PostTestInfo = new Tuple<string, Exception>(status, failureException);
            return this;
        }

        private TestRun PackageTestRun()
        {
            return new TestRun(Test, HijackedTestOutput, PostTestInfo);
        }

        private object TestFixtureInstance { get; set; }
        private Tuple<string, Exception> PostTestInfo { get; set; }
        private string HijackedTestOutput { get; set; }
        private TextWriter StdOutReference { get; set; }
        private TextWriter TestOuputHijacker  { get; set;}
        private const string Success = "success";
        private const string Failure = "failure";

    }
}
