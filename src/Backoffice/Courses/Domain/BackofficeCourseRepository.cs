using System.Collections.Generic;
using System.Threading.Tasks;
using CodelyTv.Shared.Domain.FiltersByCriteria;

namespace CodelyTv.Backoffice.Courses.Domain
{
    public interface BackofficeCourseRepository
    {
        Task Save(BackofficeCourse course);
        Task<IEnumerable<BackofficeCourse>> SearchAll();
        Task<IEnumerable<BackofficeCourse>> Matching(Criteria criteria);
    }
}
