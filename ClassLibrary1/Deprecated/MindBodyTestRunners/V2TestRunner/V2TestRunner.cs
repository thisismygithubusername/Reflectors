using System.Reflection;

namespace ClassLibrary1.Deprecated.MindBodyTestRunners.V2TestRunner
{
    public class V2TestRunner
    {
        public void RunATest()
        {
            var test = new MindBodyTest
                {
                    TestName = "SomeTEST",
                    FixtureName = ""
                };

            Assembly assembly = Assembly.GetCallingAssembly();
            //var testRunner = new SimpleTestRunner<V2Environment>(assembly);
            //testRunner.AddTest(test).RunTestsInQueue();
        }
    }
}
