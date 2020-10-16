using CodelyTv.Mooc.Courses.Domain;
using CodelyTv.Shared.Infrastructure.Persistence.EntityFramework.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable(nameof(MoocContext.Courses).ToDatabaseFormat());

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(v => v.Value, v => new CourseId(v))
                .HasColumnName(nameof(Course.Id).ToDatabaseFormat());

            builder.OwnsOne(x => x.Name)
                .Property(x => x.Value)
                .HasColumnName(nameof(Course.Name).ToDatabaseFormat());

            builder.OwnsOne(x => x.Duration)
                .Property(x => x.Value)
                .HasColumnName(nameof(Course.Duration).ToDatabaseFormat());
        }
    }
}
