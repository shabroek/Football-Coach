using System;
using System.Data.Entity;
using FootballCoach.WebAPI.Migrations;

namespace FootballCoach.WebAPI.Models
{
    public class FootballContextWithAutomaticMigrationInitializer : MigrateDatabaseToLatestVersion<FootballContext, FootballContextConfiguration>
    {
    }

    public class FootballContextInitializer : DropCreateDatabaseAlways<FootballContext>
    {
    }
}