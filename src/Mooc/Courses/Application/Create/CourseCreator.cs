using System.Threading.Tasks;
using CodelyTv.Mooc.Courses.Domain;
using CodelyTv.Shared.Domain.Bus.Event;

namespace CodelyTv.Mooc.Courses.Application.Create
{
    public class CourseCreator
    {
        private readonly EventBus _eventBus;
        private readonly CourseRepository _repository;

        public CourseCreator(CourseRepository repository, EventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public async Task Create(CourseId id, CourseName name, CourseDuration duration)
        {
            var course = Course.Create(id, name, duration);

            await _repository.Save(course);
            await _eventBus.Publish(course.PullDomainEvents());
        }
    }
}
