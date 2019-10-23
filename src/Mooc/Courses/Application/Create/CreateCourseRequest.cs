namespace CodelyTv.Mooc.Courses.Application.Create
{
    public class CreateCourseRequest
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Duration { get; private set; }

        public CreateCourseRequest(string id, string name, string duration)
        {
            Id = id;
            Name = name;
            Duration = duration;
        }
    }
}