using System.Collections.Generic;
using CodelyTv.Mooc.Courses.Domain;
using CodelyTv.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework.ValueConverter
{
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
