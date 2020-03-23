namespace CodelyTv.Mooc.Courses.Infrastructure.Persistence
{
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using Microsoft.EntityFrameworkCore;
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

        public async Task<Course> Search(CourseId id)
        {
            return await this._context.Courses.FirstOrDefaultAsync(c => c.Id.Equals(id));
        }
    }
}