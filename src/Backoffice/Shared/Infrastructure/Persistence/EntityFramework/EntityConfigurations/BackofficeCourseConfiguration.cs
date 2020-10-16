using CodelyTv.Backoffice.Courses.Domain;
using CodelyTv.Shared.Infrastructure.Persistence.EntityFramework.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodelyTv.Backoffice.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    public class BackofficeCourseConfiguration : IEntityTypeConfiguration<BackofficeCourse>
    {
        public void Configure(EntityTypeBuilder<BackofficeCourse> builder)
        {
            builder.ToTable("courses");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnName(nameof(BackofficeCourse.Name).ToDatabaseFormat());
            builder.Property(x => x.Duration).HasColumnName(nameof(BackofficeCourse.Duration).ToDatabaseFormat());
        }
    }
}
