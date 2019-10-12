namespace src.test.Shared.Infrastructure
{
    using src.Shared.Domain;

    public class ConstantNumberGenerator : IRandomNumberGenerator
    {
        public int Generate()
        {
            return 1;
        }
    }
}