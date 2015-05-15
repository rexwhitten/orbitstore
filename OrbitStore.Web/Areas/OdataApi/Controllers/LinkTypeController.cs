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
    builder.EntitySet<LinkType>("LinkType");
    builder.EntitySet<BinderResourceLink>("BinderResourceLink"); 
    builder.EntitySet<IdentityBinderLink>("IdentityBinderLink"); 
    builder.EntitySet<OrganizationIdentityLink>("OrganizationIdentityLink"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class LinkTypeController : ODataController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: odata/LinkType
        [EnableQuery]
        public IQueryable<LinkType> GetLinkType()
        {
            return db.LinkType;
        }

        // GET: odata/LinkType(5)
        [EnableQuery]
        public SingleResult<LinkType> GetLinkType([FromODataUri] int key)
        {
            return SingleResult.Create(db.LinkType.Where(linkType => linkType.Id == key));
        }

        // PUT: odata/LinkType(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<LinkType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LinkType linkType = await db.LinkType.FindAsync(key);
            if (linkType == null)
            {
                return NotFound();
            }

            patch.Put(linkType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LinkTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(linkType);
        }

        // POST: odata/LinkType
        public async Task<IHttpActionResult> Post(LinkType linkType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LinkType.Add(linkType);
            await db.SaveChangesAsync();

            return Created(linkType);
        }

        // PATCH: odata/LinkType(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<LinkType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LinkType linkType = await db.LinkType.FindAsync(key);
            if (linkType == null)
            {
                return NotFound();
            }

            patch.Patch(linkType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LinkTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(linkType);
        }

        // DELETE: odata/LinkType(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            LinkType linkType = await db.LinkType.FindAsync(key);
            if (linkType == null)
            {
                return NotFound();
            }

            db.LinkType.Remove(linkType);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/LinkType(5)/BinderResourceLink
        [EnableQuery]
        public IQueryable<BinderResourceLink> GetBinderResourceLink([FromODataUri] int key)
        {
            return db.LinkType.Where(m => m.Id == key).SelectMany(m => m.BinderResourceLink);
        }

        // GET: odata/LinkType(5)/IdentityBinderLink
        [EnableQuery]
        public IQueryable<IdentityBinderLink> GetIdentityBinderLink([FromODataUri] int key)
        {
            return db.LinkType.Where(m => m.Id == key).SelectMany(m => m.IdentityBinderLink);
        }

        // GET: odata/LinkType(5)/OrganizationIdentityLink
        [EnableQuery]
        public IQueryable<OrganizationIdentityLink> GetOrganizationIdentityLink([FromODataUri] int key)
        {
            return db.LinkType.Where(m => m.Id == key).SelectMany(m => m.OrganizationIdentityLink);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LinkTypeExists(int key)
        {
            return db.LinkType.Count(e => e.Id == key) > 0;
        }
    }
}
