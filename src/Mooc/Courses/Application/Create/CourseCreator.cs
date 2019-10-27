namespace CodelyTv.Mooc.Courses.Application.Create
{
    using Domain;

    public class CourseCreator
    {
        private readonly ICourseRepository _repository;

        public CourseCreator(ICourseRepository repository)
        {
            _repository = repository;
        }

        public void Invoke(CreateCourseRequest request)
        {
            var id = new CourseId(request.Id);
            var name = new CourseName(request.Name);
            var duration = new CourseDuration(request.Duration);

            Course course = new Course(id, name, duration);

            this._repository.Save(course);
        }
    }
}