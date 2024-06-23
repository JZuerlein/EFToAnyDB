using EFToAnyDB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EFToAnyDB.SQLServer
{
    public class DBContextFactory : IDesignTimeDbContextFactory<EFCoreContext>
    {
        public EFCoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EFCoreContext>();
            optionsBuilder.UseSqlServer("", _ => _.MigrationsAssembly("EFToAnyDB.SQLServer"));

            return new EFCoreContext(optionsBuilder.Options);
        }
    }
}
