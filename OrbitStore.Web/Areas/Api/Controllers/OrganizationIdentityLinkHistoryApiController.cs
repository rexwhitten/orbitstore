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
    public class OrganizationIdentityLinkHistoryApiController : ApiController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: api/OrganizationIdentityLinkHistoryApi
        public IQueryable<OrganizationIdentityLinkHistory> GetOrganizationIdentityLinkHistory()
        {
            return db.OrganizationIdentityLinkHistory;
        }

        // GET: api/OrganizationIdentityLinkHistoryApi/5
        [ResponseType(typeof(OrganizationIdentityLinkHistory))]
        public async Task<IHttpActionResult> GetOrganizationIdentityLinkHistory(long id)
        {
            OrganizationIdentityLinkHistory organizationIdentityLinkHistory = await db.OrganizationIdentityLinkHistory.FindAsync(id);
            if (organizationIdentityLinkHistory == null)
            {
                return NotFound();
            }

            return Ok(organizationIdentityLinkHistory);
        }

        // PUT: api/OrganizationIdentityLinkHistoryApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrganizationIdentityLinkHistory(long id, OrganizationIdentityLinkHistory organizationIdentityLinkHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != organizationIdentityLinkHistory.AuditId)
            {
                return BadRequest();
            }

            db.Entry(organizationIdentityLinkHistory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationIdentityLinkHistoryExists(id))
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

        // POST: api/OrganizationIdentityLinkHistoryApi
        [ResponseType(typeof(OrganizationIdentityLinkHistory))]
        public async Task<IHttpActionResult> PostOrganizationIdentityLinkHistory(OrganizationIdentityLinkHistory organizationIdentityLinkHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrganizationIdentityLinkHistory.Add(organizationIdentityLinkHistory);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = organizationIdentityLinkHistory.AuditId }, organizationIdentityLinkHistory);
        }

        // DELETE: api/OrganizationIdentityLinkHistoryApi/5
        [ResponseType(typeof(OrganizationIdentityLinkHistory))]
        public async Task<IHttpActionResult> DeleteOrganizationIdentityLinkHistory(long id)
        {
            OrganizationIdentityLinkHistory organizationIdentityLinkHistory = await db.OrganizationIdentityLinkHistory.FindAsync(id);
            if (organizationIdentityLinkHistory == null)
            {
                return NotFound();
            }

            db.OrganizationIdentityLinkHistory.Remove(organizationIdentityLinkHistory);
            await db.SaveChangesAsync();

            return Ok(organizationIdentityLinkHistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrganizationIdentityLinkHistoryExists(long id)
        {
            return db.OrganizationIdentityLinkHistory.Count(e => e.AuditId == id) > 0;
        }
    }
}