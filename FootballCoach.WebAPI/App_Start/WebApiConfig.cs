using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using FootballCoach.Model;
using FootballCoach.WebAPI.Models;

namespace FootballCoach.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Player>("Players");
            builder.EntitySet<Team>("Teams");
            builder.EntitySet<Match>("Matches");
            builder.EntitySet<MatchEvent>("MatchEvents");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
