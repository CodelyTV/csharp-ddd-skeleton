using System.Threading.Tasks;
using CodelyTv.Mooc.Courses.Domain;
using CodelyTv.Mooc.CoursesCounters.Domain;
using CodelyTv.Shared.Domain;

namespace CodelyTv.Mooc.CoursesCounters.Application.Incrementer
{
    public class CoursesCounterIncrementer
    {
        private readonly ICoursesCounterRepository _repository;
        private readonly IUuidGenerator _uuidGenerator;

        public CoursesCounterIncrementer(ICoursesCounterRepository repository, IUuidGenerator uuidGenerator)
        {
            this._repository = repository;
            this._uuidGenerator = uuidGenerator;
        }

        public async Task Increment(CourseId id)
        {
            var counter = await _repository.Search() ?? InitializeCounter();

            if (!counter.HasIncremented(id))
            {
                counter.Increment(id);

                await _repository.Save(counter);
            }
        }

        private CoursesCounter InitializeCounter()
        {
            return CoursesCounter.Initialize(_uuidGenerator.Generate());
        }
    }
}
