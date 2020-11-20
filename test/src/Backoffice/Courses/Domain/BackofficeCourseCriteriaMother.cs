using System.Collections.Generic;
using CodelyTv.Shared.Domain.FiltersByCriteria;
using CodelyTv.Test.Shared.Domain.Criterias;

namespace CodelyTv.Test.Backoffice.Courses.Domain
{
    public static class BackofficeCourseCriteriaMother
    {
        public static Criteria NameContains(string text)
        {
            return CriteriaMother.Create(
                FiltersMother.CreateOne(
                    FilterMother.FromValues(new Dictionary<string, string>
                    {
                        {"field", "name"},
                        {"operator", "CONTAINS"},
                        {"value", text}
                    })
                ), Order.None()
            );
        }
    }
}
