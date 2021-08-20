namespace PAC.DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ovarhaul3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Position", "ModifiedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Position", "ModifiedDate", c => c.DateTime(nullable: false));
        }
    }
}
