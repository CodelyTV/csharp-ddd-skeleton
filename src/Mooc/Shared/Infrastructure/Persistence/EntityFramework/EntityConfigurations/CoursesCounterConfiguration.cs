namespace CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    using Courses.Domain;
    using CoursesCounter.Domain;
    using Extension;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CoursesCounterConfiguration : IEntityTypeConfiguration<CoursesCounter>
    {
        public void Configure(EntityTypeBuilder<CoursesCounter> builder)
        {
            builder.ToTable(nameof(MoocContext.CoursesCounter).FormatDatabaseName());

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(v => v.Value, v => new CoursesCounterId(v))
                .HasColumnName(nameof(Course.Id));

            builder.OwnsOne(x => x.Total).Property(x => x.Value).HasColumnName(nameof(CoursesCounter.Total));
        }
    }
}