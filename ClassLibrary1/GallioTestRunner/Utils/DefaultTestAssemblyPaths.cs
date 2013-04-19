using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.GallioTestRunner.Utils
{
    public static class DefaultTestAssemblyPaths
    {
        private static readonly string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public static readonly string ApiTestPath = MyDocumentsPath + "/GitHub/mbregressiontests/CSharp/APITest.Library/bin/Debug/APITest.Library.dll";

        public static readonly string RegressionV1TestsPath = MyDocumentsPath + "/GitHub/mbregressiontests/CSharp/Regression.Tests/bin/Debug/Regression.Tests.dll";

    }
}
