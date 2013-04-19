using System.Collections.Generic;

namespace ClassLibrary1.GallioTestRunner.Utils.FilterGenerators
{

    public class GallioFilterGenerator
    {
        public const string FilterUrl = "http://www.gallio.org/wiki/doku.php?id=tools:gallio_test_selection_filters";

        public string GenerateFixtureFilter(string fixtureName)
        {
            return FilterAllFixtureTests(fixtureName);
        }

        public string GenerateSpecificTestsFilter(List<string> lists )
        {
            return new SpecificTestsGenerator().AddTests(lists).Filter;
        }

        private string FilterAllFixtureTests(string fixtureName)
        {
            return new FixtureFilterGenerator().AddFixture(fixtureName).Filter;
        }
    }
}
