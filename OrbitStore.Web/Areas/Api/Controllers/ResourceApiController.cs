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
    public class ResourceApiController : ApiController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: api/ResourceApi
        public IQueryable<Resource> GetResource()
        {
            return db.Resource;
        }

        // GET: api/ResourceApi/5
        [ResponseType(typeof(Resource))]
        public async Task<IHttpActionResult> GetResource(int id)
        {
            Resource resource = await db.Resource.FindAsync(id);
            if (resource == null)
            {
                return NotFound();
            }

            return Ok(resource);
        }

        // PUT: api/ResourceApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutResource(int id, Resource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resource.Id)
            {
                return BadRequest();
            }

            db.Entry(resource).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceExists(id))
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

        // POST: api/ResourceApi
        [ResponseType(typeof(Resource))]
        public async Task<IHttpActionResult> PostResource(Resource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Resource.Add(resource);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = resource.Id }, resource);
        }

        // DELETE: api/ResourceApi/5
        [ResponseType(typeof(Resource))]
        public async Task<IHttpActionResult> DeleteResource(int id)
        {
            Resource resource = await db.Resource.FindAsync(id);
            if (resource == null)
            {
                return NotFound();
            }

            db.Resource.Remove(resource);
            await db.SaveChangesAsync();

            return Ok(resource);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResourceExists(int id)
        {
            return db.Resource.Count(e => e.Id == id) > 0;
        }
    }
}