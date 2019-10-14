namespace Mooc.Courses.Application.Create
{
    using Domain;

    public class CourseCreator
    {
        public ICourseRepository Repository { get; private set; }

        public CourseCreator(ICourseRepository repository)
        {
            Repository = repository;
        }

        public void Invoke(CreateCourseRequest request)
        {
            Course course = new Course(request.Id, request.Name, request.Duration);

            this.Repository.Save(course);
        }
    }
}