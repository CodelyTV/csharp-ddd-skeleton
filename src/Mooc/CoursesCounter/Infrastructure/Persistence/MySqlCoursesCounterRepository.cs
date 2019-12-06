namespace CodelyTv.Mooc.CoursesCounter.Infrastructure.Persistence
{
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Shared.Infrastructure.Persistence.EntityFramework;

    public class MySqlCoursesCounterRepository : ICoursesCounterRepository
    {
        private MoocContext _context;

        public MySqlCoursesCounterRepository(MoocContext context)
        {
            this._context = context;
        }

        public async Task Save(CoursesCounter counter)
        {
            if (this._context.Entry(counter).State == EntityState.Detached)
            {
                this._context.CoursesCounter.Add(counter);
            }
            else
            {
                this._context.CoursesCounter.Update(counter);
            }

            await this._context.SaveChangesAsync();
        }

        public CoursesCounter Search()
        {
            return this._context.CoursesCounter.FirstOrDefault();
        }
    }
}