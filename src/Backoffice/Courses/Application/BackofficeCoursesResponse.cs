using System.Collections.Generic;

namespace CodelyTv.Backoffice.Courses.Application
{
    public class BackofficeCoursesResponse
    {
        public readonly IEnumerable<BackofficeCourseResponse> Courses;

        public BackofficeCoursesResponse(IEnumerable<BackofficeCourseResponse> courses)
        {
            Courses = courses;
        }
    }
}
