namespace CC.Plugins.Vehicle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CCVehicle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        tbVehicleId = c.Int(nullable: false),
                        ProgramId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        StartYW = c.Int(nullable: false),
                        EndYW = c.Int(nullable: false),
                        NumberOfLevels = c.Int(nullable: false),
                        SellUnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deleted = c.Boolean(),
                        CreatedByUser = c.Int(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false),
                        UpdatedByUser = c.Int(),
                        UpdatedOnUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CCProgram", t => t.ProgramId)
                .Index(t => t.ProgramId);
            
           
            
            
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CCVehicle", "ProgramId", "dbo.CCProgram");
            DropIndex("dbo.CCVehicle", new[] { "ProgramId" });
           
           
            DropTable("dbo.CCVehicle");
        }
    }
}
