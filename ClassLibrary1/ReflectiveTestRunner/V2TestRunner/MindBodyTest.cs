using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.ReflectiveTestRunner.TestModules.@abstract;

namespace ClassLibrary1.ReflectiveTestRunner.V2TestRunner
{
    class MindBodyTest : ITest
    {
        public string TestName { get; set; }
        public string FixtureName { get; set; }
    }
}
