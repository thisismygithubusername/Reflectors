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
        private const string BaseTestFilter = "Member: ";
        private const string Div = ", ";

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

        private string FilterAllFixtureTests(string fixtureName)
        {
            return new FixtureFilterGenerator().AddFixture(fixtureName).Filter;
        }


        internal class FixtureFilterGenerator
        {
            private const string BaseFixtureFilter = "Type: ";
            private const string Div = ", ";
            private string filter = "";
            private readonly bool firstAdded;

            public FixtureFilterGenerator()
            {
                filter += BaseFixtureFilter;
                firstAdded = false;
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
                get { return firstAdded ? Div : ""; }
            }

        }

        internal class SpecificTestsInFixtureGenerator
        {
            
        }
    }
}
