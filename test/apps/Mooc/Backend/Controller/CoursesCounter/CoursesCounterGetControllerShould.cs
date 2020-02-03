namespace MoocTest.apps.Backend.Controller.CoursesCounter
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using CodelyTv.Apps.Mooc.Backend;
    using CodelyTv.Mooc.Courses.Domain;
    using CodelyTv.Shared.Domain.Bus.Event;
    using CodelyTv.Shared.Infrastructure.Bus.Event;
    using CodelyTv.Test.Shared;
    using CodelyTv.Tests.Mooc;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class CoursesCounterGetControllerShould : IClassFixture<MoocWebApplicationFactory<Startup>>
    {
        private readonly MoocWebApplicationFactory<Startup> _factory;

        public CoursesCounterGetControllerShould(MoocWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task get_the_counter_with_one_course()
        {
            var client = _factory.GetAnonymousClient();

            var domainEvents = new List<DomainEvent>
            {
                new CourseCreatedDomainEvent("92dd8402-69f3-4900-b569-3f2c2797065f", "DDD en CSharp", "10 years")
            };

            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var eventBus = scope.ServiceProvider.GetRequiredService<InMemoryApplicationEventBus>();
                await eventBus.Publish(domainEvents);

                using (var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("/courses-counter", UriKind.Relative)
                })
                {
                    var response = await client.SendAsync(request);
                    var result = response.Content.ReadAsStringAsync().Result;
                    Assert.Equal(200, (int) response.StatusCode);
                    Assert.Equal("{\"total\":1}", result);
                }
            }
        }

        [Fact]
        public async Task get_the_counter_with_more_than_one_course()
        {
            var client = _factory.GetAnonymousClient();

            var domainEvents = new List<DomainEvent>
            {
                new CourseCreatedDomainEvent("8f34bc99-e0e2-4296-a008-75f51f03aeb4", "DDD en Java", "7 days"),
                new CourseCreatedDomainEvent("3642f700-868a-4778-9317-a2d542d01785", "DDD en PHP", "6 days"),
                new CourseCreatedDomainEvent("92dd8402-69f3-4900-b569-3f2c2797065f", "DDD en CSharp", "10 years")
            };
            
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var eventBus = scope.ServiceProvider.GetRequiredService<InMemoryApplicationEventBus>();
                await eventBus.Publish(domainEvents);

                using (var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("/courses-counter", UriKind.Relative)
                })
                {
                    var response = await client.SendAsync(request);
                    var result = await Utility.GetResponseContent<string>(response);
                    Assert.Equal(200, (int) response.StatusCode);
                    Assert.Equal("{\"total\":3}", result);
                }
            }
            
            
            
            
        }

        // [Fact]
        // public async Task get_the_counter_with_more_than_one_course_having_duplicated_events()
        // {
        //     GivenISendEventsToTheBus(new List<DomainEvent>
        //         {
        //             new CourseCreatedDomainEvent("8f34bc99-e0e2-4296-a008-75f51f03aeb4", "DDD en Java", "7 days"),
        //             new CourseCreatedDomainEvent("8f34bc99-e0e2-4296-a008-75f51f03aeb4", "DDD en Java", "7 days"),
        //             new CourseCreatedDomainEvent("8f34bc99-e0e2-4296-a008-75f51f03aeb4", "DDD en Java", "7 days"),
        //             new CourseCreatedDomainEvent("3642f700-868a-4778-9317-a2d542d01785", "DDD en PHP", "6 days"),
        //             new CourseCreatedDomainEvent("3642f700-868a-4778-9317-a2d542d01785", "DDD en PHP", "6 days"),
        //             new CourseCreatedDomainEvent("3642f700-868a-4778-9317-a2d542d01785", "DDD en PHP", "6 days"),
        //             new CourseCreatedDomainEvent("3642f700-868a-4778-9317-a2d542d01785", "DDD en PHP", "6 days"),
        //             new CourseCreatedDomainEvent("92dd8402-69f3-4900-b569-3f2c2797065f", "DDD en CSharp", "10 years"),
        //             new CourseCreatedDomainEvent("92dd8402-69f3-4900-b569-3f2c2797065f", "DDD en CSharp", "10 years")
        //         }
        //     );
        //
        //     await AssertResponse("GET", "/courses-counter", 200, "{\"total\":3}");
        // }
    }
}