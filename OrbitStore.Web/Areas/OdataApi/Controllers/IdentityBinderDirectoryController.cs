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
    builder.EntitySet<IdentityBinderDirectory>("IdentityBinderDirectory");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class IdentityBinderDirectoryController : ODataController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: odata/IdentityBinderDirectory
        [EnableQuery]
        public IQueryable<IdentityBinderDirectory> GetIdentityBinderDirectory()
        {
            return db.IdentityBinderDirectory;
        }

        // GET: odata/IdentityBinderDirectory(5)
        [EnableQuery]
        public SingleResult<IdentityBinderDirectory> GetIdentityBinderDirectory([FromODataUri] int key)
        {
            return SingleResult.Create(db.IdentityBinderDirectory.Where(identityBinderDirectory => identityBinderDirectory.IdentityId == key));
        }

        // PUT: odata/IdentityBinderDirectory(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<IdentityBinderDirectory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityBinderDirectory identityBinderDirectory = await db.IdentityBinderDirectory.FindAsync(key);
            if (identityBinderDirectory == null)
            {
                return NotFound();
            }

            patch.Put(identityBinderDirectory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdentityBinderDirectoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(identityBinderDirectory);
        }

        // POST: odata/IdentityBinderDirectory
        public async Task<IHttpActionResult> Post(IdentityBinderDirectory identityBinderDirectory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IdentityBinderDirectory.Add(identityBinderDirectory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IdentityBinderDirectoryExists(identityBinderDirectory.IdentityId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(identityBinderDirectory);
        }

        // PATCH: odata/IdentityBinderDirectory(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<IdentityBinderDirectory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityBinderDirectory identityBinderDirectory = await db.IdentityBinderDirectory.FindAsync(key);
            if (identityBinderDirectory == null)
            {
                return NotFound();
            }

            patch.Patch(identityBinderDirectory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdentityBinderDirectoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(identityBinderDirectory);
        }

        // DELETE: odata/IdentityBinderDirectory(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            IdentityBinderDirectory identityBinderDirectory = await db.IdentityBinderDirectory.FindAsync(key);
            if (identityBinderDirectory == null)
            {
                return NotFound();
            }

            db.IdentityBinderDirectory.Remove(identityBinderDirectory);
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

        private bool IdentityBinderDirectoryExists(int key)
        {
            return db.IdentityBinderDirectory.Count(e => e.IdentityId == key) > 0;
        }
    }
}
