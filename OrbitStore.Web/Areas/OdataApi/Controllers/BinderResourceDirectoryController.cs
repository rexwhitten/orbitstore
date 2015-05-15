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
    builder.EntitySet<BinderResourceDirectory>("BinderResourceDirectory");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class BinderResourceDirectoryController : ODataController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: odata/BinderResourceDirectory
        [EnableQuery]
        public IQueryable<BinderResourceDirectory> GetBinderResourceDirectory()
        {
            return db.BinderResourceDirectory;
        }

        // GET: odata/BinderResourceDirectory(5)
        [EnableQuery]
        public SingleResult<BinderResourceDirectory> GetBinderResourceDirectory([FromODataUri] int key)
        {
            return SingleResult.Create(db.BinderResourceDirectory.Where(binderResourceDirectory => binderResourceDirectory.BinderId == key));
        }

        // PUT: odata/BinderResourceDirectory(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<BinderResourceDirectory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BinderResourceDirectory binderResourceDirectory = await db.BinderResourceDirectory.FindAsync(key);
            if (binderResourceDirectory == null)
            {
                return NotFound();
            }

            patch.Put(binderResourceDirectory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BinderResourceDirectoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(binderResourceDirectory);
        }

        // POST: odata/BinderResourceDirectory
        public async Task<IHttpActionResult> Post(BinderResourceDirectory binderResourceDirectory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BinderResourceDirectory.Add(binderResourceDirectory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BinderResourceDirectoryExists(binderResourceDirectory.BinderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(binderResourceDirectory);
        }

        // PATCH: odata/BinderResourceDirectory(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<BinderResourceDirectory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BinderResourceDirectory binderResourceDirectory = await db.BinderResourceDirectory.FindAsync(key);
            if (binderResourceDirectory == null)
            {
                return NotFound();
            }

            patch.Patch(binderResourceDirectory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BinderResourceDirectoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(binderResourceDirectory);
        }

        // DELETE: odata/BinderResourceDirectory(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            BinderResourceDirectory binderResourceDirectory = await db.BinderResourceDirectory.FindAsync(key);
            if (binderResourceDirectory == null)
            {
                return NotFound();
            }

            db.BinderResourceDirectory.Remove(binderResourceDirectory);
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

        private bool BinderResourceDirectoryExists(int key)
        {
            return db.BinderResourceDirectory.Count(e => e.BinderId == key) > 0;
        }
    }
}
