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
    public class IdentityHistoriesController : Controller
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: IdentityHistories
        public async Task<ActionResult> Index()
        {
            return View(await db.IdentityHistory.ToListAsync());
        }

        // GET: IdentityHistories/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityHistory identityHistory = await db.IdentityHistory.FindAsync(id);
            if (identityHistory == null)
            {
                return HttpNotFound();
            }
            return View(identityHistory);
        }

        // GET: IdentityHistories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IdentityHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AuditId,Id,Name,PasswordHash,Active,AuditAction,AuditDate,AuditUser,AuditApp")] IdentityHistory identityHistory)
        {
            if (ModelState.IsValid)
            {
                db.IdentityHistory.Add(identityHistory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(identityHistory);
        }

        // GET: IdentityHistories/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityHistory identityHistory = await db.IdentityHistory.FindAsync(id);
            if (identityHistory == null)
            {
                return HttpNotFound();
            }
            return View(identityHistory);
        }

        // POST: IdentityHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AuditId,Id,Name,PasswordHash,Active,AuditAction,AuditDate,AuditUser,AuditApp")] IdentityHistory identityHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(identityHistory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(identityHistory);
        }

        // GET: IdentityHistories/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityHistory identityHistory = await db.IdentityHistory.FindAsync(id);
            if (identityHistory == null)
            {
                return HttpNotFound();
            }
            return View(identityHistory);
        }

        // POST: IdentityHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            IdentityHistory identityHistory = await db.IdentityHistory.FindAsync(id);
            db.IdentityHistory.Remove(identityHistory);
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
