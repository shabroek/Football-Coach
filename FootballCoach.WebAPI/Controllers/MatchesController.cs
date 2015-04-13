using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using FootballCoach.Model;
using FootballCoach.WebAPI.Models;

namespace FootballCoach.WebAPI.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

   */
    public class MatchesController : ODataController
    {
        private FootballContext db = new FootballContext();

        // GET: odata/Matches
        [EnableQuery]
        public IQueryable<Match> GetMatches()
        {
            return db.Matches;
        }

        // GET: odata/Matches(5)
        [EnableQuery]
        public SingleResult<Match> GetMatch([FromODataUri] int key)
        {
            return SingleResult.Create(db.Matches.Where(match => match.MatchId == key));
        }

        // PUT: odata/Matches(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Match> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Match match = db.Matches.Find(key);
            if (match == null)
            {
                return NotFound();
            }

            patch.Put(match);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(match);
        }

        // POST: odata/Matches
        public IHttpActionResult Post(Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Matches.Add(match);
            db.SaveChanges();

            return Created(match);
        }

        // PATCH: odata/Matches(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Match> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Match match = db.Matches.Find(key);
            if (match == null)
            {
                return NotFound();
            }

            patch.Patch(match);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(match);
        }

        // DELETE: odata/Matches(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Match match = db.Matches.Find(key);
            if (match == null)
            {
                return NotFound();
            }

            db.Matches.Remove(match);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Matches(5)/Events
        [EnableQuery]
        public IQueryable<MatchEvent> GetEvents([FromODataUri] int key)
        {
            return db.Matches.Where(m => m.MatchId == key).SelectMany(m => m.Events);
        }

        // GET: odata/Matches(5)/Opponent
        [EnableQuery]
        public SingleResult<Team> GetOpponent([FromODataUri] int key)
        {
            return SingleResult.Create(db.Matches.Where(m => m.MatchId == key).Select(m => m.Opponent));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MatchExists(int key)
        {
            return db.Matches.Count(e => e.MatchId == key) > 0;
        }
    }
}
