using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Gallio.Model;
using Gallio.Model.Isolation;
using Gallio.Model.Schema;
using Gallio.Runner;
using Gallio.Runner.Projects;
using Gallio.Runner.Reports.Schema;
using Gallio.Runtime;
using Gallio.Runtime.ConsoleSupport;
using Gallio.Runtime.Debugging;
using Gallio.Runtime.Extensibility;
using Gallio.Runtime.Hosting;
using Gallio.Runtime.Logging;
using Gallio.Runtime.ProgressMonitoring;

namespace ClassLibrary1.ReflectiveTestRunner
{
    public class GallioInvoker
    {
        public GallioInvoker()
        {
            
        }

        public GallioTestRunner GallioRunner
        {
            get { return new GallioTestRunner();}
        }

    }

    public class GallioTestRunner
    {
        private const string GallioDirectory = @"C:\Program Files\Gallio\bin\";

        public static void RunTest(string assemblyLoacation)
        {
            ILogger logger = (ILogger) new RichConsoleLogger(NativeConsole.Instance);
            
            var setup = new Gallio.Runtime.RuntimeSetup();
            setup.AddPluginDirectory(GallioDirectory);
            RuntimeBootstrap.Initialize(setup, logger);

            var launcher = new TestLauncher
                {
                    Logger = logger,
                    ProgressMonitorProvider = NullProgressMonitorProvider.Instance,
                    RuntimeSetup = setup,
                    TestProject = {TestRunnerFactoryName = StandardTestRunnerFactoryNames.Local}
                };
            launcher.AddFilePattern(assemblyLoacation);
            launcher.Run();
            //ITestRunnerManager testRunnerManager = RuntimeAccessor.ServiceLocator.Resolve<ITestRunnerManager>();
            //ITestRunnerFactory testRunnerFactory = testRunnerManager.GetFactory(consolidatedTestProject.TestRunnerFactoryName);
           // ITestRunner runner = TestRunnerFactory.CreateTestRunner();
            //var result = new TestLauncherResult(new Report { TestPackage = new TestPackageData(TestProject.TestPackage) });
        }

        
    }
}
