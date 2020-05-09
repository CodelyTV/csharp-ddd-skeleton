namespace CodelyTv.Mooc.Courses.Application.Create
{
    using System.Threading.Tasks;
    using CodelyTv.Shared.Domain.Bus.Event;
    using Domain;

    public class CourseCreator
    {
        private readonly ICourseRepository _repository;
        private readonly IEventBus _eventBus;

        public CourseCreator(ICourseRepository repository, IEventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public async Task Create(CourseId id, CourseName name, CourseDuration duration)
        {
            Course course = Course.Create(id, name, duration);

            await this._repository.Save(course);
            await this._eventBus.Publish(course.PullDomainEvents());
        }
    }
}