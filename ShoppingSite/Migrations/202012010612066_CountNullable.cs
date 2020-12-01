namespace ShoppingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Carts", "Count", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Carts", "Count", c => c.Int(nullable: false));
        }
    }
}
