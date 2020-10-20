using System.Threading.Tasks;

namespace CodelyTv.Mooc.Courses.Domain
{
    public interface CourseRepository
    {
        Task Save(Course course);
        Task<Course> Search(CourseId id);
    }
}
