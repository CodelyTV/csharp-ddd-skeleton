using System.Threading.Tasks;
using CodelyTv.Mooc.Courses.Domain;
using CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CodelyTv.Mooc.Courses.Infrastructure.Persistence
{
    public class MsSqlCourseRepository : CourseRepository
    {
        private readonly MoocContext _context;

        public MsSqlCourseRepository(MoocContext context)
        {
            _context = context;
        }

        public async Task Save(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task<Course> Search(CourseId id)
        {
            return await _context.Courses.FirstOrDefaultAsync(c => c.Id.Equals(id));
        }
    }
}
