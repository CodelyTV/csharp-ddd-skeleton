namespace CodelyTv.Mooc.Courses.Domain
{
    public class Course
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
    }
}