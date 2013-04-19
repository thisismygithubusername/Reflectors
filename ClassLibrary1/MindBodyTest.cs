using ClassLibrary1.ReflectiveTestRunner.TestModules.@abstract;

namespace ClassLibrary1
{
    class MindBodyTest : ITest
    {
        public MindBodyTest(string testName, string fixtureName)
        {
            TestName = testName;
            FixtureName = fixtureName;
        }

        public MindBodyTest(string testName)
        {
            TestName = testName;
        }

        public MindBodyTest()
        {

        }

        public string TestName { get; set; }
        public string FixtureName { get; set; }
    }
}
