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
    builder.EntitySet<IdentityBinderLink>("IdentityBinderLink");
    builder.EntitySet<Binder>("Binder"); 
    builder.EntitySet<Identity>("Identity"); 
    builder.EntitySet<LinkType>("LinkType"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class IdentityBinderLinkController : ODataController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: odata/IdentityBinderLink
        [EnableQuery]
        public IQueryable<IdentityBinderLink> GetIdentityBinderLink()
        {
            return db.IdentityBinderLink;
        }

        // GET: odata/IdentityBinderLink(5)
        [EnableQuery]
        public SingleResult<IdentityBinderLink> GetIdentityBinderLink([FromODataUri] int key)
        {
            return SingleResult.Create(db.IdentityBinderLink.Where(identityBinderLink => identityBinderLink.Id == key));
        }

        // PUT: odata/IdentityBinderLink(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<IdentityBinderLink> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityBinderLink identityBinderLink = await db.IdentityBinderLink.FindAsync(key);
            if (identityBinderLink == null)
            {
                return NotFound();
            }

            patch.Put(identityBinderLink);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdentityBinderLinkExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(identityBinderLink);
        }

        // POST: odata/IdentityBinderLink
        public async Task<IHttpActionResult> Post(IdentityBinderLink identityBinderLink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IdentityBinderLink.Add(identityBinderLink);
            await db.SaveChangesAsync();

            return Created(identityBinderLink);
        }

        // PATCH: odata/IdentityBinderLink(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<IdentityBinderLink> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityBinderLink identityBinderLink = await db.IdentityBinderLink.FindAsync(key);
            if (identityBinderLink == null)
            {
                return NotFound();
            }

            patch.Patch(identityBinderLink);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdentityBinderLinkExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(identityBinderLink);
        }

        // DELETE: odata/IdentityBinderLink(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            IdentityBinderLink identityBinderLink = await db.IdentityBinderLink.FindAsync(key);
            if (identityBinderLink == null)
            {
                return NotFound();
            }

            db.IdentityBinderLink.Remove(identityBinderLink);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/IdentityBinderLink(5)/Binder
        [EnableQuery]
        public SingleResult<Binder> GetBinder([FromODataUri] int key)
        {
            return SingleResult.Create(db.IdentityBinderLink.Where(m => m.Id == key).Select(m => m.Binder));
        }

        // GET: odata/IdentityBinderLink(5)/Identity
        [EnableQuery]
        public SingleResult<Identity> GetIdentity([FromODataUri] int key)
        {
            return SingleResult.Create(db.IdentityBinderLink.Where(m => m.Id == key).Select(m => m.Identity));
        }

        // GET: odata/IdentityBinderLink(5)/LinkType
        [EnableQuery]
        public SingleResult<LinkType> GetLinkType([FromODataUri] int key)
        {
            return SingleResult.Create(db.IdentityBinderLink.Where(m => m.Id == key).Select(m => m.LinkType));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IdentityBinderLinkExists(int key)
        {
            return db.IdentityBinderLink.Count(e => e.Id == key) > 0;
        }
    }
}
