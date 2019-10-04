namespace src.test.Shared.Infrastructure
{
    using src.Shared.Infrastructure;
    using Xunit;

    public class RandomNumberGeneratorMother
    {
        [Fact]
        public void Generate_WithoutParameter_IsInt()
        {
            var generator = new RandomNumberGenerator();

            Assert.IsType<int>(generator.Generate());
        }
    }
}