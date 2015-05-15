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
    public class IdentityBinderDirectoryApiController : ApiController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: api/IdentityBinderDirectoryApi
        public IQueryable<IdentityBinderDirectory> GetIdentityBinderDirectory()
        {
            return db.IdentityBinderDirectory;
        }

        // GET: api/IdentityBinderDirectoryApi/5
        [ResponseType(typeof(IdentityBinderDirectory))]
        public async Task<IHttpActionResult> GetIdentityBinderDirectory(int id)
        {
            IdentityBinderDirectory identityBinderDirectory = await db.IdentityBinderDirectory.FindAsync(id);
            if (identityBinderDirectory == null)
            {
                return NotFound();
            }

            return Ok(identityBinderDirectory);
        }

        // PUT: api/IdentityBinderDirectoryApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutIdentityBinderDirectory(int id, IdentityBinderDirectory identityBinderDirectory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != identityBinderDirectory.IdentityId)
            {
                return BadRequest();
            }

            db.Entry(identityBinderDirectory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdentityBinderDirectoryExists(id))
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

        // POST: api/IdentityBinderDirectoryApi
        [ResponseType(typeof(IdentityBinderDirectory))]
        public async Task<IHttpActionResult> PostIdentityBinderDirectory(IdentityBinderDirectory identityBinderDirectory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IdentityBinderDirectory.Add(identityBinderDirectory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IdentityBinderDirectoryExists(identityBinderDirectory.IdentityId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = identityBinderDirectory.IdentityId }, identityBinderDirectory);
        }

        // DELETE: api/IdentityBinderDirectoryApi/5
        [ResponseType(typeof(IdentityBinderDirectory))]
        public async Task<IHttpActionResult> DeleteIdentityBinderDirectory(int id)
        {
            IdentityBinderDirectory identityBinderDirectory = await db.IdentityBinderDirectory.FindAsync(id);
            if (identityBinderDirectory == null)
            {
                return NotFound();
            }

            db.IdentityBinderDirectory.Remove(identityBinderDirectory);
            await db.SaveChangesAsync();

            return Ok(identityBinderDirectory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IdentityBinderDirectoryExists(int id)
        {
            return db.IdentityBinderDirectory.Count(e => e.IdentityId == id) > 0;
        }
    }
}