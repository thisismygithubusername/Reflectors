using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.ReflectiveTestRunner.GallioWrappers
{
    public class RunnerLoader
    {
        private Dictionary<string, List<string>> AllTestsToRun;
        
        public RunnerLoader()
        {
            AllTestsToRun = new Dictionary<string, List<string>>();    
        }

        public Dictionary<string, List<string>> DictOfTests
        {
            get { return AllTestsToRun; }
        }

        public void LoadTestToDictonary(string fixture, string test )
        {
            LoadSingleTest(fixture, test);
        }

        public void LoadTestsToDictonary(string fixture, List<string> tests)
        {
            LoadMultipleTests(fixture, tests);
        }

        private void LoadSingleTest(string fixture, string testName)
        {
            ContainsFixture(fixture);
            AddTest(fixture, testName);
        }

        private void LoadMultipleTests(string fixtureName, IEnumerable<string> tests )
        {
            ContainsFixture(fixtureName);
            foreach (string test in tests)
            {
                AddTest(fixtureName, test);
            }
        }

        private void AddTest(string fixtureName, string testName)
        {
            AllTestsToRun[fixtureName].Add(testName);
        }

        private void ContainsFixture(string fixtureName)
        {
            if(!AllTestsToRun.ContainsKey(fixtureName))
                CreateNewList(fixtureName);
        }

        private void CreateNewList(string fixtureName)
        {
            AllTestsToRun[fixtureName] = new List<string>();
        } 

    }
}
