using System.Reflection;
using ClassLibrary1.ReflectiveTestRunner.TestModules;
using ClassLibrary1.Reflectors;

namespace ClassLibrary1.MindBodyTestRunners.V2TestRunner
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
