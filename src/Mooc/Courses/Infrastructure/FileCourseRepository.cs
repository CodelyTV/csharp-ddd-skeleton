using System.IO;
using System.Threading.Tasks;
using CodelyTv.Mooc.Courses.Domain;
using Newtonsoft.Json;

namespace CodelyTv.Mooc.Courses.Infrastructure
{
    public class FileCourseRepository : CourseRepository
    {
        private readonly string _filePath = Directory.GetCurrentDirectory() + "/courses";

        public async Task Save(Course course)
        {
            await Task.Run(() =>
            {
                using (var outputFile = new StreamWriter(FileName(course.Id.Value), false))
                {
                    outputFile.WriteLine(JsonConvert.SerializeObject(course));
                }
            });
        }

        public async Task<Course> Search(CourseId id)
        {
            if (File.Exists(FileName(id.Value)))
            {
                var text = await File.ReadAllTextAsync(FileName(id.Value));
                return JsonConvert.DeserializeObject<Course>(text);
            }

            return null;
        }

        private string FileName(string id)
        {
            return $"{_filePath}.{id}.repo";
        }
    }
}
