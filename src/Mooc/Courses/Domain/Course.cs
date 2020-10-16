using System;
using CodelyTv.Shared.Domain.Aggregate;
using CodelyTv.Shared.Domain.Courses.Domain;

namespace CodelyTv.Mooc.Courses.Domain
{
    public class Course : AggregateRoot
    {
        public CourseId Id { get; }
        public CourseName Name { get; }
        public CourseDuration Duration { get; }

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
            var course = new Course(id, name, duration);

            course.Record(new CourseCreatedDomainEvent(id.Value, name.Value, duration.Value));

            return course;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;

            var item = obj as Course;
            if (item == null) return false;

            return Id.Equals(item.Id) && Name.Equals(item.Name) && Duration.Equals(item.Duration);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Duration);
        }
    }
}
