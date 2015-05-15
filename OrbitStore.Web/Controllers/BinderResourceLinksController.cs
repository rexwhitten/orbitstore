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
    public class BinderResourceLinksController : Controller
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: BinderResourceLinks
        public async Task<ActionResult> Index()
        {
            var binderResourceLink = db.BinderResourceLink.Include(b => b.Binder).Include(b => b.Resource).Include(b => b.LinkType);
            return View(await binderResourceLink.ToListAsync());
        }

        // GET: BinderResourceLinks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinderResourceLink binderResourceLink = await db.BinderResourceLink.FindAsync(id);
            if (binderResourceLink == null)
            {
                return HttpNotFound();
            }
            return View(binderResourceLink);
        }

        // GET: BinderResourceLinks/Create
        public ActionResult Create()
        {
            ViewBag.BinderId = new SelectList(db.Binder, "Id", "Path");
            ViewBag.ResourceId = new SelectList(db.Resource, "Id", "Path");
            ViewBag.LinkTypeId = new SelectList(db.LinkType, "Id", "Path");
            return View();
        }

        // POST: BinderResourceLinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,BinderId,ResourceId,LinkTypeId,Active")] BinderResourceLink binderResourceLink)
        {
            if (ModelState.IsValid)
            {
                db.BinderResourceLink.Add(binderResourceLink);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BinderId = new SelectList(db.Binder, "Id", "Path", binderResourceLink.BinderId);
            ViewBag.ResourceId = new SelectList(db.Resource, "Id", "Path", binderResourceLink.ResourceId);
            ViewBag.LinkTypeId = new SelectList(db.LinkType, "Id", "Path", binderResourceLink.LinkTypeId);
            return View(binderResourceLink);
        }

        // GET: BinderResourceLinks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinderResourceLink binderResourceLink = await db.BinderResourceLink.FindAsync(id);
            if (binderResourceLink == null)
            {
                return HttpNotFound();
            }
            ViewBag.BinderId = new SelectList(db.Binder, "Id", "Path", binderResourceLink.BinderId);
            ViewBag.ResourceId = new SelectList(db.Resource, "Id", "Path", binderResourceLink.ResourceId);
            ViewBag.LinkTypeId = new SelectList(db.LinkType, "Id", "Path", binderResourceLink.LinkTypeId);
            return View(binderResourceLink);
        }

        // POST: BinderResourceLinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,BinderId,ResourceId,LinkTypeId,Active")] BinderResourceLink binderResourceLink)
        {
            if (ModelState.IsValid)
            {
                db.Entry(binderResourceLink).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BinderId = new SelectList(db.Binder, "Id", "Path", binderResourceLink.BinderId);
            ViewBag.ResourceId = new SelectList(db.Resource, "Id", "Path", binderResourceLink.ResourceId);
            ViewBag.LinkTypeId = new SelectList(db.LinkType, "Id", "Path", binderResourceLink.LinkTypeId);
            return View(binderResourceLink);
        }

        // GET: BinderResourceLinks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinderResourceLink binderResourceLink = await db.BinderResourceLink.FindAsync(id);
            if (binderResourceLink == null)
            {
                return HttpNotFound();
            }
            return View(binderResourceLink);
        }

        // POST: BinderResourceLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BinderResourceLink binderResourceLink = await db.BinderResourceLink.FindAsync(id);
            db.BinderResourceLink.Remove(binderResourceLink);
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
