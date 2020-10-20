using System.Threading.Tasks;
using CodelyTv.Mooc.CoursesCounters.Domain;
using CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CodelyTv.Mooc.CoursesCounters.Infrastructure.Persistence
{
    public class MsSqlCoursesCounterRepository : CoursesCounterRepository
    {
        private readonly MoocContext _context;

        public MsSqlCoursesCounterRepository(MoocContext context)
        {
            _context = context;
        }

        public async Task Save(CoursesCounter counter)
        {
            if (_context.Entry(counter).State == EntityState.Detached)
                await _context.AddAsync(counter);
            else
                _context.Entry(counter).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task<CoursesCounter> Search()
        {
            return await _context.CoursesCounter.SingleOrDefaultAsync();
        }
    }
}
