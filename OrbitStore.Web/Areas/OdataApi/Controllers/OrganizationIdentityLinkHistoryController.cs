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
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using OrbitStore.Web.Data;

namespace OrbitStore.Web.Areas.OdataApi.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using OrbitStore.Web.Data;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<OrganizationIdentityLinkHistory>("OrganizationIdentityLinkHistory");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class OrganizationIdentityLinkHistoryController : ODataController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: odata/OrganizationIdentityLinkHistory
        [EnableQuery]
        public IQueryable<OrganizationIdentityLinkHistory> GetOrganizationIdentityLinkHistory()
        {
            return db.OrganizationIdentityLinkHistory;
        }

        // GET: odata/OrganizationIdentityLinkHistory(5)
        [EnableQuery]
        public SingleResult<OrganizationIdentityLinkHistory> GetOrganizationIdentityLinkHistory([FromODataUri] long key)
        {
            return SingleResult.Create(db.OrganizationIdentityLinkHistory.Where(organizationIdentityLinkHistory => organizationIdentityLinkHistory.AuditId == key));
        }

        // PUT: odata/OrganizationIdentityLinkHistory(5)
        public async Task<IHttpActionResult> Put([FromODataUri] long key, Delta<OrganizationIdentityLinkHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OrganizationIdentityLinkHistory organizationIdentityLinkHistory = await db.OrganizationIdentityLinkHistory.FindAsync(key);
            if (organizationIdentityLinkHistory == null)
            {
                return NotFound();
            }

            patch.Put(organizationIdentityLinkHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationIdentityLinkHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(organizationIdentityLinkHistory);
        }

        // POST: odata/OrganizationIdentityLinkHistory
        public async Task<IHttpActionResult> Post(OrganizationIdentityLinkHistory organizationIdentityLinkHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrganizationIdentityLinkHistory.Add(organizationIdentityLinkHistory);
            await db.SaveChangesAsync();

            return Created(organizationIdentityLinkHistory);
        }

        // PATCH: odata/OrganizationIdentityLinkHistory(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<OrganizationIdentityLinkHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OrganizationIdentityLinkHistory organizationIdentityLinkHistory = await db.OrganizationIdentityLinkHistory.FindAsync(key);
            if (organizationIdentityLinkHistory == null)
            {
                return NotFound();
            }

            patch.Patch(organizationIdentityLinkHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationIdentityLinkHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(organizationIdentityLinkHistory);
        }

        // DELETE: odata/OrganizationIdentityLinkHistory(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            OrganizationIdentityLinkHistory organizationIdentityLinkHistory = await db.OrganizationIdentityLinkHistory.FindAsync(key);
            if (organizationIdentityLinkHistory == null)
            {
                return NotFound();
            }

            db.OrganizationIdentityLinkHistory.Remove(organizationIdentityLinkHistory);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrganizationIdentityLinkHistoryExists(long key)
        {
            return db.OrganizationIdentityLinkHistory.Count(e => e.AuditId == key) > 0;
        }
    }
}
