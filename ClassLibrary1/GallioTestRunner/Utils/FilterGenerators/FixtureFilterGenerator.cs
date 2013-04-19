namespace ClassLibrary1.GallioTestRunner.Utils.FilterGenerators
{
    public class FixtureFilterGenerator
    {
        private const string BaseFixtureFilter = "Type: ";
        private const string Div = ", ";
        private string _filter = "";
        private readonly int _fixtureCount;

        public FixtureFilterGenerator()
        {
            _filter += BaseFixtureFilter;
            _fixtureCount = 0;
        }

        public string Filter
        {
            get { return _filter; }
        }

        public FixtureFilterGenerator AddFixture(string fixtureName)
        {
            return Add(fixtureName);
        }

        private FixtureFilterGenerator Add(string fixtureName)
        {
            _filter += Divider + fixtureName;

            return this;
        }

        private string Divider
        {
            get { return (_fixtureCount > 0) ? Div : ""; }
        }

    }
}
