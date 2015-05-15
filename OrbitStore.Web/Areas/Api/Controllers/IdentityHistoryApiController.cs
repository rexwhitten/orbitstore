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
    public class IdentityHistoryApiController : ApiController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: api/IdentityHistoryApi
        public IQueryable<IdentityHistory> GetIdentityHistory()
        {
            return db.IdentityHistory;
        }

        // GET: api/IdentityHistoryApi/5
        [ResponseType(typeof(IdentityHistory))]
        public async Task<IHttpActionResult> GetIdentityHistory(long id)
        {
            IdentityHistory identityHistory = await db.IdentityHistory.FindAsync(id);
            if (identityHistory == null)
            {
                return NotFound();
            }

            return Ok(identityHistory);
        }

        // PUT: api/IdentityHistoryApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutIdentityHistory(long id, IdentityHistory identityHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != identityHistory.AuditId)
            {
                return BadRequest();
            }

            db.Entry(identityHistory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdentityHistoryExists(id))
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

        // POST: api/IdentityHistoryApi
        [ResponseType(typeof(IdentityHistory))]
        public async Task<IHttpActionResult> PostIdentityHistory(IdentityHistory identityHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IdentityHistory.Add(identityHistory);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = identityHistory.AuditId }, identityHistory);
        }

        // DELETE: api/IdentityHistoryApi/5
        [ResponseType(typeof(IdentityHistory))]
        public async Task<IHttpActionResult> DeleteIdentityHistory(long id)
        {
            IdentityHistory identityHistory = await db.IdentityHistory.FindAsync(id);
            if (identityHistory == null)
            {
                return NotFound();
            }

            db.IdentityHistory.Remove(identityHistory);
            await db.SaveChangesAsync();

            return Ok(identityHistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IdentityHistoryExists(long id)
        {
            return db.IdentityHistory.Count(e => e.AuditId == id) > 0;
        }
    }
}