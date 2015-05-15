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
    builder.EntitySet<ResourceHistory>("ResourceHistories");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ResourceHistoriesController : ODataController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: odata/ResourceHistories
        [EnableQuery]
        public IQueryable<ResourceHistory> GetResourceHistories()
        {
            return db.ResourceHistory;
        }

        // GET: odata/ResourceHistories(5)
        [EnableQuery]
        public SingleResult<ResourceHistory> GetResourceHistory([FromODataUri] long key)
        {
            return SingleResult.Create(db.ResourceHistory.Where(resourceHistory => resourceHistory.AuditId == key));
        }

        // PUT: odata/ResourceHistories(5)
        public async Task<IHttpActionResult> Put([FromODataUri] long key, Delta<ResourceHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResourceHistory resourceHistory = await db.ResourceHistory.FindAsync(key);
            if (resourceHistory == null)
            {
                return NotFound();
            }

            patch.Put(resourceHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(resourceHistory);
        }

        // POST: odata/ResourceHistories
        public async Task<IHttpActionResult> Post(ResourceHistory resourceHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ResourceHistory.Add(resourceHistory);
            await db.SaveChangesAsync();

            return Created(resourceHistory);
        }

        // PATCH: odata/ResourceHistories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<ResourceHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResourceHistory resourceHistory = await db.ResourceHistory.FindAsync(key);
            if (resourceHistory == null)
            {
                return NotFound();
            }

            patch.Patch(resourceHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(resourceHistory);
        }

        // DELETE: odata/ResourceHistories(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            ResourceHistory resourceHistory = await db.ResourceHistory.FindAsync(key);
            if (resourceHistory == null)
            {
                return NotFound();
            }

            db.ResourceHistory.Remove(resourceHistory);
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

        private bool ResourceHistoryExists(long key)
        {
            return db.ResourceHistory.Count(e => e.AuditId == key) > 0;
        }
    }
}
