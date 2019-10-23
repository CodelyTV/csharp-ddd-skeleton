namespace CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework
{
    using Courses.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MoocContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }

        public MoocContext(DbContextOptions<MoocContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder) modelBuilder.Entity<Course>(), "courses");

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