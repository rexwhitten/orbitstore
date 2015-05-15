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
    builder.EntitySet<OrganizationIdentityLink>("OrganizationIdentityLink");
    builder.EntitySet<Identity>("Identity"); 
    builder.EntitySet<LinkType>("LinkType"); 
    builder.EntitySet<Organization>("Organization"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class OrganizationIdentityLinkController : ODataController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: odata/OrganizationIdentityLink
        [EnableQuery]
        public IQueryable<OrganizationIdentityLink> GetOrganizationIdentityLink()
        {
            return db.OrganizationIdentityLink;
        }

        // GET: odata/OrganizationIdentityLink(5)
        [EnableQuery]
        public SingleResult<OrganizationIdentityLink> GetOrganizationIdentityLink([FromODataUri] int key)
        {
            return SingleResult.Create(db.OrganizationIdentityLink.Where(organizationIdentityLink => organizationIdentityLink.Id == key));
        }

        // PUT: odata/OrganizationIdentityLink(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<OrganizationIdentityLink> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OrganizationIdentityLink organizationIdentityLink = await db.OrganizationIdentityLink.FindAsync(key);
            if (organizationIdentityLink == null)
            {
                return NotFound();
            }

            patch.Put(organizationIdentityLink);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationIdentityLinkExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(organizationIdentityLink);
        }

        // POST: odata/OrganizationIdentityLink
        public async Task<IHttpActionResult> Post(OrganizationIdentityLink organizationIdentityLink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrganizationIdentityLink.Add(organizationIdentityLink);
            await db.SaveChangesAsync();

            return Created(organizationIdentityLink);
        }

        // PATCH: odata/OrganizationIdentityLink(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<OrganizationIdentityLink> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OrganizationIdentityLink organizationIdentityLink = await db.OrganizationIdentityLink.FindAsync(key);
            if (organizationIdentityLink == null)
            {
                return NotFound();
            }

            patch.Patch(organizationIdentityLink);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationIdentityLinkExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(organizationIdentityLink);
        }

        // DELETE: odata/OrganizationIdentityLink(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            OrganizationIdentityLink organizationIdentityLink = await db.OrganizationIdentityLink.FindAsync(key);
            if (organizationIdentityLink == null)
            {
                return NotFound();
            }

            db.OrganizationIdentityLink.Remove(organizationIdentityLink);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/OrganizationIdentityLink(5)/Identity
        [EnableQuery]
        public SingleResult<Identity> GetIdentity([FromODataUri] int key)
        {
            return SingleResult.Create(db.OrganizationIdentityLink.Where(m => m.Id == key).Select(m => m.Identity));
        }

        // GET: odata/OrganizationIdentityLink(5)/LinkType
        [EnableQuery]
        public SingleResult<LinkType> GetLinkType([FromODataUri] int key)
        {
            return SingleResult.Create(db.OrganizationIdentityLink.Where(m => m.Id == key).Select(m => m.LinkType));
        }

        // GET: odata/OrganizationIdentityLink(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] int key)
        {
            return SingleResult.Create(db.OrganizationIdentityLink.Where(m => m.Id == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrganizationIdentityLinkExists(int key)
        {
            return db.OrganizationIdentityLink.Count(e => e.Id == key) > 0;
        }
    }
}
