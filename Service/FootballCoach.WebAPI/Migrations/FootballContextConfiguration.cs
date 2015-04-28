using System;
using FootballCoach.Model;
using FootballCoach.WebAPI.Models;

namespace FootballCoach.WebAPI.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class FootballContextConfiguration : DbMigrationsConfiguration<FootballContext>
    {
        public FootballContextConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "FootballCoach.WebAPI.Models.FootballContext";
        }

        protected override void Seed(FootballContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Players.AddOrUpdate(p => new {p.FirstName, p.LastName},
                new Player {PlayerId = 1, FirstName = "Sander", LastName = "van Broekhoven", DateOfBirth = new DateTime(1984, 5, 11), PreferredPosition = Position.Winger},
                new Player {PlayerId = 2, FirstName = "Mark", LastName = "Didden", DateOfBirth = new DateTime(1984, 7, 11), PreferredPosition = Position.Winger},
                new Player {PlayerId = 3, FirstName = "Michel", LastName = "van Lin", DateOfBirth = new DateTime(1965, 2, 26), PreferredPosition = Position.FullBack},
                new Player {PlayerId = 4, FirstName = "Arie", LastName = "van Rijsewijk", DateOfBirth = null, PreferredPosition = Position.CentralDefender},
                new Player {PlayerId = 5, FirstName = "Bart", LastName = "Wouters", DateOfBirth = new DateTime(1973, 5, 1), PreferredPosition = Position.Goalkeeper},
                new Player {PlayerId = 6, FirstName = "Erik", LastName = "van Duppen", DateOfBirth = new DateTime(1981, 8, 9), PreferredPosition = Position.Striker},
                new Player {PlayerId = 7, FirstName = "Marcel", LastName = "van Broekhoven", DateOfBirth = new DateTime(1960, 8, 4), PreferredPosition = Position.AttackingMidfielder},
                new Player {PlayerId = 8, FirstName = "Bart", LastName = "de Groof", DateOfBirth = new DateTime(1984, 8, 14), PreferredPosition = Position.Winger},
                new Player {PlayerId = 9, FirstName = "Max", LastName = "Haen", DateOfBirth = new DateTime(1984, 9, 1), PreferredPosition = Position.Midfielder},
                new Player {PlayerId = 10, FirstName = "Bart", LastName = "de Groof", DateOfBirth = new DateTime(1984, 8, 14), PreferredPosition = Position.Winger},
                new Player {PlayerId = 11, FirstName = "Ed", LastName = "van Schie", DateOfBirth = new DateTime(1969, 8, 14), PreferredPosition = Position.Midfielder},
                new Player {PlayerId = 12, FirstName = "Örjan", LastName = "Game", DateOfBirth = new DateTime(1984, 8, 14), PreferredPosition = Position.Striker},
                new Player {PlayerId = 13, FirstName = "Laurent", LastName = "van Geelen", DateOfBirth = null, PreferredPosition = Position.Striker},
                new Player {PlayerId = 14, FirstName = "Frans", LastName = "Brekelmans", DateOfBirth = new DateTime(1963, 12, 3), PreferredPosition = Position.CentralDefender},
                new Player {PlayerId = 15, FirstName = "Bas", LastName = "Kluytmans", DateOfBirth = null, PreferredPosition = Position.Striker},
                new Player {PlayerId = 16, FirstName = "Engelbert", LastName = "Zoontjens", DateOfBirth = null, PreferredPosition = Position.Winger},
                new Player {PlayerId = 17, FirstName = "Curt", LastName = "Verhulst", DateOfBirth = null, PreferredPosition = Position.FullBack},
                new Player {PlayerId = 18, FirstName = "Corné", LastName = "van de Pas", DateOfBirth = null, PreferredPosition = Position.CentralDefender},
                new Player {PlayerId = 19, FirstName = "Casper", LastName = "Verhoeven", DateOfBirth = null, PreferredPosition = Position.Winger},
                new Player {PlayerId = 20, FirstName = "René", LastName = "van Broekhoven", DateOfBirth = new DateTime(1957, 3, 6), PreferredPosition = Position.CentralDefender},
                new Player {PlayerId = 21, FirstName = "Roel", LastName = "van Zoggel", DateOfBirth = null, PreferredPosition = Position.DefensiveMidfielder},
                new Player {PlayerId = 22, FirstName = "Eric", LastName = "van Rijsewijk", DateOfBirth = null, PreferredPosition = Position.Winger});

            context.Matches.AddOrUpdate(p => p.Date,
                new Match { MatchId = 1, Date = DateTime.Today, Opponent = new Team { TeamId = 1, Name = "Sarto", TeamNumber = 10 }, IsHomeMatch = false },
                new Match { MatchId = 2, Date = DateTime.Today.AddDays(-7), Opponent = new Team { TeamId = 2, Name = "SVSSS", TeamNumber = 4 }, IsHomeMatch = true },
                new Match { MatchId = 3, Date = DateTime.Today.AddDays(-14), Opponent = new Team { TeamId = 3, Name = "Wilhelmina Boys", TeamNumber = 8 }, IsHomeMatch = false },
                new Match { MatchId = 4, Date = DateTime.Today.AddDays(-21), Opponent = new Team { TeamId = 4, Name = "Spoordonkse Boys", TeamNumber = 3 }, IsHomeMatch = true },
                new Match { MatchId = 5, Date = DateTime.Today.AddDays(-28), Opponent = new Team { TeamId = 5, Name = "Oisterwijk", TeamNumber = 2 }, IsHomeMatch = false });
        }
    }
}
