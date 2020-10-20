using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodelyTv.Backoffice.Courses.Domain;
using CodelyTv.Shared.Domain.FiltersByCriteria;
using CodelyTv.Shared.Infrastructure.Elasticsearch;
using Newtonsoft.Json;

namespace CodelyTv.Backoffice.Courses.Infrastructure.Persistence.Elasticsearch
{
    public class ElasticsearchBackofficeCourseRepository : ElasticsearchRepository<BackofficeCourse>,
        BackofficeCourseRepository
    {
        public ElasticsearchBackofficeCourseRepository(ElasticsearchClient client) : base(client)
        {
        }

        public async Task Save(BackofficeCourse course)
        {
            await Persist(course.Id, JsonConvert.SerializeObject(course?.ToPrimitives()));
        }

        public async Task<IEnumerable<BackofficeCourse>> Matching(Criteria criteria)
        {
            var docs = await SearchByCriteria(criteria);
            return docs?.Select(BackofficeCourse.FromPrimitives).ToList();
        }

        public async Task<IEnumerable<BackofficeCourse>> SearchAll()
        {
            var docs = await SearchAllInElastic();
            return docs?.Select(BackofficeCourse.FromPrimitives).ToList();
        }

        protected override string ModuleName()
        {
            return nameof(BackofficeCourse).ToLower();
        }
    }
}
