namespace CodelyTv.Shared.Infrastructure.Persistence.EntityFramework.Criteria
{
    using System.Linq;
    using CodelyTv.Shared.Domain.FiltersByCriteria;
    using Microsoft.EntityFrameworkCore;

    public static class SearchByCriteriaExtension
    {
        public static IQueryable<T> SearchByCriteria<T>(this DbSet<T> db, Criteria criteria) where T : class
        {
            return db.Where(criteria).OrderBy(criteria).Offset(criteria).Limit(criteria);
        }
    }
}