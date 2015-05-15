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
    builder.EntitySet<BinderResourceLink>("BinderResourceLink");
    builder.EntitySet<Binder>("Binder"); 
    builder.EntitySet<Resource>("Resource"); 
    builder.EntitySet<LinkType>("LinkType"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class BinderResourceLinkController : ODataController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: odata/BinderResourceLink
        [EnableQuery]
        public IQueryable<BinderResourceLink> GetBinderResourceLink()
        {
            return db.BinderResourceLink;
        }

        // GET: odata/BinderResourceLink(5)
        [EnableQuery]
        public SingleResult<BinderResourceLink> GetBinderResourceLink([FromODataUri] int key)
        {
            return SingleResult.Create(db.BinderResourceLink.Where(binderResourceLink => binderResourceLink.Id == key));
        }

        // PUT: odata/BinderResourceLink(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<BinderResourceLink> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BinderResourceLink binderResourceLink = await db.BinderResourceLink.FindAsync(key);
            if (binderResourceLink == null)
            {
                return NotFound();
            }

            patch.Put(binderResourceLink);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BinderResourceLinkExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(binderResourceLink);
        }

        // POST: odata/BinderResourceLink
        public async Task<IHttpActionResult> Post(BinderResourceLink binderResourceLink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BinderResourceLink.Add(binderResourceLink);
            await db.SaveChangesAsync();

            return Created(binderResourceLink);
        }

        // PATCH: odata/BinderResourceLink(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<BinderResourceLink> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BinderResourceLink binderResourceLink = await db.BinderResourceLink.FindAsync(key);
            if (binderResourceLink == null)
            {
                return NotFound();
            }

            patch.Patch(binderResourceLink);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BinderResourceLinkExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(binderResourceLink);
        }

        // DELETE: odata/BinderResourceLink(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            BinderResourceLink binderResourceLink = await db.BinderResourceLink.FindAsync(key);
            if (binderResourceLink == null)
            {
                return NotFound();
            }

            db.BinderResourceLink.Remove(binderResourceLink);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/BinderResourceLink(5)/Binder
        [EnableQuery]
        public SingleResult<Binder> GetBinder([FromODataUri] int key)
        {
            return SingleResult.Create(db.BinderResourceLink.Where(m => m.Id == key).Select(m => m.Binder));
        }

        // GET: odata/BinderResourceLink(5)/Resource
        [EnableQuery]
        public SingleResult<Resource> GetResource([FromODataUri] int key)
        {
            return SingleResult.Create(db.BinderResourceLink.Where(m => m.Id == key).Select(m => m.Resource));
        }

        // GET: odata/BinderResourceLink(5)/LinkType
        [EnableQuery]
        public SingleResult<LinkType> GetLinkType([FromODataUri] int key)
        {
            return SingleResult.Create(db.BinderResourceLink.Where(m => m.Id == key).Select(m => m.LinkType));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BinderResourceLinkExists(int key)
        {
            return db.BinderResourceLink.Count(e => e.Id == key) > 0;
        }
    }
}
