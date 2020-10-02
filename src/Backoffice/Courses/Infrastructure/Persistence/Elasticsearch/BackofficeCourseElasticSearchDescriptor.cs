namespace CodelyTv.Backoffice.Courses.Infrastructure.Persistence.Elasticsearch
{
    using CodelyTv.Backoffice.Courses.Domain;
    using Nest;

    public static class BackofficeCourseElasticsearchDescriptor
    {
        public static CreateIndexDescriptor CreateBackofficeCourseDescriptor(this CreateIndexDescriptor descriptor)
        {
            return descriptor?.Map<BackofficeCourse>(m => m.AutoMap().Properties(pr => pr
                .Keyword(i => i.Name("id"))
                .Keyword(i => i.Name("name"))
            ));
        }
    }
}