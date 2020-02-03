namespace MoocTest.apps.Backend.Controller.Courses
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using CodelyTv.Apps.Mooc.Backend;
    using CodelyTv.Tests.Mooc;
    using Xunit;

    public class CoursesPutControllerShould : IClassFixture<MoocWebApplicationFactory<Startup>>
    {
        private readonly MoocWebApplicationFactory<Startup> _factory;

        public CoursesPutControllerShould(MoocWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test()
        {
            var client = _factory.GetAnonymousClient();

            using (var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri("/courses/1aab45ba-3c7a-4344-8936-78466eca77fa", UriKind.Relative),
                Content = new StringContent("{\"name\": \"The best course\", \"duration\": \"5 hours\"}", Encoding.UTF8,
                    "application/json")
            })
            {
                var response = await client.SendAsync(request);

                Assert.Equal(201, (int) response.StatusCode);
            }
        }
    }
}