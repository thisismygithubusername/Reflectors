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
        }

        public Assembly EnvironmentAssembly { get; set; }

        public ITest Test { get; set; }

        public T TestEnvironment { get; set; } 

        public TestRun Run()
        {
            return TryRunTest();
        }

        private object TestFixtureInstance
        {
            get; set;
        }

        private TestRun TryRunTest()
        {
            CreateTestFixtureInstance();
            Tuple<string, Exception> testResults = null;
            var sb = new StringBuilder();

            using (var writer = new StringWriter(sb))
            {
                Console.SetOut(writer);
                TestEnvironment.Setup(TestFixtureInstance);
                testResults = InvokeTest();
                TestEnvironment.TearDown(TestFixtureInstance);
            }
            var output = sb.ToString();
            return new TestRun(Test, output, testResults);
        }

        private Tuple<string, Exception> InvokeTest()
        {
            string status = "success";
            Exception failureException = null;
            try
            {
                BaseReflector.ExecuteInstanceMethodFromInstance(TestFixtureInstance, Test.TestName);
            }
            catch (Exception e)
            {
                failureException = e;
                status = "failure";
            }
            return new Tuple<string, Exception>(status, failureException);
        }

        private void CreateTestFixtureInstance()
        {
            TestFixtureInstance = EnvironmentReflector.CreateTestFixtureInstance(EnvironmentAssembly, Test.FixtureName);
        }

        
    }
}
