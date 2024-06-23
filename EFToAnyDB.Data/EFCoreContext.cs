using EFToAnyDB.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EFToAnyDB.Data
{
    public class EFCoreContext : DbContext
    {
        public DbSet<Person> People => Set<Person>();

        public EFCoreContext(DbContextOptions<EFCoreContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
