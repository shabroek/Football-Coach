using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData;
using FootballCoach.Model;
using FootballCoach.WebAPI.Models;

namespace FootballCoach.WebAPI.Controllers
{
    public class TeamsController : ODataController
    {
        private FootballContext db = new FootballContext();

        // GET: odata/Teams
        [EnableQuery]
        public IQueryable<Team> GetTeams()
        {
            return db.Teams;
        }

        // GET: odata/Teams(5)
        [EnableQuery]
        public SingleResult<Team> GetTeam([FromODataUri] int key)
        {
            return SingleResult.Create(db.Teams.Where(team => team.TeamId == key));
        }

        // PUT: odata/Teams(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Team> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Team team = db.Teams.Find(key);
            if (team == null)
            {
                return NotFound();
            }

            patch.Put(team);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(team);
        }

        // POST: odata/Teams
        public IHttpActionResult Post(Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teams.Add(team);
            db.SaveChanges();

            return Created(team);
        }

        // PATCH: odata/Teams(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Team> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Team team = db.Teams.Find(key);
            if (team == null)
            {
                return NotFound();
            }

            patch.Patch(team);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(team);
        }

        // DELETE: odata/Teams(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Team team = db.Teams.Find(key);
            if (team == null)
            {
                return NotFound();
            }

            db.Teams.Remove(team);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Teams(5)/Matches
        [EnableQuery]
        public IQueryable<Match> GetMatches([FromODataUri] int key)
        {
            return db.Teams.Where(m => m.TeamId == key).SelectMany(m => m.Matches);
        }

        // GET: odata/Teams(5)/Players
        [EnableQuery]
        public IQueryable<Player> GetPlayers([FromODataUri] int key)
        {
            return db.Teams.Where(m => m.TeamId == key).SelectMany(m => m.Players);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamExists(int key)
        {
            return db.Teams.Count(e => e.TeamId == key) > 0;
        }
    }
}
