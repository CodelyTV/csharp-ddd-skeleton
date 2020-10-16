using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CodelyTv.Shared.Infrastructure.Persistence.EntityFramework.Criteria
{
    public static class SearchByCriteriaExtension
    {
        public static IQueryable<T> SearchByCriteria<T>(this DbSet<T> db, Domain.FiltersByCriteria.Criteria criteria)
            where T : class
        {
            return db.Where(criteria).OrderBy(criteria).Offset(criteria).Limit(criteria);
        }
    }
}
