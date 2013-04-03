using System.Reflection;
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

        private object TestFixtureInstance { get; set; }

        public ITest Test { get; set; }

        public T TestEnvironment { get; set; } 

        public ITestRun Run()
        {
            return InvokeTest();
        }

        private ITestRun InvokeTest()
        {
            CreateTestFixtureInstance();
            TestEnvironment.Setup(TestFixtureInstance);
            TryRunTest();
            TestEnvironment.TearDown(TestFixtureInstance);
            return new TestRun(true);
        }

        private void TryRunTest()
        {
            BaseReflector.ExecuteInstanceMethodFromInstance(TestFixtureInstance, Test.TestName);
        }

        private void CreateTestFixtureInstance()
        {
            TestFixtureInstance = EnvironmentReflector.CreateTestFixtureInstance(EnvironmentAssembly, Test.FixtureName);
        }

        
    }
}
