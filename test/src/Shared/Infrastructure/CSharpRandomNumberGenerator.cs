namespace src.test.Shared.Infrastructure
{
    using src.Shared.Infrastructure;
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