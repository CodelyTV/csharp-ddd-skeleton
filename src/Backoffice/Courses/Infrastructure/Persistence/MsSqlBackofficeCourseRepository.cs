using System.Collections.Generic;
using System.Threading.Tasks;
using CodelyTv.Backoffice.Courses.Domain;
using CodelyTv.Backoffice.Shared.Infrastructure.Persistence.EntityFramework;
using CodelyTv.Shared.Domain.FiltersByCriteria;
using CodelyTv.Shared.Infrastructure.Persistence.EntityFramework.Criteria;
using Microsoft.EntityFrameworkCore;

namespace CodelyTv.Backoffice.Courses.Infrastructure.Persistence
{
    public class MsSqlBackofficeCourseRepository : BackofficeCourseRepository
    {
        private readonly BackofficeContext _context;

        public MsSqlBackofficeCourseRepository(BackofficeContext context)
        {
            _context = context;
        }

        public async Task Save(BackofficeCourse course)
        {
            await _context.BackofficeCourses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BackofficeCourse>> SearchAll()
        {
            return await _context.BackofficeCourses.ToListAsync();
        }

        public async Task<IEnumerable<BackofficeCourse>> Matching(Criteria criteria)
        {
            return await _context.BackofficeCourses.SearchByCriteria(criteria).ToListAsync();
        }
    }
}
