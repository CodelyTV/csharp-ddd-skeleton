using System.Collections.Generic;
using System.Globalization;
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
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }

            await Persist(course.Id ?? throw new InvalidOperationException("Course ID is missing"),
                          JsonConvert.SerializeObject(course.ToPrimitives() ?? throw new InvalidOperationException("Course primitives are missing")));
        }

        public async Task<IEnumerable<BackofficeCourse>> Matching(Criteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException(nameof(criteria));
            }

            var docs = await SearchByCriteria(criteria);
            return docs?.Select(BackofficeCourse.FromPrimitives).ToList() ?? Enumerable.Empty<BackofficeCourse>();
        }

        public async Task<IEnumerable<BackofficeCourse>> SearchAll()
        {
            var docs = await SearchAllInElastic();
            return docs?.Select(BackofficeCourse.FromPrimitives).ToList() ?? Enumerable.Empty<BackofficeCourse>();
        }

        protected override string ModuleName()
        {
            return nameof(BackofficeCourse).ToLower(CultureInfo.CurrentCulture);
        }
    }
}
