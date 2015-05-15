using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using OrbitStore.Web.Data;

namespace OrbitStore.Web.Areas.Api.Controllers
{
    public class ResourceHistoryApiController : ApiController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: api/ResourceHistoryApi
        public IQueryable<ResourceHistory> GetResourceHistory()
        {
            return db.ResourceHistory;
        }

        // GET: api/ResourceHistoryApi/5
        [ResponseType(typeof(ResourceHistory))]
        public async Task<IHttpActionResult> GetResourceHistory(long id)
        {
            ResourceHistory resourceHistory = await db.ResourceHistory.FindAsync(id);
            if (resourceHistory == null)
            {
                return NotFound();
            }

            return Ok(resourceHistory);
        }

        // PUT: api/ResourceHistoryApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutResourceHistory(long id, ResourceHistory resourceHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resourceHistory.AuditId)
            {
                return BadRequest();
            }

            db.Entry(resourceHistory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceHistoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ResourceHistoryApi
        [ResponseType(typeof(ResourceHistory))]
        public async Task<IHttpActionResult> PostResourceHistory(ResourceHistory resourceHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ResourceHistory.Add(resourceHistory);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = resourceHistory.AuditId }, resourceHistory);
        }

        // DELETE: api/ResourceHistoryApi/5
        [ResponseType(typeof(ResourceHistory))]
        public async Task<IHttpActionResult> DeleteResourceHistory(long id)
        {
            ResourceHistory resourceHistory = await db.ResourceHistory.FindAsync(id);
            if (resourceHistory == null)
            {
                return NotFound();
            }

            db.ResourceHistory.Remove(resourceHistory);
            await db.SaveChangesAsync();

            return Ok(resourceHistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResourceHistoryExists(long id)
        {
            return db.ResourceHistory.Count(e => e.AuditId == id) > 0;
        }
    }
}