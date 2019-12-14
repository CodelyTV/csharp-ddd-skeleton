namespace CodelyTv.Mooc.CoursesCounter.Application.Incrementer
{
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

        public void Increment(CourseId id)
        {
            CoursesCounter counter = repository.Search() ?? InitializeCounter();

            if (!counter.HasIncremented(id))
            {
                counter.Increment(id);

                repository.Save(counter);
            }
        }

        private CoursesCounter InitializeCounter()
        {
            return CoursesCounter.Initialize(uuidGenerator.Generate());
        }
    }
}