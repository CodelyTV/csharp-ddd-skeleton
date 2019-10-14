namespace Mooc.Courses.Domain
{
    public class Course
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Duration { get; private set; }

        public Course(string id, string name, string duration)
        {
            Id = id;
            Name = name;
            Duration = duration;
        }
    }
}