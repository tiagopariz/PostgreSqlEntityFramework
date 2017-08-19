#Projeto de Domínio

1. Crie um projeto do tipo class library em PostgreSqlEntityFramework/Domain chamado PostgreSqlEntityFramework.Domain
2. Exclua Class1.cs
3. Remova todas as referências
4. Crie a classe Person em PostgreSqlEntityFramework.Domain/Entities
```CSharp
using System;

namespace PostgreSqlEntityFramework.Domain.Entities
{
    public class Person
    {
        public Guid PersonId { get; set; }
        public string Fullname { get; set; }
    }
}
```

#Projeto de Dados

1. Crie um projeto do tipo class library em PostgreSqlEntityFramework/Infra/Data chamado PostgreSqlEntityFramework.Infra.Data
2. Exclua Class1.cs
3. Remova todas as referências, exceto System
4. Install-Package Npgsql.EntityFramework
5. Adicione referência para o projeto PostgreSqlEntityFramework.Domain
6. Crie o contexto em PostgreSqlEntityFramework.Infra.Data/Context
```CSharp
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
```
6. Set as startup project
7. App.Config
<?xml version="1.0" encoding="utf-8"?>
<configuration>

  <configSections>
    <!-- For more information on Entity Framework configuration, visit http://go.microsoft.com/fwlink/?LinkID=237468 -->
    <section name="entityFramework" type="System.Data.Entity.Internal.ConfigFile.EntityFrameworkSection, EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" requirePermission="false" />
  </configSections>

  <connectionStrings>
    <add name="PostgresDbConnection"
         connectionString="Server=localhost;Port=5432;Database=postgresDb;User Id=postgres;Password=123456;"
         providerName="Npgsql" />
  </connectionStrings>

  <system.data>
    <DbProviderFactories>
      <add name="Npgsql Data Provider" invariant="Npgsql" description=".NET Data Provider for PostgreSQL" type="Npgsql.NpgsqlFactory, Npgsql, Version=2.2.7.0, Culture=neutral, PublicKeyToken=5D8B90D52F46FDA7"/>
    </DbProviderFactories>
  </system.data>
  
  <entityFramework>
    <defaultConnectionFactory type="System.Data.Entity.Infrastructure.LocalDbConnectionFactory, EntityFramework">
      <parameters>
        <parameter value="v13.0" />
      </parameters>
    </defaultConnectionFactory>
    <providers>
      <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
      <provider invariantName="Npgsql" type="Npgsql.NpgsqlServices, Npgsql.EntityFramework" />
    </providers>
  </entityFramework>
  
</configuration>​​

8. Enable-Migrations -ProjectName "PostgreSqlEntityFramework.Infra.Data" -ConnectionStringName PostgresDbConnection -ContextTypeName PostgreSqlEntityFramework.Infra.Data.Context.Context -MigrationsDirectory PostgresMigrations
9. Add-Migration V0001__InitialSetup
10. Update-database