using System;
using ClassLibrary1.ReflectiveTestRunner.TestModules.@abstract;

namespace ClassLibrary1.ReflectiveTestRunner.TestModules
{
    public class TestRun 
    {
        public TestRun(ITest test, string testOutput, Tuple<string, Exception> result)
        {
            Test = test;
            Status = result.Item1;
            Exception = result.Item2;
            TestOutput = testOutput;
        }

        public ITest Test { get; private set; }
        public Exception Exception { get; private set; }
        public string Status { get; private set; }
        public string TestOutput { get; private set; }
    }
}
