namespace ClassLibrary1.ReflectiveTestRunner.TestModules.@abstract
{
    public interface ITestEnvironment
    {
        void Setup(object testFixture);
        void TearDown(object testFixture);
    }
}
