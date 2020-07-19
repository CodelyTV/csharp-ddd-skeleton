namespace CodelyTv.Backoffice.Courses.Domain
{
    using System;
    using System.Collections.Generic;

    public class BackofficeCourse
    {
        public string Id { get; }
        public string Name { get; }
        public string Duration { get; }

        public BackofficeCourse(string id, string name, string duration)
        {
            Id = id;
            Name = name;
            Duration = duration;
        }

        private BackofficeCourse()
        {
        }

        public Dictionary<string, string> ToPrimitives()
        {
            return new Dictionary<string, string>()
            {
                {"id", Id},
                {"name", Name},
                {"duration", Duration}
            };
        }

        public BackofficeCourse FromPrimitives(string aggregateId, Dictionary<string, string> body)
        {
            return new BackofficeCourse(aggregateId, body["name"], body["duration"]);
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;

            var item = obj as BackofficeCourse;
            if (item == null) return false;

            return Id.Equals(item.Id) && Name.Equals(item.Name) &&
                   Duration.Equals(item.Duration);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Duration);
        }
    }
}