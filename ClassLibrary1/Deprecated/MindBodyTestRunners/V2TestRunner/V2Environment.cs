using System;
using ClassLibrary1.ReflectiveTestRunner.TestModules.@abstract;

namespace ClassLibrary1.Deprecated.MindBodyTestRunners.V2TestRunner
{
    /*
     * Create an Environment class that runs
     * setup / teardown 
     */
    public class V2Environment : ITestEnvironment
    {
        //Use BaseReflector to get intacne find test setup and test tear down atrributes. 

        public void Setup(object fixtureinstance)
        {
           // throw new NotImplementedException();
            Console.Write("BLAH");
        }

        public void TearDown(object fixtureInstance)
        {
            //throw new NotImplementedException();
            Console.Write("BLAH");
        }

        public object FixtureInstance { get; set; }
    }
}
