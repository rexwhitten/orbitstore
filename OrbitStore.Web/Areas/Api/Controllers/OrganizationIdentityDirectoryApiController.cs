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
using System.Web.Http.Description;
using OrbitStore.Web.Data;

namespace OrbitStore.Web.Areas.Api.Controllers
{
    public class OrganizationIdentityDirectoryApiController : ApiController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: api/OrganizationIdentityDirectoryApi
        public IQueryable<OrganizationIdentityDirectory> GetOrganizationIdentityDirectory()
        {
            return db.OrganizationIdentityDirectory;
        }

        // GET: api/OrganizationIdentityDirectoryApi/5
        [ResponseType(typeof(OrganizationIdentityDirectory))]
        public async Task<IHttpActionResult> GetOrganizationIdentityDirectory(int id)
        {
            OrganizationIdentityDirectory organizationIdentityDirectory = await db.OrganizationIdentityDirectory.FindAsync(id);
            if (organizationIdentityDirectory == null)
            {
                return NotFound();
            }

            return Ok(organizationIdentityDirectory);
        }

        // PUT: api/OrganizationIdentityDirectoryApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrganizationIdentityDirectory(int id, OrganizationIdentityDirectory organizationIdentityDirectory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != organizationIdentityDirectory.OrganizationId)
            {
                return BadRequest();
            }

            db.Entry(organizationIdentityDirectory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationIdentityDirectoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/OrganizationIdentityDirectoryApi
        [ResponseType(typeof(OrganizationIdentityDirectory))]
        public async Task<IHttpActionResult> PostOrganizationIdentityDirectory(OrganizationIdentityDirectory organizationIdentityDirectory)
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

            return CreatedAtRoute("DefaultApi", new { id = organizationIdentityDirectory.OrganizationId }, organizationIdentityDirectory);
        }

        // DELETE: api/OrganizationIdentityDirectoryApi/5
        [ResponseType(typeof(OrganizationIdentityDirectory))]
        public async Task<IHttpActionResult> DeleteOrganizationIdentityDirectory(int id)
        {
            OrganizationIdentityDirectory organizationIdentityDirectory = await db.OrganizationIdentityDirectory.FindAsync(id);
            if (organizationIdentityDirectory == null)
            {
                return NotFound();
            }

            db.OrganizationIdentityDirectory.Remove(organizationIdentityDirectory);
            await db.SaveChangesAsync();

            return Ok(organizationIdentityDirectory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrganizationIdentityDirectoryExists(int id)
        {
            return db.OrganizationIdentityDirectory.Count(e => e.OrganizationId == id) > 0;
        }
    }
}