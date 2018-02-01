namespace CatStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class float_cost_to_double : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cats", "Cost", c => c.Double(nullable: false));
            AlterColumn("dbo.Orders", "Sum", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Sum", c => c.Single(nullable: false));
            AlterColumn("dbo.Cats", "Cost", c => c.Single(nullable: false));
        }
    }
}
