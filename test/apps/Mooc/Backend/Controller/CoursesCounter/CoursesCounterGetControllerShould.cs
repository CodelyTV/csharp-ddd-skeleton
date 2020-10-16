using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CodelyTv.Apps.Mooc.Backend;
using CodelyTv.Shared.Domain.Bus.Event;
using CodelyTv.Shared.Domain.Courses.Domain;
using CodelyTv.Test.Mooc;
using Xunit;

namespace MoocTest.apps.Backend.Controller.CoursesCounter
{
    public class CoursesCounterGetControllerShould : MoocContextApplicationTestCase
    {
        public CoursesCounterGetControllerShould(MoocWebApplicationFactory<Startup> factory) : base(factory)
        {
            CreateAnonymousClient();
        }

        [Fact]
        public async Task get_the_counter_with_one_course()
        {
            await GivenISendEventsToTheBus(new List<DomainEvent>
                {
                    new CourseCreatedDomainEvent("8f34bc99-e0e2-4296-a008-75f51f03aeb4", "DDD en Java", "7 days")
                }
            );

            await AssertResponse(HttpMethod.Get, "/courses-counter", 200, "{\"total\":1}");
        }

        [Fact]
        public async Task get_the_counter_with_more_than_one_course()
        {
            await GivenISendEventsToTheBus(new List<DomainEvent>
                {
                    new CourseCreatedDomainEvent("8f34bc99-e0e2-4296-a008-75f51f03aeb4", "DDD en Java", "7 days"),
                    new CourseCreatedDomainEvent("3642f700-868a-4778-9317-a2d542d01785", "DDD en PHP", "6 days"),
                    new CourseCreatedDomainEvent("92dd8402-69f3-4900-b569-3f2c2797065f", "DDD en CSharp", "10 years")
                }
            );

            await AssertResponse(HttpMethod.Get, "/courses-counter", 200, "{\"total\":3}");
        }

        [Fact]
        public async Task get_the_counter_with_more_than_one_course_having_duplicated_events()
        {
            await GivenISendEventsToTheBus(new List<DomainEvent>
                {
                    new CourseCreatedDomainEvent("8f34bc99-e0e2-4296-a008-75f51f03aeb4", "DDD en Java", "7 days"),
                    new CourseCreatedDomainEvent("8f34bc99-e0e2-4296-a008-75f51f03aeb4", "DDD en Java", "7 days"),
                    new CourseCreatedDomainEvent("8f34bc99-e0e2-4296-a008-75f51f03aeb4", "DDD en Java", "7 days"),
                    new CourseCreatedDomainEvent("3642f700-868a-4778-9317-a2d542d01785", "DDD en PHP", "6 days"),
                    new CourseCreatedDomainEvent("3642f700-868a-4778-9317-a2d542d01785", "DDD en PHP", "6 days"),
                    new CourseCreatedDomainEvent("3642f700-868a-4778-9317-a2d542d01785", "DDD en PHP", "6 days"),
                    new CourseCreatedDomainEvent("3642f700-868a-4778-9317-a2d542d01785", "DDD en PHP", "6 days"),
                    new CourseCreatedDomainEvent("92dd8402-69f3-4900-b569-3f2c2797065f", "DDD en CSharp", "10 years"),
                    new CourseCreatedDomainEvent("92dd8402-69f3-4900-b569-3f2c2797065f", "DDD en CSharp", "10 years")
                }
            );

            await AssertResponse(HttpMethod.Get, "/courses-counter", 200, "{\"total\":3}");
        }
    }
}
