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
    public class BinderHistoryApiController : ApiController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: api/BinderHistoryApi
        public IQueryable<BinderHistory> GetBinderHistory()
        {
            return db.BinderHistory;
        }

        // GET: api/BinderHistoryApi/5
        [ResponseType(typeof(BinderHistory))]
        public async Task<IHttpActionResult> GetBinderHistory(long id)
        {
            BinderHistory binderHistory = await db.BinderHistory.FindAsync(id);
            if (binderHistory == null)
            {
                return NotFound();
            }

            return Ok(binderHistory);
        }

        // PUT: api/BinderHistoryApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBinderHistory(long id, BinderHistory binderHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != binderHistory.AuditId)
            {
                return BadRequest();
            }

            db.Entry(binderHistory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BinderHistoryExists(id))
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

        // POST: api/BinderHistoryApi
        [ResponseType(typeof(BinderHistory))]
        public async Task<IHttpActionResult> PostBinderHistory(BinderHistory binderHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BinderHistory.Add(binderHistory);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = binderHistory.AuditId }, binderHistory);
        }

        // DELETE: api/BinderHistoryApi/5
        [ResponseType(typeof(BinderHistory))]
        public async Task<IHttpActionResult> DeleteBinderHistory(long id)
        {
            BinderHistory binderHistory = await db.BinderHistory.FindAsync(id);
            if (binderHistory == null)
            {
                return NotFound();
            }

            db.BinderHistory.Remove(binderHistory);
            await db.SaveChangesAsync();

            return Ok(binderHistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BinderHistoryExists(long id)
        {
            return db.BinderHistory.Count(e => e.AuditId == id) > 0;
        }
    }
}