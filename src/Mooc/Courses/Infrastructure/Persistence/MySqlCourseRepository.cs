namespace Mooc.Courses.Infrastructure.Persistence
{
    using System.Linq;
    using Domain;
    using EntityFramework;

    public class MySqlCourseRepository : ICourseRepository
    {
        private CourseContext Context;

        public MySqlCourseRepository(CourseContext context)
        {
            this.Context = context;
        }

        public void Save(Course course)
        {
            this.Context.Courses.Add(course);
            this.Context.SaveChanges();
        }

        public Course Search(CourseId id)
        {
            return this.Context.Courses.FirstOrDefault(c => c.Id.Equals(id));
        }
    }
}