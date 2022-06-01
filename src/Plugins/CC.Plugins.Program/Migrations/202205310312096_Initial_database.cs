namespace CC.Plugins.Program.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CCProgram",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        tbProgramCode = c.String(nullable: false, maxLength: 50),
                        Code = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 100),
                        ShortDescription = c.String(nullable: false, maxLength: 250),
                        LongDescription = c.String(nullable: false),
                        ProgramType = c.String(nullable: false, maxLength: 20),
                        Sequence = c.Int(),
                        LockOutDays = c.Int(),
                        Frequency = c.Int(),
                        AppliedTo = c.Int(),
                        Deleted = c.Boolean(),
                        CreatedByUser = c.Int(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false),
                        UpdatedByUser = c.Int(),
                        UpdatedOnUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CCProgram");
        }
    }
}
