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
    public class IdentityBinderLinkApiController : ApiController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: api/IdentityBinderLinkApi
        public IQueryable<IdentityBinderLink> GetIdentityBinderLink()
        {
            return db.IdentityBinderLink;
        }

        // GET: api/IdentityBinderLinkApi/5
        [ResponseType(typeof(IdentityBinderLink))]
        public async Task<IHttpActionResult> GetIdentityBinderLink(int id)
        {
            IdentityBinderLink identityBinderLink = await db.IdentityBinderLink.FindAsync(id);
            if (identityBinderLink == null)
            {
                return NotFound();
            }

            return Ok(identityBinderLink);
        }

        // PUT: api/IdentityBinderLinkApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutIdentityBinderLink(int id, IdentityBinderLink identityBinderLink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != identityBinderLink.Id)
            {
                return BadRequest();
            }

            db.Entry(identityBinderLink).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdentityBinderLinkExists(id))
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

        // POST: api/IdentityBinderLinkApi
        [ResponseType(typeof(IdentityBinderLink))]
        public async Task<IHttpActionResult> PostIdentityBinderLink(IdentityBinderLink identityBinderLink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IdentityBinderLink.Add(identityBinderLink);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = identityBinderLink.Id }, identityBinderLink);
        }

        // DELETE: api/IdentityBinderLinkApi/5
        [ResponseType(typeof(IdentityBinderLink))]
        public async Task<IHttpActionResult> DeleteIdentityBinderLink(int id)
        {
            IdentityBinderLink identityBinderLink = await db.IdentityBinderLink.FindAsync(id);
            if (identityBinderLink == null)
            {
                return NotFound();
            }

            db.IdentityBinderLink.Remove(identityBinderLink);
            await db.SaveChangesAsync();

            return Ok(identityBinderLink);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IdentityBinderLinkExists(int id)
        {
            return db.IdentityBinderLink.Count(e => e.Id == id) > 0;
        }
    }
}