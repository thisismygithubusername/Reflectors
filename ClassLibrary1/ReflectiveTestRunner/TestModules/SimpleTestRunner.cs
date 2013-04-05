using System.Collections.Generic;
using System.Reflection;
using ClassLibrary1.ReflectiveTestRunner.TestModules.@abstract;
using ClassLibrary1.Reflectors;

namespace ClassLibrary1.ReflectiveTestRunner.TestModules
{
    public class SimpleTestRunner<T> where T : ITestEnvironment, new() 
    {
        public SimpleTestRunner(Assembly assembly)
        {
            CurrentAssembly = assembly;
        }

        public Queue<ITest> TestsToRun
        {
            get { return _testsToRun; }
        }

        public List<TestRun> TestsRan
        {
            get { return _testsRan; }
        }

        public Assembly CurrentAssembly
        {
            get; set;
        }


        public SimpleTestRunner<T> AddTest(ITest test)
        {
            AddTestToQueue(test);
            return this;
        }

        public SimpleTestRunner<T> AddTests(IEnumerable<ITest> tests)
        {
            AddTestsToQueue(tests);
            return this;
        }

        public SimpleTestRunner<T> RunTestsInQueue()
        {
            RunAllTests();
            return this;
        }

        public SimpleTestRunner<T> RunTest(ITest test)
        {
            RunSingleTest(test);
            return this;
        }

        private void RunSingleTest(ITest test)
        {
            var testRun = new TestReflector<T>(test, CurrentAssembly).Run();
            TestsRan.Add(testRun);
        }

        private void RunAllTests()
        {
            while (TestsToRun.Count > 0)
            {
                TestsRan.Add(new TestReflector<T>(_testsToRun.Dequeue(), CurrentAssembly).Run()); 
            }
        }

        private void AddTestToQueue(ITest test)
        {
            TestsToRun.Enqueue(test);
        }

        private void AddTestsToQueue(IEnumerable<ITest> tests)
        {
            foreach (var test in tests)
            {
                TestsToRun.Enqueue(test);
            }
        }

        private readonly Queue<ITest> _testsToRun = new Queue<ITest>();
        private readonly List<TestRun> _testsRan = new List<TestRun>();

    }
}
