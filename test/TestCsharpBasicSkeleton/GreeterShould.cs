namespace Test.CsharpBasicSkeleton
{
    using src.CsharpBasicSkeleton;
    using Xunit;

    public class GreeterShould
    {
        [Fact]
        public void Greet_HelloMessage_Receives()
        {
            Assert.Equal("Hello Jhon", Greeter.Greet("Jhon"));
        }
    }
}