namespace Mooc.Courses.Application
{
    using Domain;

    public class CourseCreator
    {
        public ICourseRepository Repository { get; private set; }

        public CourseCreator(ICourseRepository repository)
        {
            Repository = repository;
        }

        public void Invoke(string id, string name, string duration)
        {
            Course course = new Course(id, name, duration);

            this.Repository.Save(course);
        }
    }
}