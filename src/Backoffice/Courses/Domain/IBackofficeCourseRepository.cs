namespace CodelyTv.Backoffice.Courses.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBackofficeCourseRepository
    {
        Task Save(BackofficeCourse course);
        Task<IEnumerable<BackofficeCourse>> SearchAll();
    }
}