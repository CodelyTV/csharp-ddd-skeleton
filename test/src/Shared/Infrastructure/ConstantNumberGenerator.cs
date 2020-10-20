using CodelyTv.Shared.Domain;

namespace CodelyTv.Test.Shared.Infrastructure
{
    public class ConstantNumberGenerator : RandomNumberGenerator
    {
        public int Generate()
        {
            return 1;
        }
    }
}
