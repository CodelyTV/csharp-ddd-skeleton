namespace CodelyTv.Backoffice.Courses.Application
{
    using System.Collections.Generic;

    public class BackofficeCoursesResponse
    {
        public readonly IEnumerable<BackofficeCourseResponse> Courses;

        public BackofficeCoursesResponse(IEnumerable<BackofficeCourseResponse> courses)
        {
            Courses = courses;
        }
    }
}