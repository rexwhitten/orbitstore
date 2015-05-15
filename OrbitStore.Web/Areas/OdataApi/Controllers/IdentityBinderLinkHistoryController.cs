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
    builder.EntitySet<IdentityBinderLinkHistory>("IdentityBinderLinkHistory");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class IdentityBinderLinkHistoryController : ODataController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: odata/IdentityBinderLinkHistory
        [EnableQuery]
        public IQueryable<IdentityBinderLinkHistory> GetIdentityBinderLinkHistory()
        {
            return db.IdentityBinderLinkHistory;
        }

        // GET: odata/IdentityBinderLinkHistory(5)
        [EnableQuery]
        public SingleResult<IdentityBinderLinkHistory> GetIdentityBinderLinkHistory([FromODataUri] long key)
        {
            return SingleResult.Create(db.IdentityBinderLinkHistory.Where(identityBinderLinkHistory => identityBinderLinkHistory.AuditId == key));
        }

        // PUT: odata/IdentityBinderLinkHistory(5)
        public async Task<IHttpActionResult> Put([FromODataUri] long key, Delta<IdentityBinderLinkHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityBinderLinkHistory identityBinderLinkHistory = await db.IdentityBinderLinkHistory.FindAsync(key);
            if (identityBinderLinkHistory == null)
            {
                return NotFound();
            }

            patch.Put(identityBinderLinkHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdentityBinderLinkHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(identityBinderLinkHistory);
        }

        // POST: odata/IdentityBinderLinkHistory
        public async Task<IHttpActionResult> Post(IdentityBinderLinkHistory identityBinderLinkHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IdentityBinderLinkHistory.Add(identityBinderLinkHistory);
            await db.SaveChangesAsync();

            return Created(identityBinderLinkHistory);
        }

        // PATCH: odata/IdentityBinderLinkHistory(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<IdentityBinderLinkHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityBinderLinkHistory identityBinderLinkHistory = await db.IdentityBinderLinkHistory.FindAsync(key);
            if (identityBinderLinkHistory == null)
            {
                return NotFound();
            }

            patch.Patch(identityBinderLinkHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdentityBinderLinkHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(identityBinderLinkHistory);
        }

        // DELETE: odata/IdentityBinderLinkHistory(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            IdentityBinderLinkHistory identityBinderLinkHistory = await db.IdentityBinderLinkHistory.FindAsync(key);
            if (identityBinderLinkHistory == null)
            {
                return NotFound();
            }

            db.IdentityBinderLinkHistory.Remove(identityBinderLinkHistory);
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

        private bool IdentityBinderLinkHistoryExists(long key)
        {
            return db.IdentityBinderLinkHistory.Count(e => e.AuditId == key) > 0;
        }
    }
}
