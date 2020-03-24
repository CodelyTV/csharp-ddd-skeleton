namespace CodelyTv.Mooc.CoursesCounter.Infrastructure.Persistence
{
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Shared.Infrastructure.Persistence.EntityFramework;

    public class MsSqlCoursesCounterRepository : ICoursesCounterRepository
    {
        private readonly MoocContext _context;

        public MsSqlCoursesCounterRepository(MoocContext context)
        {
            this._context = context;
        }

        public async Task Save(CoursesCounter counter)
        {
            if (this._context.Entry(counter).State == EntityState.Detached)
            {
                await this._context.AddAsync(counter);
            }
            else
            {
                this._context.Entry(counter).State = EntityState.Modified;
            }

            await this._context.SaveChangesAsync();
        }

        public async Task<CoursesCounter> Search()
        {
            return await this._context.CoursesCounter.FirstOrDefaultAsync();
        }
    }
}