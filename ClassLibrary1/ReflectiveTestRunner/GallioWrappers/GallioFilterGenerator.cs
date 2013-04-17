using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.ReflectiveTestRunner.GallioWrappers
{
    public class GallioFilterGenerator
    {
        public const string FilterURL = "http://www.gallio.org/wiki/doku.php?id=tools:gallio_test_selection_filters";

        public string GenerateRunAllTestsFilter()
        {
            return FilterAllTests();
        }

        public string GenerateFixtureFilter(string fixtureName)
        {
            return FilterAllFixtureTests(fixtureName);
        }

        private string FilterAllTests()
        {
            return "*";
        }

        public string GenerateSpecificTestsFilter(List<string> lists )
        {
            return new SpecificTestsGenerator().AddTests(lists).Filter;
        }

        private string FilterAllFixtureTests(string fixtureName)
        {
            return new FixtureFilterGenerator().AddFixture(fixtureName).Filter;
        }


        internal class FixtureFilterGenerator
        {
            private const string BaseFixtureFilter = "Type: ";
            private const string Div = ", ";
            private string filter = "";
            private readonly int fixtureCount;

            public FixtureFilterGenerator()
            {
                filter += BaseFixtureFilter;
                fixtureCount = 0;
            }

            public string Filter
            {
                get { return filter; }
            }

            public FixtureFilterGenerator AddFixture(string fixtureName)
            {
                return Add(fixtureName);
            }

            private FixtureFilterGenerator Add(string fixtureName)
            {
                filter += Divider + fixtureName;

                return this;
            }

            private string Divider
            {
                get { return (fixtureCount > 0) ? Div : ""; }
            }

        }

        internal class SpecificTestsInFixtureGenerator 
        {
            private const string BaseTestFilter = "Member: ";
            private const string Div = ", ";
            private string filter = "";
            private int testCount;

            public SpecificTestsInFixtureGenerator(string fixture)
            {
                filter += new FixtureFilterGenerator().AddFixture(fixture).Filter;
                testCount = 0;
            }

            public void AddTests( IEnumerable<string> tests)
            {
                AddTestsToFilter(tests);
            }

            public string Filter
            {
                get { return filter; }
            }

            //Todo
            private void AddTestsToFilter(IEnumerable<string> tests)
            {
                foreach (var test in tests)
                {
                    test.GetEnumerator();
                }
            } 

        }

        internal class SpecificTestsGenerator 
        {
            private const string BaseTestFilter = "Member: ";
            private const string Div = ", ";
            private string filter = "";
            private int testCount;

            public SpecificTestsGenerator()
            {
                filter += BaseTestFilter;
                testCount = 0;
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
                get { return filter; }
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
                filter += Divider + test;
                testCount++;
                return this;
            }

            private string Divider
            {
                get { return (testCount > 0) ? Div : ""; }
            }

        }

    }
}
