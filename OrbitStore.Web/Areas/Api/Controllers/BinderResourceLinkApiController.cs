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
    public class BinderResourceLinkApiController : ApiController
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: api/BinderResourceLinkApi
        public IQueryable<BinderResourceLink> GetBinderResourceLink()
        {
            return db.BinderResourceLink;
        }

        // GET: api/BinderResourceLinkApi/5
        [ResponseType(typeof(BinderResourceLink))]
        public async Task<IHttpActionResult> GetBinderResourceLink(int id)
        {
            BinderResourceLink binderResourceLink = await db.BinderResourceLink.FindAsync(id);
            if (binderResourceLink == null)
            {
                return NotFound();
            }

            return Ok(binderResourceLink);
        }

        // PUT: api/BinderResourceLinkApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBinderResourceLink(int id, BinderResourceLink binderResourceLink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != binderResourceLink.Id)
            {
                return BadRequest();
            }

            db.Entry(binderResourceLink).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BinderResourceLinkExists(id))
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

        // POST: api/BinderResourceLinkApi
        [ResponseType(typeof(BinderResourceLink))]
        public async Task<IHttpActionResult> PostBinderResourceLink(BinderResourceLink binderResourceLink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BinderResourceLink.Add(binderResourceLink);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = binderResourceLink.Id }, binderResourceLink);
        }

        // DELETE: api/BinderResourceLinkApi/5
        [ResponseType(typeof(BinderResourceLink))]
        public async Task<IHttpActionResult> DeleteBinderResourceLink(int id)
        {
            BinderResourceLink binderResourceLink = await db.BinderResourceLink.FindAsync(id);
            if (binderResourceLink == null)
            {
                return NotFound();
            }

            db.BinderResourceLink.Remove(binderResourceLink);
            await db.SaveChangesAsync();

            return Ok(binderResourceLink);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BinderResourceLinkExists(int id)
        {
            return db.BinderResourceLink.Count(e => e.Id == id) > 0;
        }
    }
}