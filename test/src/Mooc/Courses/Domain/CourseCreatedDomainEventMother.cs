namespace CodelyTv.Test.Mooc.Courses.Domain
{
    using CodelyTv.Mooc.Courses.Domain;

    public class CourseCreatedDomainEventMother
    {
        public static CourseCreatedDomainEvent Create(CourseId id, CourseName name, CourseDuration duration)
        {
            return new CourseCreatedDomainEvent(id.Value, name.Value, duration.Value);
        }

        public static CourseCreatedDomainEvent FromCourse(Course course)
        {
            return Create(course.Id, course.Name, course.Duration);
        }

        public static CourseCreatedDomainEvent Random()
        {
            return Create(CourseIdMother.Random(), CourseNameMother.Random(), CourseDurationMother.Random());
        }
    }
}