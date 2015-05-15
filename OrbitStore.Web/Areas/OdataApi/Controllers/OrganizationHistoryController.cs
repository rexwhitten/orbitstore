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
    builder.EntitySet<OrganizationHistory>("OrganizationHistory");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class OrganizationHistoryController : ODataController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: odata/OrganizationHistory
        [EnableQuery]
        public IQueryable<OrganizationHistory> GetOrganizationHistory()
        {
            return db.OrganizationHistory;
        }

        // GET: odata/OrganizationHistory(5)
        [EnableQuery]
        public SingleResult<OrganizationHistory> GetOrganizationHistory([FromODataUri] long key)
        {
            return SingleResult.Create(db.OrganizationHistory.Where(organizationHistory => organizationHistory.AuditId == key));
        }

        // PUT: odata/OrganizationHistory(5)
        public async Task<IHttpActionResult> Put([FromODataUri] long key, Delta<OrganizationHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OrganizationHistory organizationHistory = await db.OrganizationHistory.FindAsync(key);
            if (organizationHistory == null)
            {
                return NotFound();
            }

            patch.Put(organizationHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(organizationHistory);
        }

        // POST: odata/OrganizationHistory
        public async Task<IHttpActionResult> Post(OrganizationHistory organizationHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrganizationHistory.Add(organizationHistory);
            await db.SaveChangesAsync();

            return Created(organizationHistory);
        }

        // PATCH: odata/OrganizationHistory(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<OrganizationHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OrganizationHistory organizationHistory = await db.OrganizationHistory.FindAsync(key);
            if (organizationHistory == null)
            {
                return NotFound();
            }

            patch.Patch(organizationHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(organizationHistory);
        }

        // DELETE: odata/OrganizationHistory(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            OrganizationHistory organizationHistory = await db.OrganizationHistory.FindAsync(key);
            if (organizationHistory == null)
            {
                return NotFound();
            }

            db.OrganizationHistory.Remove(organizationHistory);
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

        private bool OrganizationHistoryExists(long key)
        {
            return db.OrganizationHistory.Count(e => e.AuditId == key) > 0;
        }
    }
}
