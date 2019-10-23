namespace CodelyTv.Mooc.Courses.Domain
{
    public interface ICourseRepository
    {
        void Save(Course course);
        Course Search(CourseId id);
    }
}