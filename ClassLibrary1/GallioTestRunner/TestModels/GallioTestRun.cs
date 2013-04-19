using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallio.Runner;

namespace ClassLibrary1.GallioTestRunner.TestModels
{
    public class GallioTestRun
    {
        public GallioTestRun(TestLauncherResult result, DateTime time)
        {
            Result = result;
            StartTime = time;
        }

        public DateTime StartTime { get; set; }
        public TestLauncherResult Result { get; set; }

    }
}
