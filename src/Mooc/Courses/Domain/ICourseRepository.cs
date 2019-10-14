namespace Mooc.Courses.Domain
{
    public interface ICourseRepository
    {
        void Save(Course course);
        Course Search(string id);
    }
}