namespace CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework
{
    using CodelyTv.Shared.Domain.Bus.Event;
    using Courses.Domain;
    using CoursesCounter.Domain;
    using EntityConfigurations;
    using Microsoft.EntityFrameworkCore;

    public class MoocContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<CoursesCounter> CoursesCounter { get; set; }
        public DbSet<DomainEventPrimitive> DomainEvents { get; set; }

        public MoocContext(DbContextOptions<MoocContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new CoursesCounterConfiguration());
            modelBuilder.ApplyConfiguration(new DomainEventPrimitiveConfiguration());
        }
    }
}