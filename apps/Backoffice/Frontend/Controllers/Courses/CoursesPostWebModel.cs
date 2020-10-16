using System.ComponentModel.DataAnnotations;
using CodelyTv.Shared.Validator.Attributes;

namespace CodelyTv.Apps.Backoffice.Frontend.Controllers.Courses
{
    public class CoursesPostWebModel
    {
        [Uuid]
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Duration { get; set; }
    }
}
