namespace CodelyTv.Backoffice.Courses.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CodelyTv.Shared.Domain.FiltersByCriteria;

    public interface IBackofficeCourseRepository
    {
        Task Save(BackofficeCourse course);
        Task<IEnumerable<BackofficeCourse>> SearchAll();
        Task<IEnumerable<BackofficeCourse>> Matching(Criteria criteria);
    }
}