using CodelyTv.Shared.Domain.FiltersByCriteria;

namespace CodelyTv.Test.Shared.Domain.Criterias
{
    public static class FilterValueMother
    {
        public static FilterValue Create(string value)
        {
            return new FilterValue(value);
        }

        public static FilterValue Random()
        {
            return Create(WordMother.Random());
        }
    }
}
