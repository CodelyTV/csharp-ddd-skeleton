namespace Mooc.Courses.Infrastructure.Persistence.EntityFramework
{
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class CourseContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }

        public CourseContext(DbContextOptions<CourseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .ToTable("courses");

            modelBuilder.Entity<Course>().HasKey(x => x.Id);

            modelBuilder.Entity<Course>().Property(x => x.Id)
                .HasConversion(v => v.Value, v => new CourseId(v));

            modelBuilder.Entity<Course>().Property(x => x.Name)
                .HasConversion(v => v.Value, v => new CourseName(v));

            modelBuilder.Entity<Course>().Property(x => x.Duration)
                .HasConversion(v => v.Value, v => new CourseDuration(v));
        }
    }
}