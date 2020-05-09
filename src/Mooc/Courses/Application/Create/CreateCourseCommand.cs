namespace CodelyTv.Mooc.Courses.Application.Create
{
    using CodelyTv.Shared.Domain.Bus.Command;

    public class CreateCourseCommand : Command
    {
        public string Id { get; }
        public string Name { get; }
        public string Duration { get; }

        public CreateCourseCommand(string id, string name, string duration)
        {
            Id = id;
            Name = name;
            Duration = duration;
        }
    }
}