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
    builder.EntitySet<Resource>("Resources");
    builder.EntitySet<BinderResourceLink>("BinderResourceLink"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ResourcesController : ODataController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: odata/Resources
        [EnableQuery]
        public IQueryable<Resource> GetResources()
        {
            return db.Resource;
        }

        // GET: odata/Resources(5)
        [EnableQuery]
        public SingleResult<Resource> GetResource([FromODataUri] int key)
        {
            return SingleResult.Create(db.Resource.Where(resource => resource.Id == key));
        }

        // PUT: odata/Resources(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Resource> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Resource resource = await db.Resource.FindAsync(key);
            if (resource == null)
            {
                return NotFound();
            }

            patch.Put(resource);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(resource);
        }

        // POST: odata/Resources
        public async Task<IHttpActionResult> Post(Resource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Resource.Add(resource);
            await db.SaveChangesAsync();

            return Created(resource);
        }

        // PATCH: odata/Resources(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Resource> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Resource resource = await db.Resource.FindAsync(key);
            if (resource == null)
            {
                return NotFound();
            }

            patch.Patch(resource);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(resource);
        }

        // DELETE: odata/Resources(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Resource resource = await db.Resource.FindAsync(key);
            if (resource == null)
            {
                return NotFound();
            }

            db.Resource.Remove(resource);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Resources(5)/BinderResourceLink
        [EnableQuery]
        public IQueryable<BinderResourceLink> GetBinderResourceLink([FromODataUri] int key)
        {
            return db.Resource.Where(m => m.Id == key).SelectMany(m => m.BinderResourceLink);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResourceExists(int key)
        {
            return db.Resource.Count(e => e.Id == key) > 0;
        }
    }
}
