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
    builder.EntitySet<Organization>("Organization");
    builder.EntitySet<OrganizationIdentityLink>("OrganizationIdentityLink"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class OrganizationController : ODataController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: odata/Organization
        [EnableQuery]
        public IQueryable<Organization> GetOrganization()
        {
            return db.Organization;
        }

        // GET: odata/Organization(5)
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] int key)
        {
            return SingleResult.Create(db.Organization.Where(organization => organization.Id == key));
        }

        // PUT: odata/Organization(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Organization> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Organization organization = await db.Organization.FindAsync(key);
            if (organization == null)
            {
                return NotFound();
            }

            patch.Put(organization);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(organization);
        }

        // POST: odata/Organization
        public async Task<IHttpActionResult> Post(Organization organization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Organization.Add(organization);
            await db.SaveChangesAsync();

            return Created(organization);
        }

        // PATCH: odata/Organization(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Organization> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Organization organization = await db.Organization.FindAsync(key);
            if (organization == null)
            {
                return NotFound();
            }

            patch.Patch(organization);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(organization);
        }

        // DELETE: odata/Organization(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Organization organization = await db.Organization.FindAsync(key);
            if (organization == null)
            {
                return NotFound();
            }

            db.Organization.Remove(organization);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Organization(5)/OrganizationIdentityLink
        [EnableQuery]
        public IQueryable<OrganizationIdentityLink> GetOrganizationIdentityLink([FromODataUri] int key)
        {
            return db.Organization.Where(m => m.Id == key).SelectMany(m => m.OrganizationIdentityLink);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrganizationExists(int key)
        {
            return db.Organization.Count(e => e.Id == key) > 0;
        }
    }
}
