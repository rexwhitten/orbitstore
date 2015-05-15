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
    public class BinderResourceLinkHistoryApiController : ApiController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: api/BinderResourceLinkHistoryApi
        public IQueryable<BinderResourceLinkHistory> GetBinderResourceLinkHistory()
        {
            return db.BinderResourceLinkHistory;
        }

        // GET: api/BinderResourceLinkHistoryApi/5
        [ResponseType(typeof(BinderResourceLinkHistory))]
        public async Task<IHttpActionResult> GetBinderResourceLinkHistory(long id)
        {
            BinderResourceLinkHistory binderResourceLinkHistory = await db.BinderResourceLinkHistory.FindAsync(id);
            if (binderResourceLinkHistory == null)
            {
                return NotFound();
            }

            return Ok(binderResourceLinkHistory);
        }

        // PUT: api/BinderResourceLinkHistoryApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBinderResourceLinkHistory(long id, BinderResourceLinkHistory binderResourceLinkHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != binderResourceLinkHistory.AuditId)
            {
                return BadRequest();
            }

            db.Entry(binderResourceLinkHistory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BinderResourceLinkHistoryExists(id))
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

        // POST: api/BinderResourceLinkHistoryApi
        [ResponseType(typeof(BinderResourceLinkHistory))]
        public async Task<IHttpActionResult> PostBinderResourceLinkHistory(BinderResourceLinkHistory binderResourceLinkHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BinderResourceLinkHistory.Add(binderResourceLinkHistory);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = binderResourceLinkHistory.AuditId }, binderResourceLinkHistory);
        }

        // DELETE: api/BinderResourceLinkHistoryApi/5
        [ResponseType(typeof(BinderResourceLinkHistory))]
        public async Task<IHttpActionResult> DeleteBinderResourceLinkHistory(long id)
        {
            BinderResourceLinkHistory binderResourceLinkHistory = await db.BinderResourceLinkHistory.FindAsync(id);
            if (binderResourceLinkHistory == null)
            {
                return NotFound();
            }

            db.BinderResourceLinkHistory.Remove(binderResourceLinkHistory);
            await db.SaveChangesAsync();

            return Ok(binderResourceLinkHistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BinderResourceLinkHistoryExists(long id)
        {
            return db.BinderResourceLinkHistory.Count(e => e.AuditId == id) > 0;
        }
    }
}