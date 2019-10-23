namespace CodelyTv.Test.Shared.Infrastructure
{
    using CodelyTv.Shared.Domain;

    public class ConstantNumberGenerator : IRandomNumberGenerator
    {
        public int Generate()
        {
            return 1;
        }
    }
}