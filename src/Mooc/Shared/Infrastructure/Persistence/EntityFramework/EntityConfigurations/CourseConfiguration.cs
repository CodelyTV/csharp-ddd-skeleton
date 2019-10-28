namespace CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    using Courses.Domain;
    using Extension;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable(nameof(MoocContext.Courses).FormatDatabaseName());

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(v => v.Value, v => new CourseId(v))
                .HasColumnName(nameof(Course.Id));

            builder.OwnsOne(x => x.Name).Property(x => x.Value).HasColumnName(nameof(Course.Name));
            builder.OwnsOne(x => x.Duration).Property(x => x.Value).HasColumnName(nameof(Course.Duration));
        }
    }
}