namespace CC.Plugins.Tactic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CCTactic",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        tbTacticId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        StartYW = c.Int(nullable: false),
                        EndYW = c.Int(nullable: false),
                        TacticCode = c.String(nullable: false, maxLength: 50),
                        TacticType = c.String(nullable: false, maxLength: 50),
                        TacticDescription = c.String(nullable: false, maxLength: 150),
                        TacticAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deleted = c.Boolean(),
                        CreatedByUser = c.Int(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false),
                        UpdatedByUser = c.Int(),
                        UpdatedOnUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CCVehicle", t => t.VehicleId)
                .Index(t => t.VehicleId);  
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CCTactic", "VehicleId", "dbo.CCVehicle");         
            DropIndex("dbo.CCTactic", new[] { "VehicleId" });           
            DropTable("dbo.CCTactic");
        }
    }
}
