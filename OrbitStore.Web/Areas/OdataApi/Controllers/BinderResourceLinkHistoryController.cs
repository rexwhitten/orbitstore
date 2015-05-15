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
    builder.EntitySet<BinderResourceLinkHistory>("BinderResourceLinkHistory");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class BinderResourceLinkHistoryController : ODataController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: odata/BinderResourceLinkHistory
        [EnableQuery]
        public IQueryable<BinderResourceLinkHistory> GetBinderResourceLinkHistory()
        {
            return db.BinderResourceLinkHistory;
        }

        // GET: odata/BinderResourceLinkHistory(5)
        [EnableQuery]
        public SingleResult<BinderResourceLinkHistory> GetBinderResourceLinkHistory([FromODataUri] long key)
        {
            return SingleResult.Create(db.BinderResourceLinkHistory.Where(binderResourceLinkHistory => binderResourceLinkHistory.AuditId == key));
        }

        // PUT: odata/BinderResourceLinkHistory(5)
        public async Task<IHttpActionResult> Put([FromODataUri] long key, Delta<BinderResourceLinkHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BinderResourceLinkHistory binderResourceLinkHistory = await db.BinderResourceLinkHistory.FindAsync(key);
            if (binderResourceLinkHistory == null)
            {
                return NotFound();
            }

            patch.Put(binderResourceLinkHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BinderResourceLinkHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(binderResourceLinkHistory);
        }

        // POST: odata/BinderResourceLinkHistory
        public async Task<IHttpActionResult> Post(BinderResourceLinkHistory binderResourceLinkHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BinderResourceLinkHistory.Add(binderResourceLinkHistory);
            await db.SaveChangesAsync();

            return Created(binderResourceLinkHistory);
        }

        // PATCH: odata/BinderResourceLinkHistory(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<BinderResourceLinkHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BinderResourceLinkHistory binderResourceLinkHistory = await db.BinderResourceLinkHistory.FindAsync(key);
            if (binderResourceLinkHistory == null)
            {
                return NotFound();
            }

            patch.Patch(binderResourceLinkHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BinderResourceLinkHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(binderResourceLinkHistory);
        }

        // DELETE: odata/BinderResourceLinkHistory(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            BinderResourceLinkHistory binderResourceLinkHistory = await db.BinderResourceLinkHistory.FindAsync(key);
            if (binderResourceLinkHistory == null)
            {
                return NotFound();
            }

            db.BinderResourceLinkHistory.Remove(binderResourceLinkHistory);
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

        private bool BinderResourceLinkHistoryExists(long key)
        {
            return db.BinderResourceLinkHistory.Count(e => e.AuditId == key) > 0;
        }
    }
}
