using System.Threading.Tasks;
using CodelyTv.Shared.Domain.Bus.Query;

namespace CodelyTv.Mooc.CoursesCounters.Application.Find
{
    public class FindCoursesCounterQueryHandler : QueryHandler<FindCoursesCounterQuery, CoursesCounterResponse>
    {
        private readonly CoursesCounterFinder _finder;

        public FindCoursesCounterQueryHandler(CoursesCounterFinder finder)
        {
            _finder = finder;
        }

        public async Task<CoursesCounterResponse> Handle(FindCoursesCounterQuery query)
        {
            return await _finder.Find();
        }
    }
}
