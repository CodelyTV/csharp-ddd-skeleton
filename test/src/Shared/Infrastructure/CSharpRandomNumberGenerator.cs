namespace SharedTest.src.Infrastructure
{
    using Shared.Infrastructure;
    using Xunit;

    public class CSharpRandomNumberGeneratorMother
    {
        [Fact]
        public void Generate_WithoutParameter_IsInt()
        {
            var generator = new CSharpRandomNumberGenerator();

            Assert.IsType<int>(generator.Generate());
        }
    }
}