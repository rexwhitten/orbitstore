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
    public class IdentityBinderLinksController : Controller
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: IdentityBinderLinks
        public async Task<ActionResult> Index()
        {
            var identityBinderLink = db.IdentityBinderLink.Include(i => i.Binder).Include(i => i.Identity).Include(i => i.LinkType);
            return View(await identityBinderLink.ToListAsync());
        }

        // GET: IdentityBinderLinks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityBinderLink identityBinderLink = await db.IdentityBinderLink.FindAsync(id);
            if (identityBinderLink == null)
            {
                return HttpNotFound();
            }
            return View(identityBinderLink);
        }

        // GET: IdentityBinderLinks/Create
        public ActionResult Create()
        {
            ViewBag.BinderId = new SelectList(db.Binder, "Id", "Path");
            ViewBag.IdentityId = new SelectList(db.Identity, "Id", "Name");
            ViewBag.LinkTypeId = new SelectList(db.LinkType, "Id", "Path");
            return View();
        }

        // POST: IdentityBinderLinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdentityId,BinderId,LinkTypeId,Active")] IdentityBinderLink identityBinderLink)
        {
            if (ModelState.IsValid)
            {
                db.IdentityBinderLink.Add(identityBinderLink);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BinderId = new SelectList(db.Binder, "Id", "Path", identityBinderLink.BinderId);
            ViewBag.IdentityId = new SelectList(db.Identity, "Id", "Name", identityBinderLink.IdentityId);
            ViewBag.LinkTypeId = new SelectList(db.LinkType, "Id", "Path", identityBinderLink.LinkTypeId);
            return View(identityBinderLink);
        }

        // GET: IdentityBinderLinks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityBinderLink identityBinderLink = await db.IdentityBinderLink.FindAsync(id);
            if (identityBinderLink == null)
            {
                return HttpNotFound();
            }
            ViewBag.BinderId = new SelectList(db.Binder, "Id", "Path", identityBinderLink.BinderId);
            ViewBag.IdentityId = new SelectList(db.Identity, "Id", "Name", identityBinderLink.IdentityId);
            ViewBag.LinkTypeId = new SelectList(db.LinkType, "Id", "Path", identityBinderLink.LinkTypeId);
            return View(identityBinderLink);
        }

        // POST: IdentityBinderLinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IdentityId,BinderId,LinkTypeId,Active")] IdentityBinderLink identityBinderLink)
        {
            if (ModelState.IsValid)
            {
                db.Entry(identityBinderLink).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BinderId = new SelectList(db.Binder, "Id", "Path", identityBinderLink.BinderId);
            ViewBag.IdentityId = new SelectList(db.Identity, "Id", "Name", identityBinderLink.IdentityId);
            ViewBag.LinkTypeId = new SelectList(db.LinkType, "Id", "Path", identityBinderLink.LinkTypeId);
            return View(identityBinderLink);
        }

        // GET: IdentityBinderLinks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityBinderLink identityBinderLink = await db.IdentityBinderLink.FindAsync(id);
            if (identityBinderLink == null)
            {
                return HttpNotFound();
            }
            return View(identityBinderLink);
        }

        // POST: IdentityBinderLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IdentityBinderLink identityBinderLink = await db.IdentityBinderLink.FindAsync(id);
            db.IdentityBinderLink.Remove(identityBinderLink);
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
