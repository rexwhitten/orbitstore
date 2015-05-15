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
    public class IdentityBinderLinkHistoriesController : Controller
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: IdentityBinderLinkHistories
        public async Task<ActionResult> Index()
        {
            return View(await db.IdentityBinderLinkHistory.ToListAsync());
        }

        // GET: IdentityBinderLinkHistories/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityBinderLinkHistory identityBinderLinkHistory = await db.IdentityBinderLinkHistory.FindAsync(id);
            if (identityBinderLinkHistory == null)
            {
                return HttpNotFound();
            }
            return View(identityBinderLinkHistory);
        }

        // GET: IdentityBinderLinkHistories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IdentityBinderLinkHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AuditId,Id,IdentityId,BinderId,LinkTypeId,Active,AuditAction,AuditDate,AuditUser,AuditApp")] IdentityBinderLinkHistory identityBinderLinkHistory)
        {
            if (ModelState.IsValid)
            {
                db.IdentityBinderLinkHistory.Add(identityBinderLinkHistory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(identityBinderLinkHistory);
        }

        // GET: IdentityBinderLinkHistories/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityBinderLinkHistory identityBinderLinkHistory = await db.IdentityBinderLinkHistory.FindAsync(id);
            if (identityBinderLinkHistory == null)
            {
                return HttpNotFound();
            }
            return View(identityBinderLinkHistory);
        }

        // POST: IdentityBinderLinkHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AuditId,Id,IdentityId,BinderId,LinkTypeId,Active,AuditAction,AuditDate,AuditUser,AuditApp")] IdentityBinderLinkHistory identityBinderLinkHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(identityBinderLinkHistory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(identityBinderLinkHistory);
        }

        // GET: IdentityBinderLinkHistories/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityBinderLinkHistory identityBinderLinkHistory = await db.IdentityBinderLinkHistory.FindAsync(id);
            if (identityBinderLinkHistory == null)
            {
                return HttpNotFound();
            }
            return View(identityBinderLinkHistory);
        }

        // POST: IdentityBinderLinkHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            IdentityBinderLinkHistory identityBinderLinkHistory = await db.IdentityBinderLinkHistory.FindAsync(id);
            db.IdentityBinderLinkHistory.Remove(identityBinderLinkHistory);
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
