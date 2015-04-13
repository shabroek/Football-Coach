using System;
using System.Data.Entity;
using FootballCoach.WebAPI.Migrations;

namespace FootballCoach.WebAPI.Models
{
    public class FootballContextInitializer : MigrateDatabaseToLatestVersion<FootballContext, FootballContextConfiguration>
    {
    }
}