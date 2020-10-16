using CodelyTv.Mooc.Courses.Application.Create;
using CodelyTv.Mooc.Courses.Domain;

namespace CodelyTv.Test.Mooc.Courses.Domain
{
    public class CourseMother
    {
        public static Course Create(CourseId id, CourseName name, CourseDuration duration)
        {
            return new Course(id, name, duration);
        }

        public static Course FromRequest(CreateCourseCommand request)
        {
            return Create(CourseIdMother.Create(request.Id), CourseNameMother.Create(request.Name),
                CourseDurationMother.Create(request.Duration));
        }

        public static Course Random()
        {
            return Create(CourseIdMother.Random(), CourseNameMother.Random(), CourseDurationMother.Random());
        }
    }
}
