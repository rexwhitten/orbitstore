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
    public class IdentityBinderLinkHistoryApiController : ApiController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: api/IdentityBinderLinkHistoryApi
        public IQueryable<IdentityBinderLinkHistory> GetIdentityBinderLinkHistory()
        {
            return db.IdentityBinderLinkHistory;
        }

        // GET: api/IdentityBinderLinkHistoryApi/5
        [ResponseType(typeof(IdentityBinderLinkHistory))]
        public async Task<IHttpActionResult> GetIdentityBinderLinkHistory(long id)
        {
            IdentityBinderLinkHistory identityBinderLinkHistory = await db.IdentityBinderLinkHistory.FindAsync(id);
            if (identityBinderLinkHistory == null)
            {
                return NotFound();
            }

            return Ok(identityBinderLinkHistory);
        }

        // PUT: api/IdentityBinderLinkHistoryApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutIdentityBinderLinkHistory(long id, IdentityBinderLinkHistory identityBinderLinkHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != identityBinderLinkHistory.AuditId)
            {
                return BadRequest();
            }

            db.Entry(identityBinderLinkHistory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdentityBinderLinkHistoryExists(id))
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

        // POST: api/IdentityBinderLinkHistoryApi
        [ResponseType(typeof(IdentityBinderLinkHistory))]
        public async Task<IHttpActionResult> PostIdentityBinderLinkHistory(IdentityBinderLinkHistory identityBinderLinkHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IdentityBinderLinkHistory.Add(identityBinderLinkHistory);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = identityBinderLinkHistory.AuditId }, identityBinderLinkHistory);
        }

        // DELETE: api/IdentityBinderLinkHistoryApi/5
        [ResponseType(typeof(IdentityBinderLinkHistory))]
        public async Task<IHttpActionResult> DeleteIdentityBinderLinkHistory(long id)
        {
            IdentityBinderLinkHistory identityBinderLinkHistory = await db.IdentityBinderLinkHistory.FindAsync(id);
            if (identityBinderLinkHistory == null)
            {
                return NotFound();
            }

            db.IdentityBinderLinkHistory.Remove(identityBinderLinkHistory);
            await db.SaveChangesAsync();

            return Ok(identityBinderLinkHistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IdentityBinderLinkHistoryExists(long id)
        {
            return db.IdentityBinderLinkHistory.Count(e => e.AuditId == id) > 0;
        }
    }
}