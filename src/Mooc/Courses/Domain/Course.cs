namespace CodelyTv.Mooc.Courses.Domain
{
    public class Course
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
    }
}