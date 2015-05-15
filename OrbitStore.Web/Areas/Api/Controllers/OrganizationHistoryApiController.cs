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
    public class OrganizationHistoryApiController : ApiController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: api/OrganizationHistoryApi
        public IQueryable<OrganizationHistory> GetOrganizationHistory()
        {
            return db.OrganizationHistory;
        }

        // GET: api/OrganizationHistoryApi/5
        [ResponseType(typeof(OrganizationHistory))]
        public async Task<IHttpActionResult> GetOrganizationHistory(long id)
        {
            OrganizationHistory organizationHistory = await db.OrganizationHistory.FindAsync(id);
            if (organizationHistory == null)
            {
                return NotFound();
            }

            return Ok(organizationHistory);
        }

        // PUT: api/OrganizationHistoryApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrganizationHistory(long id, OrganizationHistory organizationHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != organizationHistory.AuditId)
            {
                return BadRequest();
            }

            db.Entry(organizationHistory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationHistoryExists(id))
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

        // POST: api/OrganizationHistoryApi
        [ResponseType(typeof(OrganizationHistory))]
        public async Task<IHttpActionResult> PostOrganizationHistory(OrganizationHistory organizationHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrganizationHistory.Add(organizationHistory);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = organizationHistory.AuditId }, organizationHistory);
        }

        // DELETE: api/OrganizationHistoryApi/5
        [ResponseType(typeof(OrganizationHistory))]
        public async Task<IHttpActionResult> DeleteOrganizationHistory(long id)
        {
            OrganizationHistory organizationHistory = await db.OrganizationHistory.FindAsync(id);
            if (organizationHistory == null)
            {
                return NotFound();
            }

            db.OrganizationHistory.Remove(organizationHistory);
            await db.SaveChangesAsync();

            return Ok(organizationHistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrganizationHistoryExists(long id)
        {
            return db.OrganizationHistory.Count(e => e.AuditId == id) > 0;
        }
    }
}