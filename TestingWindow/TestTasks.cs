using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.GallioTestRunner.Runners;
using ClassLibrary1.GallioTestRunner.Utils;

namespace TestingWindow
{
    public static class TestTasks
    {
        public static void RunTwoV1Tests()
        {
            var v1Runner = new SimpleGallioRunner(DefaultTestAssemblyPaths.RegressionV1TestsPath);
            var results = v1Runner.LoadTests(new List<string> { "TestDateControls", "EditProgram" }).RunLoadedTests().TestRuns.First();
        }
    }
}
