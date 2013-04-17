using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.ReflectiveTestRunner.Reflectors;
using ClassLibrary1.ReflectiveTestRunner.TestModules;
using ClassLibrary1.ReflectiveTestRunner.TestModules.@abstract;
using ClassLibrary1.Reflectors;

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

        public  void RunAllTests(string testName)
        {
            var testRunner = new SimpleTestRunner<ApiEnvironment>(V1path);
            testRunner.RunAllTest(testName);
        }

        public void RunCheckScheduleItems()
        {
            var testRunner = new SimpleTestRunner<ApiEnvironment>(path);
            var results =
                testRunner.AddTest(new MindBodyTest
                    {
                        TestName = "CheckScheduleItems",
                        FixtureName = "APITest.Library.APITests.Tests.AppointmentTests"
                    }).RunTestsInQueue().TestsRan;

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
            var apiTestRunner = new SimpleTestRunner<ApiEnvironment>(path);
            var v1TestRunner = new SimpleTestRunner<V1Environment>(V1path);

            var apiresults =
                apiTestRunner.AddTest(new MindBodyTest
                    {
                        TestName = "CheckScheduleItems",
                        FixtureName = "APITest.Library.APITests.Tests.AppointmentTests"
                    }).RunTestsInQueue().TestsRan;

            var v1results =
                v1TestRunner.AddTest(new MindBodyTest
                    {
                        TestName = "TestDateControls",
                        FixtureName = "Regression.Tests.Coverage.BusinessMode.ClassesTests"
                    }).RunTestsInQueue().TestsRan;

            DumpTestRun(v1results, apiresults);
        }

        private void DumpTestRun(IEnumerable<TestRun> v1results, IEnumerable<TestRun> apiresults  )
        {
            DumpTests(v1results);
            DumpTests(apiresults);
        }

        private void DumpTests(IEnumerable<TestRun> tests)
        {
            foreach (var testRun in tests)
            {
                Console.WriteLine(testRun);
            }
        }

        internal class V1Environment : ITestEnvironment
        {
            public void Setup(object tf)
            {
                ExecuteFixtureSetup(tf);
                var metaData = tf.GetType().MetadataToken;
                var type = tf.GetType();
                //type.Module.ModuleHandle.ResolveFieldHandle(metaData);
                Console.WriteLine(metaData);
                FindTestSetup(tf);
                ExecuteTestSetup(tf);
            }

            public void TearDown(object tf)
            {
                FindTestTearDown(tf);
                ExecuteTestTearDown(tf);
            }

            private void FindTestSetup(object fixture)
            {
                TestSetupInfo = EnvironmentReflector.GetMethodWithAttributeFromObject(fixture, SetupAttributeName);
            } 

            private void ExecuteTestSetup(object fixture)
            {
                EnvironmentReflector.TryToExecuteSetupMethod(fixture, TestSetupInfo);
            }

            private void FindTestTearDown(object fixture)
            {
                TestTearDownInfo = EnvironmentReflector.GetMethodWithAttributeFromObject(fixture, TearDownAttributeName);
            }

            private void ExecuteFixtureSetup(object fixture)
            {
                FixtureSetupInfo = EnvironmentReflector.GetMethodWithAttributeFromObject(fixture, FixtureSetup);
                try
                {
                    EnvironmentReflector.ExecuteMethodFromMethodInfo(fixture, FixtureSetupInfo);
                }
                catch (Exception)
                {
                    Console.WriteLine("FailedToExecuteFixtureSetup");
                }
            } 
            
            private void ExecuteTestTearDown(object fixture)
            {
                EnvironmentReflector.TryToExecuteTearDown(fixture, TestTearDownInfo);
            }
            
            private MethodInfo TestSetupInfo { get; set; }
            private MethodInfo TestTearDownInfo { get; set; }
            private MethodInfo FixtureSetupInfo { get; set; }
            private const string SetupAttributeName = "TestInitialize";
            private const string TearDownAttributeName = "TearDown";
            private const string FixtureSetup = "FixtureSetUp";
        }
    }
}
