namespace CodelyTv.Mooc.CoursesCounter.Application.Incrementer
{
    using System.Threading.Tasks;
    using CodelyTv.Shared.Domain;
    using Courses.Domain;
    using Domain;

    public class CoursesCounterIncrementer
    {
        private ICoursesCounterRepository repository;
        private IUuidGenerator uuidGenerator;

        public CoursesCounterIncrementer(ICoursesCounterRepository repository, IUuidGenerator uuidGenerator)
        {
            this.repository = repository;
            this.uuidGenerator = uuidGenerator;
        }

        public async Task Increment(CourseId id)
        {
            CoursesCounter counter = await repository.Search() ?? InitializeCounter();

            if (!counter.HasIncremented(id))
            {
                counter.Increment(id);

                await repository.Save(counter);
            }
        }

        private CoursesCounter InitializeCounter()
        {
            return CoursesCounter.Initialize(uuidGenerator.Generate());
        }
    }
}