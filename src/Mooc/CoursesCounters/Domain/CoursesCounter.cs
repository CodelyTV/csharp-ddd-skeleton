using System;
using System.Collections.Generic;
using System.Linq;
using CodelyTv.Mooc.Courses.Domain;

namespace CodelyTv.Mooc.CoursesCounters.Domain
{
    public class CoursesCounter
    {
        public CoursesCounterId Id { get; }
        public CoursesCounterTotal Total { get; private set; }
        public List<CourseId> ExistingCourses { get; }

        public CoursesCounter(CoursesCounterId id, CoursesCounterTotal total, List<CourseId> existingCourses)
        {
            Id = id;
            Total = total;
            ExistingCourses = existingCourses;
        }

        private CoursesCounter()
        {
        }

        public static CoursesCounter Initialize(string id)
        {
            return new CoursesCounter(new CoursesCounterId(id), CoursesCounterTotal.Initialize(), new List<CourseId>());
        }

        public bool HasIncremented(CourseId id)
        {
            return ExistingCourses.Contains(id);
        }

        public void Increment(CourseId id)
        {
            Total = Total.Increment();
            ExistingCourses.Add(id);
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;

            var item = obj as CoursesCounter;
            if (item == null) return false;

            return Id.Equals(item.Id) &&
                   Total.Equals(item.Total) &&
                   ExistingCourses.SequenceEqual(item.ExistingCourses);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Total, ExistingCourses);
        }
    }
}
