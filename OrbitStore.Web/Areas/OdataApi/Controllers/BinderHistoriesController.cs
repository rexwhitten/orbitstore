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
    builder.EntitySet<BinderHistory>("BinderHistories");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class BinderHistoriesController : ODataController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: odata/BinderHistories
        [EnableQuery]
        public IQueryable<BinderHistory> GetBinderHistories()
        {
            return db.BinderHistory;
        }

        // GET: odata/BinderHistories(5)
        [EnableQuery]
        public SingleResult<BinderHistory> GetBinderHistory([FromODataUri] long key)
        {
            return SingleResult.Create(db.BinderHistory.Where(binderHistory => binderHistory.AuditId == key));
        }

        // PUT: odata/BinderHistories(5)
        public async Task<IHttpActionResult> Put([FromODataUri] long key, Delta<BinderHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BinderHistory binderHistory = await db.BinderHistory.FindAsync(key);
            if (binderHistory == null)
            {
                return NotFound();
            }

            patch.Put(binderHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BinderHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(binderHistory);
        }

        // POST: odata/BinderHistories
        public async Task<IHttpActionResult> Post(BinderHistory binderHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BinderHistory.Add(binderHistory);
            await db.SaveChangesAsync();

            return Created(binderHistory);
        }

        // PATCH: odata/BinderHistories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<BinderHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BinderHistory binderHistory = await db.BinderHistory.FindAsync(key);
            if (binderHistory == null)
            {
                return NotFound();
            }

            patch.Patch(binderHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BinderHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(binderHistory);
        }

        // DELETE: odata/BinderHistories(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            BinderHistory binderHistory = await db.BinderHistory.FindAsync(key);
            if (binderHistory == null)
            {
                return NotFound();
            }

            db.BinderHistory.Remove(binderHistory);
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

        private bool BinderHistoryExists(long key)
        {
            return db.BinderHistory.Count(e => e.AuditId == key) > 0;
        }
    }
}
