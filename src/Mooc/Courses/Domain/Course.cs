namespace CodelyTv.Mooc.Courses.Domain
{
    using System;
    using CodelyTv.Shared.Domain.Aggregate;
    using CodelyTv.Shared.Domain.Courses;

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

        public override bool Equals(object obj)
        {
            if (this == obj) return true;

            var item = obj as Course;
            if (item == null) return false;

            return this.Id.Equals(item.Id) && this.Name.Equals(item.Name) && this.Duration.Equals(item.Duration);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.Name, this.Duration);
        }
    }
}