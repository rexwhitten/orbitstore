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
    public class BinderHistoriesController : Controller
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: BinderHistories
        public async Task<ActionResult> Index()
        {
            return View(await db.BinderHistory.ToListAsync());
        }

        // GET: BinderHistories/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinderHistory binderHistory = await db.BinderHistory.FindAsync(id);
            if (binderHistory == null)
            {
                return HttpNotFound();
            }
            return View(binderHistory);
        }

        // GET: BinderHistories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BinderHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AuditId,Id,Path,Name,Active,AuditAction,AuditDate,AuditUser,AuditApp")] BinderHistory binderHistory)
        {
            if (ModelState.IsValid)
            {
                db.BinderHistory.Add(binderHistory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(binderHistory);
        }

        // GET: BinderHistories/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinderHistory binderHistory = await db.BinderHistory.FindAsync(id);
            if (binderHistory == null)
            {
                return HttpNotFound();
            }
            return View(binderHistory);
        }

        // POST: BinderHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AuditId,Id,Path,Name,Active,AuditAction,AuditDate,AuditUser,AuditApp")] BinderHistory binderHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(binderHistory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(binderHistory);
        }

        // GET: BinderHistories/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinderHistory binderHistory = await db.BinderHistory.FindAsync(id);
            if (binderHistory == null)
            {
                return HttpNotFound();
            }
            return View(binderHistory);
        }

        // POST: BinderHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            BinderHistory binderHistory = await db.BinderHistory.FindAsync(id);
            db.BinderHistory.Remove(binderHistory);
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
