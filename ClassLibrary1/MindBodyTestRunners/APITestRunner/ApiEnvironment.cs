using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.ReflectiveTestRunner.TestModules.@abstract;

namespace ClassLibrary1.MindBodyTestRunners.APITestRunner
{
    public class ApiEnvironment : ITestEnvironment 
    {
        public void Setup(object tf)
        {
            //
        }

        public void TearDown(object tf)
        {
            //
        }

        public object FixtureInstance { get; set; }
    }
}
