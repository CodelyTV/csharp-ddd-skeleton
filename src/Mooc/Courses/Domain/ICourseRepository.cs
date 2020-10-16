using System.Threading.Tasks;

namespace CodelyTv.Mooc.Courses.Domain
{
    public interface ICourseRepository
    {
        Task Save(Course course);
        Task<Course> Search(CourseId id);
    }
}
