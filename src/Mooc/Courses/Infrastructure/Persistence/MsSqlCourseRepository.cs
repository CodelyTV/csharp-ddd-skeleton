namespace CodelyTv.Mooc.Courses.Infrastructure.Persistence
{
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using Shared.Infrastructure.Persistence.EntityFramework;

    public class MsSqlCourseRepository : ICourseRepository
    {
        private MoocContext _context;

        public MsSqlCourseRepository(MoocContext context)
        {
            this._context = context;
        }

        public async Task Save(Course course)
        {
            await this._context.Courses.AddAsync(course);
            await this._context.SaveChangesAsync();
        }

        public Course Search(CourseId id)
        {
            return this._context.Courses.FirstOrDefault(c => c.Id.Value.Equals(id.Value));
        }
    }
}