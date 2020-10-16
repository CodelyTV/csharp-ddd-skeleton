using System.Collections.Generic;
using System.Linq;
using CodelyTv.Shared.Infrastructure.Persistence.EntityFramework.Extension;
using Microsoft.EntityFrameworkCore;

namespace CodelyTv.Test.Shared.Infrastructure.EntityFramework
{
    public class DatabaseCleaner
    {
        public void Invoke(DbContext context)
        {
            var tables = Tables(context);
            var truncateTablesSql = TruncateDatabaseSql(tables);
            context.Database.ExecuteSqlCommand(truncateTablesSql);
        }

        private string TruncateDatabaseSql(List<string> tables)
        {
            var truncateTables = tables.Select(x => $"TRUNCATE TABLE {x.ToDatabaseFormat()};").ToList();
            return $"SET FOREIGN_KEY_CHECKS=0;{string.Join(" ", truncateTables)} SET FOREIGN_KEY_CHECKS = 1;";
        }

        private List<string> Tables(DbContext context)
        {
            return context.GetType().GetProperties()
                .Where(x => x.PropertyType.Name == "DbSet`1")
                .Select(x => x.Name).ToList();
        }
    }
}
