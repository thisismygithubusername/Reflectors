using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.ReflectiveTestRunner;
using ClassLibrary1.ReflectiveTestRunner.TestModules.@abstract;

namespace ClassLibrary1.Reflectors
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

    }
}
