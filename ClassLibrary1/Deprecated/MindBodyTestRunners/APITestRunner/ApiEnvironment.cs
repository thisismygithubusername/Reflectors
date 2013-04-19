using ClassLibrary1.ReflectiveTestRunner.TestModules.@abstract;

namespace ClassLibrary1.Deprecated.MindBodyTestRunners.APITestRunner
{
    public class ApiEnvironment : ITestEnvironment 
    {
        public void Setup(object tf)
        {
            //
        }

        public void TearDown(object tf)
        {
            //
        }

        public object FixtureInstance { get; set; }
    }
}
