namespace PostgreSqlEntityFramework.Infra.Data.PostgresMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V0001__InitialSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonId = c.Guid(nullable: false),
                        Fullname = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.People");
        }
    }
}
