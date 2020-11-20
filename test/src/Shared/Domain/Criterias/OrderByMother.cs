using CodelyTv.Shared.Domain.FiltersByCriteria;

namespace CodelyTv.Test.Shared.Domain.Criterias
{
    public static class OrderByMother
    {
        public static OrderBy Create(string fieldName = null)
        {
            return new OrderBy(fieldName ?? WordMother.Random());
        }

        public static OrderBy Random()
        {
            return Create(WordMother.Random());
        }
    }
}
