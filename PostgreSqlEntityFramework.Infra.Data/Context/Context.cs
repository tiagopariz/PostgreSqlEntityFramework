using PostgreSqlEntityFramework.Domain.Entities;
using System.Data.Entity;

namespace PostgreSqlEntityFramework.Infra.Data.Context
{
    public class Context : DbContext
    {
        public Context()
            : base("PostgresDbConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Person> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Person>()
                .HasKey(x => x.PersonId);

            modelBuilder
                .Entity<Person>()
                .Property(x => x.Fullname)
                .HasMaxLength(200);

            base.OnModelCreating(modelBuilder);
        }
    }
}