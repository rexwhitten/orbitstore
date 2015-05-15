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
    builder.EntitySet<Identity>("Identities");
    builder.EntitySet<IdentityBinderLink>("IdentityBinderLink"); 
    builder.EntitySet<OrganizationIdentityLink>("OrganizationIdentityLink"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class IdentitiesController : ODataController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: odata/Identities
        [EnableQuery]
        public IQueryable<Identity> GetIdentities()
        {
            return db.Identity;
        }

        // GET: odata/Identities(5)
        [EnableQuery]
        public SingleResult<Identity> GetIdentity([FromODataUri] int key)
        {
            return SingleResult.Create(db.Identity.Where(identity => identity.Id == key));
        }

        // PUT: odata/Identities(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Identity> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Identity identity = await db.Identity.FindAsync(key);
            if (identity == null)
            {
                return NotFound();
            }

            patch.Put(identity);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdentityExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(identity);
        }

        // POST: odata/Identities
        public async Task<IHttpActionResult> Post(Identity identity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Identity.Add(identity);
            await db.SaveChangesAsync();

            return Created(identity);
        }

        // PATCH: odata/Identities(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Identity> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Identity identity = await db.Identity.FindAsync(key);
            if (identity == null)
            {
                return NotFound();
            }

            patch.Patch(identity);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdentityExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(identity);
        }

        // DELETE: odata/Identities(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Identity identity = await db.Identity.FindAsync(key);
            if (identity == null)
            {
                return NotFound();
            }

            db.Identity.Remove(identity);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Identities(5)/IdentityBinderLink
        [EnableQuery]
        public IQueryable<IdentityBinderLink> GetIdentityBinderLink([FromODataUri] int key)
        {
            return db.Identity.Where(m => m.Id == key).SelectMany(m => m.IdentityBinderLink);
        }

        // GET: odata/Identities(5)/OrganizationIdentityLink
        [EnableQuery]
        public IQueryable<OrganizationIdentityLink> GetOrganizationIdentityLink([FromODataUri] int key)
        {
            return db.Identity.Where(m => m.Id == key).SelectMany(m => m.OrganizationIdentityLink);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IdentityExists(int key)
        {
            return db.Identity.Count(e => e.Id == key) > 0;
        }
    }
}
