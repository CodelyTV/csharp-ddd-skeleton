namespace CodelyTv.Backoffice.Shared.Infrastructure.Persistence.EntityFramework
{
    using CodelyTv.Backoffice.Courses.Domain;
    using Microsoft.EntityFrameworkCore;

    public class BackofficeContext : DbContext
    {
        public DbSet<BackofficeCourse> BackofficeCourses { get; set; }

        public BackofficeContext(DbContextOptions<BackofficeContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}