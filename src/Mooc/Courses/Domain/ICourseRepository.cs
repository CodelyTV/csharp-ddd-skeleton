namespace CodelyTv.Mooc.Courses.Domain
{
    using System.Threading.Tasks;

    public interface ICourseRepository
    {
        Task Save(Course course);
        Task<Course> Search(CourseId id);
    }
}