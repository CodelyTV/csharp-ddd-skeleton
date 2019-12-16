namespace CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework.ValueConverter
{
    using System.Collections.Generic;
    using CodelyTv.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations;
    using Courses.Domain;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

    public class ExistingCoursesConverter : ValueConverter<List<CourseId>, string>
    {
        public ExistingCoursesConverter(ConverterMappingHints mappingHints = null)
            : base(v => ConvertConfiguration.ObjectToJson(v),
                v => ConvertConfiguration.ObjectFromJson<CourseId>(v),
                mappingHints
            )
        {
        }
    }
}