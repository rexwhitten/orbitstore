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
using OrbitStore.Web.Authentication;
using System.Dynamic;

namespace OrbitStore.Web.Areas.Api.Controllers
{
    public class IdentityApiController : ApiController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: api/IdentityApi
        public IQueryable<Identity> GetIdentity()
        {
            if (AuthenticationComponent.Authorize(Request) == true)
            {
                return db.Identity;
            }

            return null;
        }


        // GET: api/IdentityApi/5
        [ResponseType(typeof(Identity))]
        public async Task<IHttpActionResult> GetIdentity(int id)
        {
            Identity identity = await db.Identity.FindAsync(id);
            if (identity == null)
            {
                return NotFound();
            }

            return Ok(identity);
        }

        // PUT: api/IdentityApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutIdentity(int id, Identity identity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != identity.Id)
            {
                return BadRequest();
            }

            db.Entry(identity).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdentityExists(id))
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

        // POST: api/IdentityApi
        [ResponseType(typeof(Identity))]
        public async Task<IHttpActionResult> PostIdentity(Identity identity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Identity.Add(identity);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = identity.Id }, identity);
        }

        // DELETE: api/IdentityApi/5
        [ResponseType(typeof(Identity))]
        public async Task<IHttpActionResult> DeleteIdentity(int id)
        {
            Identity identity = await db.Identity.FindAsync(id);
            if (identity == null)
            {
                return NotFound();
            }

            db.Identity.Remove(identity);
            await db.SaveChangesAsync();

            return Ok(identity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IdentityExists(int id)
        {
            return db.Identity.Count(e => e.Id == id) > 0;
        }
    }
}