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
    public class BinderResourceLinkHistoriesController : Controller
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: BinderResourceLinkHistories
        public async Task<ActionResult> Index()
        {
            return View(await db.BinderResourceLinkHistory.ToListAsync());
        }

        // GET: BinderResourceLinkHistories/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinderResourceLinkHistory binderResourceLinkHistory = await db.BinderResourceLinkHistory.FindAsync(id);
            if (binderResourceLinkHistory == null)
            {
                return HttpNotFound();
            }
            return View(binderResourceLinkHistory);
        }

        // GET: BinderResourceLinkHistories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BinderResourceLinkHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AuditId,Id,BinderId,ResourceId,LinkTypeId,Active,AuditAction,AuditDate,AuditUser,AuditApp")] BinderResourceLinkHistory binderResourceLinkHistory)
        {
            if (ModelState.IsValid)
            {
                db.BinderResourceLinkHistory.Add(binderResourceLinkHistory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(binderResourceLinkHistory);
        }

        // GET: BinderResourceLinkHistories/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinderResourceLinkHistory binderResourceLinkHistory = await db.BinderResourceLinkHistory.FindAsync(id);
            if (binderResourceLinkHistory == null)
            {
                return HttpNotFound();
            }
            return View(binderResourceLinkHistory);
        }

        // POST: BinderResourceLinkHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AuditId,Id,BinderId,ResourceId,LinkTypeId,Active,AuditAction,AuditDate,AuditUser,AuditApp")] BinderResourceLinkHistory binderResourceLinkHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(binderResourceLinkHistory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(binderResourceLinkHistory);
        }

        // GET: BinderResourceLinkHistories/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinderResourceLinkHistory binderResourceLinkHistory = await db.BinderResourceLinkHistory.FindAsync(id);
            if (binderResourceLinkHistory == null)
            {
                return HttpNotFound();
            }
            return View(binderResourceLinkHistory);
        }

        // POST: BinderResourceLinkHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            BinderResourceLinkHistory binderResourceLinkHistory = await db.BinderResourceLinkHistory.FindAsync(id);
            db.BinderResourceLinkHistory.Remove(binderResourceLinkHistory);
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
