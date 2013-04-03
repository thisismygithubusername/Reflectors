using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.ReflectiveTestRunner.TestModules;
using ClassLibrary1.V2TestRunner;

namespace ClassLibrary1.MindBodyTestRunners.APITestRunner
{
    public class ApiTestRunner
    {
        private string sada =
            "C:/Users/jorge.salcedo/Documents/GitHub/mbregressiontests/CSharp/APITest.Librarybin/Debug/APITest.Library.dll";
        public Assembly Assembly
        {
            get
            {
                return
                    Assembly.LoadFrom(
                        "APITest.Library.dll");
            } 
        }
           
        public void RunCheckScheduleItems()
        {
            var testRunner = new SimpleTestRunner<ApiEnvironment>(Assembly);
            var results = testRunner.AddTest(new MindBodyTest { TestName = "CheckScheduleItems", FixtureName = "AppointmentTests" }).RunTestsInQueue().TestsRan;
            foreach (var testRun in results)
            {
                Console.WriteLine(testRun.Status);
                Console.Write(testRun.Test.TestName);
                Console.WriteLine(testRun.TestOutput);
            }
        }
    }
}
