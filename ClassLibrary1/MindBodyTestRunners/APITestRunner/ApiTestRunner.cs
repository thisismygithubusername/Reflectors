using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using APITest.Library.APITests.Tests;
using ClassLibrary1.ReflectiveTestRunner.Reflectors;
using ClassLibrary1.ReflectiveTestRunner.TestModules;
using ClassLibrary1.ReflectiveTestRunner.TestModules.@abstract;

namespace ClassLibrary1.MindBodyTestRunners.APITestRunner
{
    public class ApiTestRunner
    {
        private string sada =
            @"C:/Users/jorge.salcedo/Documents/GitHub/mbregressiontests/CSharp/APITest.Library/bin/Debug/APITest.Library.dll";

        private string path =
            @"C:/Users/jorge.salcedo/Documents/GitHub/mbregressiontests/CSharp/APITest.Library/bin/Debug/APITest.Library.dll";

        private string V1path =
            @"C:/Users/jorge.salcedo/Documents/GitHub/mbregressiontests/CSharp/Regression.Tests/bin/Debug/Regression.Tests.dll";

        public Assembly ApiAssembly
        {
            get
            {
                return
                    Assembly.LoadFile(path);
            }
        }

        public Assembly V1Assembly
        {
            get
            {
                return
                    Assembly.LoadFile(V1path);
            }
        }

        public Assembly GetAnAssembly()
        {
            Type typeblah = typeof (AppointmentTests);
            Console.WriteLine(typeblah.FullName);
            var assembly = typeblah.Assembly;
            return assembly;
        }

        public void RunCheckScheduleItems()
        {
            var testRunner = new SimpleTestRunner<ApiEnvironment>(ApiAssembly);
            var results = testRunner.AddTest(new MindBodyTest { TestName = "CheckScheduleItems", FixtureName = "APITest.Library.APITests.Tests.AppointmentTests" }).RunTestsInQueue().TestsRan;
            foreach (var testRun in results)
            {
                Console.WriteLine(testRun.Status);
                Console.Write(testRun.Test.TestName);
                Console.WriteLine(testRun.TestOutput);
                if (testRun.Exception != null)
                {
                    Console.WriteLine(testRun.Exception.Data);
                    Console.WriteLine(testRun.Exception.Message);
                    Console.WriteLine(testRun.Exception.StackTrace);
                }
            }
        }

        public void RunAPITestAndV1Test()
        {
            var apiTestRunner = new SimpleTestRunner<ApiEnvironment>(ApiAssembly);
            var v1TestRunner = new SimpleTestRunner<V1Environment>(V1Assembly);

            var apiresults = apiTestRunner.AddTest(new MindBodyTest { TestName = "CheckScheduleItems", FixtureName = "APITest.Library.APITests.Tests.AppointmentTests" }).RunTestsInQueue().TestsRan;
            var v1results = v1TestRunner.AddTest(new MindBodyTest { TestName = "TestDateControls", FixtureName = "Regression.Tests.Coverage.BusinessMode.ClassesTests" }).RunTestsInQueue().TestsRan;

            foreach (var testRun in v1results)
            {
                Console.WriteLine(testRun.Status);
                Console.Write(testRun.Test.TestName);
                Console.WriteLine(testRun.TestOutput);
                if (testRun.Exception != null)
                {
                    Console.WriteLine(testRun.Exception.Data);
                    Console.WriteLine(testRun.Exception.Message);
                    Console.WriteLine(testRun.Exception.StackTrace);
                }
            }

            foreach (var testRun in apiresults)
            {
                Console.WriteLine(testRun.Status);
                Console.Write(testRun.Test.TestName);
                Console.WriteLine(testRun.TestOutput);
                if (testRun.Exception != null)
                {
                    Console.WriteLine(testRun.Exception.Data);
                    Console.WriteLine(testRun.Exception.Message);
                    Console.WriteLine(testRun.Exception.StackTrace);
                }
            }
            
        }
        internal class V1Environment : ITestEnvironment
        {
            public void Setup(object tf)
            {
                
            }

            public void TearDown(object tf)
            {
                //
            }

        }
    }
}
