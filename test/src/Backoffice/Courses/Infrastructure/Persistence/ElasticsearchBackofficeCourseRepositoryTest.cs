using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodelyTv.Backoffice.Courses.Domain;
using CodelyTv.Test.Backoffice.Courses.Domain;
using CodelyTv.Test.Shared.Domain.Criterias;
using Xunit;

namespace CodelyTv.Test.Backoffice.Courses.Infrastructure.Persistence
{
    public class ElasticsearchBackofficeCourseRepositoryTest : BackofficeCoursesModuleInfrastructureTestCase
    {
        [Fact]
        public void ItShouldSaveAValidCourse()
        {
            ElasticRepository.Save(BackofficeCourseMother.Random());
        }

        [Fact]
        public async Task ItShouldSearchAllExistingCourses()
        {
            var existingCourse = BackofficeCourseMother.Random();
            var anotherExistingCourse = BackofficeCourseMother.Random();

            var existingCourses = new List<BackofficeCourse>
            {
                existingCourse, anotherExistingCourse
            };

            await ElasticRepository.Save(existingCourse);
            await ElasticRepository.Save(anotherExistingCourse);

            await WaitFor(async () => (await ElasticRepository.SearchAll()).Any());
            Assert.Equal(existingCourses, (await ElasticRepository.SearchAll()).ToList());
        }

        [Fact]
        public async Task ItShouldSearchAllExistingCoursesWithAnEmptyCriteria()
        {
            var existingCourse = BackofficeCourseMother.Random();
            var anotherExistingCourse = BackofficeCourseMother.Random();

            var existingCourses = new List<BackofficeCourse>
            {
                existingCourse, anotherExistingCourse
            };

            await ElasticRepository.Save(existingCourse);
            await ElasticRepository.Save(anotherExistingCourse);

            await WaitFor(async () => (await ElasticRepository.SearchAll()).Any());
            Assert.Equal(existingCourses, (await ElasticRepository.Matching(CriteriaMother.Empty())).ToList());
        }

        [Fact]
        public async Task ItShouldFilterByCriteria()
        {
            var dddInPhpCourse = BackofficeCourseMother.WithName("DDD en PHP");
            var dddInCSharpCourse = BackofficeCourseMother.WithName("DDD en C#");
            var exprimiendoIntellij = BackofficeCourseMother.WithName("Exprimiendo Intellij");

            var courses = new List<BackofficeCourse>()
            {
                dddInPhpCourse, dddInCSharpCourse
            };

            var nameContainsDddCriteria = BackofficeCourseCriteriaMother.NameContains("DDD");

            await ElasticRepository.Save(dddInPhpCourse);
            await ElasticRepository.Save(dddInCSharpCourse);
            await ElasticRepository.Save(exprimiendoIntellij);

            await WaitFor(async () => (await ElasticRepository.Matching(nameContainsDddCriteria)).Any());

            Assert.Equal(courses, (await ElasticRepository.Matching(nameContainsDddCriteria)).ToList());
        }
    }
}
