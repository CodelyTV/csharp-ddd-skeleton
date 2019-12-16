namespace CodelyTv.Test.Shared.Infrastructure.EntityFramework
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Mooc.Shared.Infrastructure.Persistence.EntityFramework.Extension;

    public class DatabaseCleaner
    {
        public void Invoke(DbContext context)
        {
            var tables = this.Tables(context);
            var truncateTablesSql = this.TruncateDatabaseSql(tables);
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