namespace CodelyTv.Test.Mooc.CoursesCounter.Domain
{
    using System.Collections.Generic;
    using CodelyTv.Mooc.Courses.Domain;
    using CodelyTv.Mooc.CoursesCounter.Domain;
    using Courses.Domain;
    using Test.Shared.Domain;

    public static class CoursesCounterMother
    {
        public static CoursesCounter Create(CoursesCounterId id, CoursesCounterTotal total, List<CourseId> existingCourses)
        {
            return new CoursesCounter(id, total, existingCourses);
        }

        public static CoursesCounter WithOne(CourseId courseId)
        {
            return Create(CoursesCounterIdMother.Random(), CoursesCounterTotalMother.One(), ListMother<CourseId>.One(courseId));
        }

        public static CoursesCounter Incrementing(CoursesCounter existingCounter, CourseId courseId)
        {
            List<CourseId> existingCourses = new List<CourseId>(existingCounter.ExistingCourses);
            existingCourses.Add(courseId);

            return Create(
                existingCounter.Id,
                CoursesCounterTotalMother.Create(existingCounter.Total.Value + 1),
                existingCourses
            );
        }

        public static CoursesCounter Random()
        {
            List<CourseId> existingCourses = ListMother<CourseId>.Random(CourseIdMother.Random);

            return Create(
                CoursesCounterIdMother.Random(),
                CoursesCounterTotalMother.Create(existingCourses.Count),
                existingCourses
            );
        }
    }
}