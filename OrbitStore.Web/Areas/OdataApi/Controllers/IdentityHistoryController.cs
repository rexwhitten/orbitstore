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
    builder.EntitySet<IdentityHistory>("IdentityHistory");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class IdentityHistoryController : ODataController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: odata/IdentityHistory
        [EnableQuery]
        public IQueryable<IdentityHistory> GetIdentityHistory()
        {
            return db.IdentityHistory;
        }

        // GET: odata/IdentityHistory(5)
        [EnableQuery]
        public SingleResult<IdentityHistory> GetIdentityHistory([FromODataUri] long key)
        {
            return SingleResult.Create(db.IdentityHistory.Where(identityHistory => identityHistory.AuditId == key));
        }

        // PUT: odata/IdentityHistory(5)
        public async Task<IHttpActionResult> Put([FromODataUri] long key, Delta<IdentityHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityHistory identityHistory = await db.IdentityHistory.FindAsync(key);
            if (identityHistory == null)
            {
                return NotFound();
            }

            patch.Put(identityHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdentityHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(identityHistory);
        }

        // POST: odata/IdentityHistory
        public async Task<IHttpActionResult> Post(IdentityHistory identityHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IdentityHistory.Add(identityHistory);
            await db.SaveChangesAsync();

            return Created(identityHistory);
        }

        // PATCH: odata/IdentityHistory(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<IdentityHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityHistory identityHistory = await db.IdentityHistory.FindAsync(key);
            if (identityHistory == null)
            {
                return NotFound();
            }

            patch.Patch(identityHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdentityHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(identityHistory);
        }

        // DELETE: odata/IdentityHistory(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            IdentityHistory identityHistory = await db.IdentityHistory.FindAsync(key);
            if (identityHistory == null)
            {
                return NotFound();
            }

            db.IdentityHistory.Remove(identityHistory);
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

        private bool IdentityHistoryExists(long key)
        {
            return db.IdentityHistory.Count(e => e.AuditId == key) > 0;
        }
    }
}
