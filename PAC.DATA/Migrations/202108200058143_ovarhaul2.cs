namespace PAC.DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ovarhaul2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Department", "ModifiedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Department", "ModifiedDate", c => c.DateTime(nullable: false));
        }
    }
}
