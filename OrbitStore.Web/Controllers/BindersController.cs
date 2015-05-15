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
    public class BindersController : Controller
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: Binders
        public async Task<ActionResult> Index()
        {
            return View(await db.Binder.ToListAsync());
        }

        // GET: Binders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Binder binder = await db.Binder.FindAsync(id);
            if (binder == null)
            {
                return HttpNotFound();
            }
            return View(binder);
        }

        // GET: Binders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Binders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Path,Name,Active")] Binder binder)
        {
            if (ModelState.IsValid)
            {
                db.Binder.Add(binder);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(binder);
        }

        // GET: Binders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Binder binder = await db.Binder.FindAsync(id);
            if (binder == null)
            {
                return HttpNotFound();
            }
            return View(binder);
        }

        // POST: Binders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Path,Name,Active")] Binder binder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(binder).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(binder);
        }

        // GET: Binders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Binder binder = await db.Binder.FindAsync(id);
            if (binder == null)
            {
                return HttpNotFound();
            }
            return View(binder);
        }

        // POST: Binders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Binder binder = await db.Binder.FindAsync(id);
            db.Binder.Remove(binder);
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
