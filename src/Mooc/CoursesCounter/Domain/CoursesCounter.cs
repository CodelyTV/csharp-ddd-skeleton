namespace CodelyTv.Mooc.CoursesCounter.Domain
{
    using System;
    using System.Collections.Generic;
    using Courses.Domain;

    public class CoursesCounter
    {
        public CoursesCounterId Id { get; private set; }
        public CoursesCounterTotal Total { get; private set; }
        public List<CourseId> ExistingCourses { get; private set; }

        public CoursesCounter(CoursesCounterId id, CoursesCounterTotal total, List<CourseId> existingCourses)
        {
            Id = id;
            Total = total;
            ExistingCourses = existingCourses;
        }

        public static CoursesCounter Initialize(string id)
        {
            return new CoursesCounter(new CoursesCounterId(id), CoursesCounterTotal.Initialize(), new List<CourseId>());
        }

        public bool HasIncremented(CourseId id)
        {
            return this.ExistingCourses.Contains(id);
        }

        public void Increment(CourseId id)
        {
            this.Total = this.Total.Increment();
            this.ExistingCourses.Add(id);
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;

            var item = obj as CoursesCounter;
            if (item == null) return false;

            return this.Id.Equals(item.Id) &&
                   this.Total.Equals(item.Total) &&
                   this.ExistingCourses.Equals(item.ExistingCourses);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.Total, this.ExistingCourses);
        }
    }
}