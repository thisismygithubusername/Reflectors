using System.Collections.Generic;
using System.Reflection;
using ClassLibrary1.ReflectiveTestRunner.TestModules.@abstract;
using ClassLibrary1.Reflectors;

namespace ClassLibrary1.ReflectiveTestRunner.TestModules
{
    public class SimpleTestRunner<T> where T : ITestEnvironment, new()
    {
        SimpleTestRunner(Assembly assembly)
        {
            CurrentAssembly = assembly;
        } 

        public Assembly CurrentAssembly { get; set; }

        public SimpleTestRunner(){}
 
        public void AddTest(ITest test)
        {
            AddTestToQueue(test);
        }

        public void AddTests(IEnumerable<ITest> tests)
        {
            AddTestsToQueue(tests);
        }

        public void RunTestsInQueue()
        {
            RunAllTests();
        }

        public void RunTest(ITest test)
        {
            RunSingleTest(test);
        }

        private void RunSingleTest(ITest test)
        {
            var testRun = new TestReflector<T>(test, CurrentAssembly).Run();
            _TestsRan.Add(testRun);
        }

        private void RunAllTests()
        {
            foreach (var test in _TestsToRun)
            {
               _TestsRan.Add(new TestReflector<T>(_TestsToRun.Dequeue(), CurrentAssembly).Run()); 
            }
        }

        private void AddTestToQueue(ITest test)
        {
            _TestsToRun.Enqueue(test);
        }

        private void AddTestsToQueue(IEnumerable<ITest> tests)
        {
            foreach (var test in tests)
            {
                _TestsToRun.Enqueue(test);
            }
        }

        public Queue<ITest> TestsToRun { get { return _TestsToRun; } }

        private  Queue<ITest> _TestsToRun = new Queue<ITest>();
        private List<ITestRun> _TestsRan = new List<ITestRun>();

    }
}
