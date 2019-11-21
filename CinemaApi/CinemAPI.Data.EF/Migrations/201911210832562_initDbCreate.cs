namespace CinemAPI.Data.EF
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDbCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cinemas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        SeatsPerRow = c.Short(nullable: false),
                        Rows = c.Short(nullable: false),
                        CinemaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cinemas", t => t.CinemaId, cascadeDelete: true)
                .Index(t => t.CinemaId);
            
            CreateTable(
                "dbo.Projections",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        AvailableSeatsCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DurationMinutes = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectionStartDate = c.DateTime(nullable: false),
                        MovieName = c.String(nullable: false),
                        CinemaName = c.String(nullable: false),
                        RoomNumber = c.Int(nullable: false),
                        Row = c.Int(nullable: false),
                        Col = c.Int(nullable: false),
                        ProjectionId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projections", t => t.ProjectionId, cascadeDelete: true)
                .Index(t => t.ProjectionId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectionStartDate = c.DateTime(nullable: false),
                        MovieName = c.String(nullable: false),
                        CinemaName = c.String(nullable: false),
                        RoomNumber = c.Int(nullable: false),
                        Row = c.Int(nullable: false),
                        Column = c.Int(nullable: false),
                        ProjectionId = c.Long(nullable: false),
                        IsCancelled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projections", t => t.ProjectionId, cascadeDelete: true)
                .Index(t => t.ProjectionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "ProjectionId", "dbo.Projections");
            DropForeignKey("dbo.Tickets", "ProjectionId", "dbo.Projections");
            DropForeignKey("dbo.Projections", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Projections", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Rooms", "CinemaId", "dbo.Cinemas");
            DropIndex("dbo.Reservations", new[] { "ProjectionId" });
            DropIndex("dbo.Tickets", new[] { "ProjectionId" });
            DropIndex("dbo.Projections", new[] { "MovieId" });
            DropIndex("dbo.Projections", new[] { "RoomId" });
            DropIndex("dbo.Rooms", new[] { "CinemaId" });
            DropTable("dbo.Reservations");
            DropTable("dbo.Tickets");
            DropTable("dbo.Movies");
            DropTable("dbo.Projections");
            DropTable("dbo.Rooms");
            DropTable("dbo.Cinemas");
        }
    }
}
