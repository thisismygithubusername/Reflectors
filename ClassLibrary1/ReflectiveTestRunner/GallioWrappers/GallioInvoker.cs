﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.ReflectiveTestRunner.GallioWrappers;
using Gallio.Framework.Utilities;
using Gallio.Model;
using Gallio.Model.Filters;
using Gallio.Model.Isolation;
using Gallio.Model.Schema;
using Gallio.Runner;
using Gallio.Runner.Projects;
using Gallio.Runner.Projects.Schema;
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
            get { return new GallioTestRunner("");}
        }

    }

    public class GallioTestRunner
    {
        private const string GallioDirectory = @"C:\Program Files\Gallio\bin\";


        public GallioTestRunner(string assemblyLocation)
        {
            AssemblyLocation = assemblyLocation;
            InitRunner();
        }

        public RuntimeSetup Setup
        {
            get; set;
        }

        public string AssemblyLocation
        {
            get; private set;
        }

        public TestLauncher Launcher
        {
            get; private set;
        }

        public ILogger Logger
        {
            get; set;
        }

        public void RunTest()
        {
            RunIt();
        }
        
        public void RunSingleTest( string testName)
        {
            RunDat(testName);
        }

        private void RunIt()
        {
            //Launcher.AddFilePattern(AssemblyLocation);
            Launcher.Run();
        }

        private void RunDat(string testName)
        {
            AddTestFilter(testName);
            Launcher.Run();
            Launcher.TestProject.ClearTestFilters();
            //AddTestFilter("AppointmentRatesAndBookingTimesTest");
            //Launcher.Run();
        }

        private void AddTestFilter(string testName)
        {
            var filters = FilterUtils.ParseTestFilterSet(
                new GallioFilterGenerator().GenerateSpecificTestsFilter(new List<string> { testName, "AppointmentRatesAndBookingTimesTest"}));
            Launcher.TestExecutionOptions.FilterSet = filters;
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

        private TestLauncher GetInitializedTestLancher()
        {
            return new TestLauncher
            {
                Logger = Logger,
                ProgressMonitorProvider = NullProgressMonitorProvider.Instance,
                RuntimeSetup = Setup,
                TestProject = { TestRunnerFactoryName = StandardTestRunnerFactoryNames.Local }
            };
        }

        private void InitializeRuntimeBootStrap()
        {
            RuntimeBootstrap.Initialize(Setup, Logger);
        }

        private void InitRunner()
        {
            Setup = GetInitSetup();
            Logger = GetLogger();
            Launcher = GetInitializedTestLancher();
            Launcher.AddFilePattern(AssemblyLocation);
            InitializeRuntimeBootStrap();
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
