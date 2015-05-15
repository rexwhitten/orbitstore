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
    builder.EntitySet<Binder>("Binders");
    builder.EntitySet<BinderResourceLink>("BinderResourceLink"); 
    builder.EntitySet<IdentityBinderLink>("IdentityBinderLink"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class BindersController : ODataController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: odata/Binders
        [EnableQuery]
        public IQueryable<Binder> GetBinders()
        {
            return db.Binder;
        }

        // GET: odata/Binders(5)
        [EnableQuery]
        public SingleResult<Binder> GetBinder([FromODataUri] int key)
        {
            return SingleResult.Create(db.Binder.Where(binder => binder.Id == key));
        }

        // PUT: odata/Binders(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Binder> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Binder binder = await db.Binder.FindAsync(key);
            if (binder == null)
            {
                return NotFound();
            }

            patch.Put(binder);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BinderExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(binder);
        }

        // POST: odata/Binders
        public async Task<IHttpActionResult> Post(Binder binder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Binder.Add(binder);
            await db.SaveChangesAsync();

            return Created(binder);
        }

        // PATCH: odata/Binders(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Binder> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Binder binder = await db.Binder.FindAsync(key);
            if (binder == null)
            {
                return NotFound();
            }

            patch.Patch(binder);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BinderExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(binder);
        }

        // DELETE: odata/Binders(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Binder binder = await db.Binder.FindAsync(key);
            if (binder == null)
            {
                return NotFound();
            }

            db.Binder.Remove(binder);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Binders(5)/BinderResourceLink
        [EnableQuery]
        public IQueryable<BinderResourceLink> GetBinderResourceLink([FromODataUri] int key)
        {
            return db.Binder.Where(m => m.Id == key).SelectMany(m => m.BinderResourceLink);
        }

        // GET: odata/Binders(5)/IdentityBinderLink
        [EnableQuery]
        public IQueryable<IdentityBinderLink> GetIdentityBinderLink([FromODataUri] int key)
        {
            return db.Binder.Where(m => m.Id == key).SelectMany(m => m.IdentityBinderLink);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BinderExists(int key)
        {
            return db.Binder.Count(e => e.Id == key) > 0;
        }
    }
}
