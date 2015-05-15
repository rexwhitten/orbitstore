using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OrbitStore.Web.Data;

namespace OrbitStore.Web.Controllers
{
    public class IdentitiesController : Controller
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: Identities
        public async Task<ActionResult> Index()
        {
            return View(await db.Identity.ToListAsync());
        }

        // GET: Identities/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Identity identity = await db.Identity.FindAsync(id);
            if (identity == null)
            {
                return HttpNotFound();
            }
            return View(identity);
        }

        // GET: Identities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Identities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,PasswordHash,Active")] Identity identity)
        {
            if (ModelState.IsValid)
            {
                db.Identity.Add(identity);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(identity);
        }

        // GET: Identities/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Identity identity = await db.Identity.FindAsync(id);
            if (identity == null)
            {
                return HttpNotFound();
            }
            return View(identity);
        }

        // POST: Identities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,PasswordHash,Active")] Identity identity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(identity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(identity);
        }

        // GET: Identities/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Identity identity = await db.Identity.FindAsync(id);
            if (identity == null)
            {
                return HttpNotFound();
            }
            return View(identity);
        }

        // POST: Identities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Identity identity = await db.Identity.FindAsync(id);
            db.Identity.Remove(identity);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
