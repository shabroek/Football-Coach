using System.Web.Http;
using FootballCoach.WebAPI.Models;

namespace FootballCoach.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            System.Data.Entity.Database.SetInitializer(new FootballContextWithAutomaticMigrationInitializer());
        }
    }
}
