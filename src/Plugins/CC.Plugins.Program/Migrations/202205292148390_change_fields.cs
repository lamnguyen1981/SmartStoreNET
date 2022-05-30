namespace CC.Plugins.Program.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_fields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CCProgram", "UpdatedByUser", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CCProgram", "UpdatedByUser", c => c.Int(nullable: false));
        }
    }
}
