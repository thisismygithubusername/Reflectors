using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Gallio.Framework.Utilities;
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

        public void RunTest(string assemblyLoacation)
        {
            RunIt(assemblyLoacation);
        }

        private void RunIt(string assemblyLoacation)
        {
            var launcher = GetInitializedTestLancher(GetInitSetup(), GetLogger());
            launcher.AddFilePattern(assemblyLoacation);
            launcher.Run();
        }

        private TestLauncher GetInitializedTestLancher(RuntimeSetup setup, ILogger logger)
        {
            RuntimeBootstrap.Initialize(setup, logger);
            return  new TestLauncher
            {
                Logger = logger,
                ProgressMonitorProvider = NullProgressMonitorProvider.Instance,
                RuntimeSetup = setup,
                TestProject = { TestRunnerFactoryName = StandardTestRunnerFactoryNames.Local }
            };
        }

        private RuntimeSetup GetInitSetup( )
        {
            var setup = new Gallio.Runtime.RuntimeSetup();
            setup.AddPluginDirectory(GallioDirectory);
            return setup;
        }


        private static ILogger GetLogger()
        {
            return (ILogger)new RichConsoleLogger(NativeConsole.Instance); 
        }

        private TestPackage TesPackage
        {
            get { return new TestPackage();}
        }

        private void DoRandomShit(Assembly assembly)
        {
            var runner = new SampleRunner();
            
            //ITestRunnerManager testRunnerManager = RuntimeAccessor.ServiceLocator.Resolve<ITestRunnerManager>();
            //ITestRunnerFactory testRunnerFactory = testRunnerManager.GetFactory(consolidatedTestProject.TestRunnerFactoryName);
            //ITestRunner runner = TestRunnerFactory.CreateTestRunner();
            //var result = new TestLauncherResult(new Report { TestPackage = new TestPackageData(TestProject.TestPackage) });
        }
        
    }
}
