namespace CodelyTv.Mooc.Courses.Domain
{
    using CodelyTv.Shared.Domain.Aggregate;

    public class Course : AggregateRoot
    {
        public CourseId Id { get; private set; }
        public CourseName Name { get; private set; }
        public CourseDuration Duration { get; private set; }

        public Course(CourseId id, CourseName name, CourseDuration duration)
        {
            Id = id;
            Name = name;
            Duration = duration;
        }

        private Course()
        {
        }

        public static Course Create(CourseId id, CourseName name, CourseDuration duration)
        {
            Course course = new Course(id, name, duration);

            course.Record(new CourseCreatedDomainEvent(id.Value, name.Value, duration.Value));

            return course;
        }
    }
}