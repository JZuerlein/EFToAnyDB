using EFToAnyDB.Data;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace EFToAnyDB.SQLite
{
    public class DBContextFactory : IDesignTimeDbContextFactory<EFCoreContext>
    {
        public EFCoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EFCoreContext>();
            optionsBuilder.UseSqlite("", _ => _.MigrationsAssembly("EFToAnyDB.SQLite"));

            return new EFCoreContext(optionsBuilder.Options);
        }
    }
}
