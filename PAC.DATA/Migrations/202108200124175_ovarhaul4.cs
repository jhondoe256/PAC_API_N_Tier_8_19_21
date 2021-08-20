namespace PAC.DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ovarhaul4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employee", "ModifiedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employee", "ModifiedDate", c => c.DateTime(nullable: false));
        }
    }
}
