using CodelyTv.Shared.Domain;

namespace CodelyTv.Test.Shared.Infrastructure
{
    public class ConstantNumberGenerator : IRandomNumberGenerator
    {
        public int Generate()
        {
            return 1;
        }
    }
}
