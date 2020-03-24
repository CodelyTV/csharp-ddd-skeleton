namespace CodelyTv.Mooc.Courses.Infrastructure
{
    using System.IO;
    using System.Threading.Tasks;
    using Domain;
    using Newtonsoft.Json;

    public class FileCourseRepository : ICourseRepository
    {
        private readonly string _filePath = Directory.GetCurrentDirectory() + "/courses";

        public async Task Save(Course course)
        {
            await Task.Run(() =>
            {
                using (StreamWriter outputFile = new StreamWriter(this.FileName(course.Id.Value), false))
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