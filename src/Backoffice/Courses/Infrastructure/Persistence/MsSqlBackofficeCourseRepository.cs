namespace CodelyTv.Backoffice.Courses.Infrastructure.Persistence
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CodelyTv.Backoffice.Courses.Domain;
    using CodelyTv.Backoffice.Shared.Infrastructure.Persistence.EntityFramework;
    using Microsoft.EntityFrameworkCore;

    public class MsSqlBackofficeCourseRepository : IBackofficeCourseRepository
    {
        private BackofficeContext _context;

        public MsSqlBackofficeCourseRepository(BackofficeContext context)
        {
            this._context = context;
        }

        public async Task Save(BackofficeCourse course)
        {
            await this._context.BackofficeCourses.AddAsync(course);
            await this._context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BackofficeCourse>> SearchAll()
        {
            return await this._context.BackofficeCourses.ToListAsync();
        }
    }
}