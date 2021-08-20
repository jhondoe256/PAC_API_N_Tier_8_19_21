namespace PAC.DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ovarhaul : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Department", "DepartmentName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Department", "DepartmentName", c => c.String());
        }
    }
}
