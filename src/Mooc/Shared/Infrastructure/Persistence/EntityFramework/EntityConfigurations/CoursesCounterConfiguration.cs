namespace CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    using System.Collections.Generic;
    using System.Linq;
    using Courses.Domain;
    using CoursesCounter.Domain;
    using Extension;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Newtonsoft.Json;

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


            builder.Property(b => b.ExistingCourses)
                .HasColumnName("existing_courses")
                .HasConversion(
                    v => ConvertExistingCoursesToJson(v),
                    v => ConvertExistingCoursesFromJson(v));
        }

        private static string ConvertExistingCoursesToJson(List<CourseId> courseIds)
        {
            return JsonConvert.SerializeObject(courseIds.Select(x => x.ToString()));
        }

        private static List<CourseId> ConvertExistingCoursesFromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<string>>(json).Select(x => new CourseId(x)).ToList();
        }
    }
}