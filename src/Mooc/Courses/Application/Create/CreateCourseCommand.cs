using CodelyTv.Shared.Domain.Bus.Command;

namespace CodelyTv.Mooc.Courses.Application.Create
{
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
