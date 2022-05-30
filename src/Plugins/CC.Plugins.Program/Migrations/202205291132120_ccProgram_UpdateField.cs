namespace CC.Plugins.Program.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ccProgram_UpdateField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CCProgram", "tbProgramCode", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.CCProgram", "tbProgramId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CCProgram", "tbProgramId", c => c.Int(nullable: false));
            DropColumn("dbo.CCProgram", "tbProgramCode");
        }
    }
}
