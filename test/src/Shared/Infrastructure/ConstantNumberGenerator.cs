namespace SharedTest.src.Infrastructure
{
    using Shared.Domain;

    public class ConstantNumberGenerator : IRandomNumberGenerator
    {
        public int Generate()
        {
            return 1;
        }
    }
}