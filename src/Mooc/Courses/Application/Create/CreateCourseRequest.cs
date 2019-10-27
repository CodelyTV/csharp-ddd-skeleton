namespace CodelyTv.Mooc.Courses.Application.Create
{
    public class CreateCourseRequest
    {
        public string Id { get; }
        public string Name { get; }
        public string Duration { get; }

        public CreateCourseRequest(string id, string name, string duration)
        {
            Id = id;
            Name = name;
            Duration = duration;
        }
    }
}