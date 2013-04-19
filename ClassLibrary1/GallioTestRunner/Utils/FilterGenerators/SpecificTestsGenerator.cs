using System.Collections.Generic;

namespace ClassLibrary1.GallioTestRunner.Utils.FilterGenerators
{
    public class SpecificTestsGenerator
    {
        private const string BaseTestFilter = "Member: ";
        private const string Div = ", ";
        private string _filter = "";
        private int _testCount;

        public SpecificTestsGenerator()
        {
            _filter += BaseTestFilter;
            _testCount = 0;
        }

        public SpecificTestsGenerator AddTests(IEnumerable<string> tests)
        {
            return AddTestsToFilter(tests);
        }

        public SpecificTestsGenerator AddTest(string test)
        {
            return AddSingleTest(test);
        }

        public string Filter
        {
            get { return _filter; }
        }

        private SpecificTestsGenerator AddTestsToFilter(IEnumerable<string> tests)
        {
            foreach (var test in tests)
            {
                AddSingleTest(test);
            }
            return this;
        }

        private SpecificTestsGenerator AddSingleTest(string test)
        {
            _filter += Divider + test;
            _testCount++;
            return this;
        }

        private string Divider
        {
            get { return (_testCount > 0) ? Div : ""; }
        }

    }
}
