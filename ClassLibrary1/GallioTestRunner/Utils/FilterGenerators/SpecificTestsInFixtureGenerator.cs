using System.Collections.Generic;

namespace ClassLibrary1.GallioTestRunner.Utils.FilterGenerators
{
    //Todo 
    public class SpecificTestsInFixtureGenerator
    {
        private const string BaseTestFilter = "Member: ";
        private const string Div = ", ";
        private readonly string _filter = "";
        private int _testCount;

        public SpecificTestsInFixtureGenerator(string fixture)
        {
            _filter += new FixtureFilterGenerator().AddFixture(fixture).Filter;
            _testCount = 0;
        }

        public void AddTests(IEnumerable<string> tests)
        {
            AddTestsToFilter(tests);
        }

        public string Filter
        {
            get { return _filter; }
        }

        //Todo
        private void AddTestsToFilter(IEnumerable<string> tests)
        {
            foreach (var test in tests)
            {
               // test.GetEnumerator();
            }
        }

    }
}
