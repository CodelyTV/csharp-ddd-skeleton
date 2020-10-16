using System.Net.Http;
using System.Threading.Tasks;
using CodelyTv.Apps.Mooc.Backend;
using CodelyTv.Test.Mooc;
using Xunit;

namespace MoocTest.apps.Backend.Controller.Courses
{
    public class CoursesPutControllerShould : MoocContextApplicationTestCase
    {
        public CoursesPutControllerShould(MoocWebApplicationFactory<Startup> factory) : base(factory)
        {
            CreateAnonymousClient();
        }

        [Fact]
        public async Task create_a_valid_non_existing_course()
        {
            await AssertRequestWithBody(
                HttpMethod.Put,
                "/courses/1aab45ba-3c7a-4344-8936-78466eca77fa",
                "{\"name\": \"The best course\", \"duration\": \"5 hours\"}",
                201);
        }
    }
}
