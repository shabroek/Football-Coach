namespace FootballCoach.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        MatchId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        IsHomeMatch = c.Boolean(nullable: false),
                        Opponent_TeamId = c.Int(),
                    })
                .PrimaryKey(t => t.MatchId)
                .ForeignKey("dbo.Teams", t => t.Opponent_TeamId)
                .Index(t => t.Opponent_TeamId);
            
            CreateTable(
                "dbo.MatchEvents",
                c => new
                    {
                        MatchEventId = c.Int(nullable: false, identity: true),
                        Minute = c.Time(nullable: false, precision: 7),
                        Remark = c.String(),
                        Type = c.Int(),
                        Reason = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        AssistedBy_PlayerId = c.Int(),
                        ScoredBy_PlayerId = c.Int(),
                        PlayerOff_PlayerId = c.Int(),
                        PlayerOn_PlayerId = c.Int(),
                        Match_MatchId = c.Int(),
                    })
                .PrimaryKey(t => t.MatchEventId)
                .ForeignKey("dbo.Players", t => t.AssistedBy_PlayerId)
                .ForeignKey("dbo.Players", t => t.ScoredBy_PlayerId)
                .ForeignKey("dbo.Players", t => t.PlayerOff_PlayerId)
                .ForeignKey("dbo.Players", t => t.PlayerOn_PlayerId)
                .ForeignKey("dbo.Matches", t => t.Match_MatchId)
                .Index(t => t.AssistedBy_PlayerId)
                .Index(t => t.ScoredBy_PlayerId)
                .Index(t => t.PlayerOff_PlayerId)
                .Index(t => t.PlayerOn_PlayerId)
                .Index(t => t.Match_MatchId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        PreferredPosition = c.Int(nullable: false),
                        Team_TeamId = c.Int(),
                    })
                .PrimaryKey(t => t.PlayerId)
                .ForeignKey("dbo.Teams", t => t.Team_TeamId)
                .Index(t => t.Team_TeamId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "Team_TeamId", "dbo.Teams");
            DropForeignKey("dbo.Matches", "Opponent_TeamId", "dbo.Teams");
            DropForeignKey("dbo.MatchEvents", "Match_MatchId", "dbo.Matches");
            DropForeignKey("dbo.MatchEvents", "PlayerOn_PlayerId", "dbo.Players");
            DropForeignKey("dbo.MatchEvents", "PlayerOff_PlayerId", "dbo.Players");
            DropForeignKey("dbo.MatchEvents", "ScoredBy_PlayerId", "dbo.Players");
            DropForeignKey("dbo.MatchEvents", "AssistedBy_PlayerId", "dbo.Players");
            DropIndex("dbo.Players", new[] { "Team_TeamId" });
            DropIndex("dbo.MatchEvents", new[] { "Match_MatchId" });
            DropIndex("dbo.MatchEvents", new[] { "PlayerOn_PlayerId" });
            DropIndex("dbo.MatchEvents", new[] { "PlayerOff_PlayerId" });
            DropIndex("dbo.MatchEvents", new[] { "ScoredBy_PlayerId" });
            DropIndex("dbo.MatchEvents", new[] { "AssistedBy_PlayerId" });
            DropIndex("dbo.Matches", new[] { "Opponent_TeamId" });
            DropTable("dbo.Teams");
            DropTable("dbo.Players");
            DropTable("dbo.MatchEvents");
            DropTable("dbo.Matches");
        }
    }
}
