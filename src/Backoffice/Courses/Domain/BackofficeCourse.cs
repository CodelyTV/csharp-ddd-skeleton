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
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Duration = duration ?? throw new ArgumentNullException(nameof(duration));
        }

        private BackofficeCourse()
        {
            Id = string.Empty;
            Name = string.Empty;
            Duration = string.Empty;
        }

        public Dictionary<string, object> ToPrimitives()
        {
            return new Dictionary<string, object>
            {
                { "id", Id },
                { "name", Name },
                { "duration", Duration }
            };
        }

        public static BackofficeCourse FromPrimitives(Dictionary<string, object> body)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            return new BackofficeCourse(
                body["id"]?.ToString() ?? throw new InvalidOperationException("ID is missing"),
                body["name"]?.ToString() ?? throw new InvalidOperationException("Name is missing"),
                body["duration"]?.ToString() ?? throw new InvalidOperationException("Duration is missing")
            );
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;

            var item = obj as BackofficeCourse;
            if (item == null) return false;

            return Id.Equals(item.Id) && Name.Equals(item.Name) && Duration.Equals(item.Duration);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Duration);
        }
    }
}
