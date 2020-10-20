using System.Threading.Tasks;
using CodelyTv.Backoffice.Courses.Domain;

namespace CodelyTv.Backoffice.Courses.Application.Create
{
    public class BackofficeCourseCreator
    {
        private readonly BackofficeCourseRepository _repository;

        public BackofficeCourseCreator(BackofficeCourseRepository repository)
        {
            _repository = repository;
        }

        public async Task Create(string id, string name, string duration)
        {
            await _repository.Save(new BackofficeCourse(id, name, duration));
        }
    }
}
