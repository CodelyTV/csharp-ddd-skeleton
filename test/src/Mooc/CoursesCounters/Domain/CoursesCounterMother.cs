using System.Collections.Generic;
using CodelyTv.Mooc.Courses.Domain;
using CodelyTv.Mooc.CoursesCounters.Domain;
using CodelyTv.Test.Mooc.Courses.Domain;
using CodelyTv.Test.Shared.Domain;

namespace CodelyTv.Test.Mooc.CoursesCounters.Domain
{
    public static class CoursesCounterMother
    {
        public static CoursesCounter Create(CoursesCounterId id, CoursesCounterTotal total,
            List<CourseId> existingCourses)
        {
            return new CoursesCounter(id, total, existingCourses);
        }

        public static CoursesCounter WithOne(CourseId courseId)
        {
            return Create(CoursesCounterIdMother.Random(), CoursesCounterTotalMother.One(),
                ListMother<CourseId>.One(courseId));
        }

        public static CoursesCounter Incrementing(
            CoursesCounter existingCounter, CourseId courseId)
        {
            var existingCourses = new List<CourseId>(existingCounter.ExistingCourses);
            existingCourses.Add(courseId);

            return Create(
                existingCounter.Id,
                CoursesCounterTotalMother.Create(existingCounter.Total.Value + 1),
                existingCourses
            );
        }

        public static CoursesCounter Random()
        {
            var existingCourses = ListMother<CourseId>.Random(CourseIdMother.Random);

            return Create(
                CoursesCounterIdMother.Random(),
                CoursesCounterTotalMother.Create(existingCourses.Count),
                existingCourses
            );
        }
    }
}
