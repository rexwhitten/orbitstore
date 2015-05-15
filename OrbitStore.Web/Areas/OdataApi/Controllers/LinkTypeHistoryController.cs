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
    builder.EntitySet<LinkTypeHistory>("LinkTypeHistory");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class LinkTypeHistoryController : ODataController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: odata/LinkTypeHistory
        [EnableQuery]
        public IQueryable<LinkTypeHistory> GetLinkTypeHistory()
        {
            return db.LinkTypeHistory;
        }

        // GET: odata/LinkTypeHistory(5)
        [EnableQuery]
        public SingleResult<LinkTypeHistory> GetLinkTypeHistory([FromODataUri] long key)
        {
            return SingleResult.Create(db.LinkTypeHistory.Where(linkTypeHistory => linkTypeHistory.AuditId == key));
        }

        // PUT: odata/LinkTypeHistory(5)
        public async Task<IHttpActionResult> Put([FromODataUri] long key, Delta<LinkTypeHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LinkTypeHistory linkTypeHistory = await db.LinkTypeHistory.FindAsync(key);
            if (linkTypeHistory == null)
            {
                return NotFound();
            }

            patch.Put(linkTypeHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LinkTypeHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(linkTypeHistory);
        }

        // POST: odata/LinkTypeHistory
        public async Task<IHttpActionResult> Post(LinkTypeHistory linkTypeHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LinkTypeHistory.Add(linkTypeHistory);
            await db.SaveChangesAsync();

            return Created(linkTypeHistory);
        }

        // PATCH: odata/LinkTypeHistory(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<LinkTypeHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LinkTypeHistory linkTypeHistory = await db.LinkTypeHistory.FindAsync(key);
            if (linkTypeHistory == null)
            {
                return NotFound();
            }

            patch.Patch(linkTypeHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LinkTypeHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(linkTypeHistory);
        }

        // DELETE: odata/LinkTypeHistory(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            LinkTypeHistory linkTypeHistory = await db.LinkTypeHistory.FindAsync(key);
            if (linkTypeHistory == null)
            {
                return NotFound();
            }

            db.LinkTypeHistory.Remove(linkTypeHistory);
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

        private bool LinkTypeHistoryExists(long key)
        {
            return db.LinkTypeHistory.Count(e => e.AuditId == key) > 0;
        }
    }
}
