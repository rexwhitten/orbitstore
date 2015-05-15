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
    builder.EntitySet<OrganizationIdentityDirectory>("OrganizationIdentityDirectory");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class OrganizationIdentityDirectoryController : ODataController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: odata/OrganizationIdentityDirectory
        [EnableQuery]
        public IQueryable<OrganizationIdentityDirectory> GetOrganizationIdentityDirectory()
        {
            return db.OrganizationIdentityDirectory;
        }

        // GET: odata/OrganizationIdentityDirectory(5)
        [EnableQuery]
        public SingleResult<OrganizationIdentityDirectory> GetOrganizationIdentityDirectory([FromODataUri] int key)
        {
            return SingleResult.Create(db.OrganizationIdentityDirectory.Where(organizationIdentityDirectory => organizationIdentityDirectory.OrganizationId == key));
        }

        // PUT: odata/OrganizationIdentityDirectory(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<OrganizationIdentityDirectory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OrganizationIdentityDirectory organizationIdentityDirectory = await db.OrganizationIdentityDirectory.FindAsync(key);
            if (organizationIdentityDirectory == null)
            {
                return NotFound();
            }

            patch.Put(organizationIdentityDirectory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationIdentityDirectoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(organizationIdentityDirectory);
        }

        // POST: odata/OrganizationIdentityDirectory
        public async Task<IHttpActionResult> Post(OrganizationIdentityDirectory organizationIdentityDirectory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrganizationIdentityDirectory.Add(organizationIdentityDirectory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrganizationIdentityDirectoryExists(organizationIdentityDirectory.OrganizationId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(organizationIdentityDirectory);
        }

        // PATCH: odata/OrganizationIdentityDirectory(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<OrganizationIdentityDirectory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OrganizationIdentityDirectory organizationIdentityDirectory = await db.OrganizationIdentityDirectory.FindAsync(key);
            if (organizationIdentityDirectory == null)
            {
                return NotFound();
            }

            patch.Patch(organizationIdentityDirectory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationIdentityDirectoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(organizationIdentityDirectory);
        }

        // DELETE: odata/OrganizationIdentityDirectory(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            OrganizationIdentityDirectory organizationIdentityDirectory = await db.OrganizationIdentityDirectory.FindAsync(key);
            if (organizationIdentityDirectory == null)
            {
                return NotFound();
            }

            db.OrganizationIdentityDirectory.Remove(organizationIdentityDirectory);
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

        private bool OrganizationIdentityDirectoryExists(int key)
        {
            return db.OrganizationIdentityDirectory.Count(e => e.OrganizationId == key) > 0;
        }
    }
}
