namespace CodelyTv.Backoffice.Shared.Infrastructure.Persistence.EntityFramework
{
    using CodelyTv.Backoffice.Courses.Domain;
    using CodelyTv.Backoffice.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations;
    using Microsoft.EntityFrameworkCore;

    public class BackofficeContext : DbContext
    {
        public DbSet<BackofficeCourse> BackofficeCourses { get; set; }

        public BackofficeContext(DbContextOptions<BackofficeContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BackofficeCourseConfiguration());
        }
    }
}