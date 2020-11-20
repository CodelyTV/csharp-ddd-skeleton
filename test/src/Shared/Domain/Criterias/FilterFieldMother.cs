using CodelyTv.Shared.Domain.FiltersByCriteria;

namespace CodelyTv.Test.Shared.Domain.Criterias
{
    public static class FilterFieldMother
    {
        public static FilterField Create(string fieldName)
        {
            return new FilterField(fieldName);
        }

        public static FilterField Random()
        {
            return new FilterField(WordMother.Random());
        }
    }
}
