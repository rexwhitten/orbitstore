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
using OrbitStore.Web.Areas.Api.Models;

namespace OrbitStore.Web.Areas.Api.Controllers
{
    public class BinderApiController : ApiController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: api/BinderApi
        public IQueryable<Binder> GetBinder()
        {
            return db.Binder;
        }

        // GET: api/BinderApi/5
        [ResponseType(typeof(BinderModel))]
        public async Task<IHttpActionResult> GetBinder(int id)
        {
            Binder binder = await db.Binder.FindAsync(id);
            if (binder == null)
            {
                return NotFound();
            }

            return Ok(binder);
        }

        // PUT: api/BinderApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBinder(int id, Binder binder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != binder.Id)
            {
                return BadRequest();
            }

            db.Entry(binder).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BinderExists(id))
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

        // POST: api/BinderApi
        [ResponseType(typeof(Binder))]
        public async Task<IHttpActionResult> PostBinder(Binder binder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Binder.Add(binder);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = binder.Id }, binder);
        }

        // DELETE: api/BinderApi/5
        [ResponseType(typeof(Binder))]
        public async Task<IHttpActionResult> DeleteBinder(int id)
        {
            Binder binder = await db.Binder.FindAsync(id);
            if (binder == null)
            {
                return NotFound();
            }

            db.Binder.Remove(binder);
            await db.SaveChangesAsync();

            return Ok(binder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BinderExists(int id)
        {
            return db.Binder.Count(e => e.Id == id) > 0;
        }
    }
}