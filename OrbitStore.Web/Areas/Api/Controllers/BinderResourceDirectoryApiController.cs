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
    public class BinderResourceDirectoryApiController : ApiController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: api/BinderResourceDirectoryApi
        public IQueryable<BinderResourceDirectory> GetBinderResourceDirectory()
        {
            return db.BinderResourceDirectory;
        }

        // GET: api/BinderResourceDirectoryApi/5
        [ResponseType(typeof(BinderResourceDirectory))]
        public async Task<IHttpActionResult> GetBinderResourceDirectory(int id)
        {
            BinderResourceDirectory binderResourceDirectory = await db.BinderResourceDirectory.FindAsync(id);
            if (binderResourceDirectory == null)
            {
                return NotFound();
            }

            return Ok(binderResourceDirectory);
        }

        // PUT: api/BinderResourceDirectoryApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBinderResourceDirectory(int id, BinderResourceDirectory binderResourceDirectory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != binderResourceDirectory.BinderId)
            {
                return BadRequest();
            }

            db.Entry(binderResourceDirectory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BinderResourceDirectoryExists(id))
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

        // POST: api/BinderResourceDirectoryApi
        [ResponseType(typeof(BinderResourceDirectory))]
        public async Task<IHttpActionResult> PostBinderResourceDirectory(BinderResourceDirectory binderResourceDirectory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BinderResourceDirectory.Add(binderResourceDirectory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BinderResourceDirectoryExists(binderResourceDirectory.BinderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = binderResourceDirectory.BinderId }, binderResourceDirectory);
        }

        // DELETE: api/BinderResourceDirectoryApi/5
        [ResponseType(typeof(BinderResourceDirectory))]
        public async Task<IHttpActionResult> DeleteBinderResourceDirectory(int id)
        {
            BinderResourceDirectory binderResourceDirectory = await db.BinderResourceDirectory.FindAsync(id);
            if (binderResourceDirectory == null)
            {
                return NotFound();
            }

            db.BinderResourceDirectory.Remove(binderResourceDirectory);
            await db.SaveChangesAsync();

            return Ok(binderResourceDirectory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BinderResourceDirectoryExists(int id)
        {
            return db.BinderResourceDirectory.Count(e => e.BinderId == id) > 0;
        }
    }
}