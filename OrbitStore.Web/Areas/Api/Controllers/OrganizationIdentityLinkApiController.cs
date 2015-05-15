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
    public class OrganizationIdentityLinkApiController : ApiController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: api/OrganizationIdentityLinkApi
        public IQueryable<OrganizationIdentityLink> GetOrganizationIdentityLink()
        {
            return db.OrganizationIdentityLink;
        }

        // GET: api/OrganizationIdentityLinkApi/5
        [ResponseType(typeof(OrganizationIdentityLink))]
        public async Task<IHttpActionResult> GetOrganizationIdentityLink(int id)
        {
            OrganizationIdentityLink organizationIdentityLink = await db.OrganizationIdentityLink.FindAsync(id);
            if (organizationIdentityLink == null)
            {
                return NotFound();
            }

            return Ok(organizationIdentityLink);
        }

        // PUT: api/OrganizationIdentityLinkApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrganizationIdentityLink(int id, OrganizationIdentityLink organizationIdentityLink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != organizationIdentityLink.Id)
            {
                return BadRequest();
            }

            db.Entry(organizationIdentityLink).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationIdentityLinkExists(id))
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

        // POST: api/OrganizationIdentityLinkApi
        [ResponseType(typeof(OrganizationIdentityLink))]
        public async Task<IHttpActionResult> PostOrganizationIdentityLink(OrganizationIdentityLink organizationIdentityLink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrganizationIdentityLink.Add(organizationIdentityLink);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = organizationIdentityLink.Id }, organizationIdentityLink);
        }

        // DELETE: api/OrganizationIdentityLinkApi/5
        [ResponseType(typeof(OrganizationIdentityLink))]
        public async Task<IHttpActionResult> DeleteOrganizationIdentityLink(int id)
        {
            OrganizationIdentityLink organizationIdentityLink = await db.OrganizationIdentityLink.FindAsync(id);
            if (organizationIdentityLink == null)
            {
                return NotFound();
            }

            db.OrganizationIdentityLink.Remove(organizationIdentityLink);
            await db.SaveChangesAsync();

            return Ok(organizationIdentityLink);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrganizationIdentityLinkExists(int id)
        {
            return db.OrganizationIdentityLink.Count(e => e.Id == id) > 0;
        }
    }
}