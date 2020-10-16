using System;
using System.Collections.Generic;

namespace CodelyTv.Backoffice.Courses.Domain
{
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

        public Dictionary<string, object> ToPrimitives()
        {
            var primitives = new Dictionary<string, object>
            {
                {"id", Id},
                {"name", Name},
                {"duration", Duration}
            };

            return primitives;
        }

        public static BackofficeCourse FromPrimitives(Dictionary<string, object> body)
        {
            return new BackofficeCourse(Convert.ToString(body["id"]), Convert.ToString(body["name"]),
                Convert.ToString(body["duration"]));
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
