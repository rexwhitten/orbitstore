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
    public class ResourceHistoriesController : Controller
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: ResourceHistories
        public async Task<ActionResult> Index()
        {
            return View(await db.ResourceHistory.ToListAsync());
        }

        // GET: ResourceHistories/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResourceHistory resourceHistory = await db.ResourceHistory.FindAsync(id);
            if (resourceHistory == null)
            {
                return HttpNotFound();
            }
            return View(resourceHistory);
        }

        // GET: ResourceHistories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResourceHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AuditId,Id,Path,Name,Content,Active,AuditAction,AuditDate,AuditUser,AuditApp")] ResourceHistory resourceHistory)
        {
            if (ModelState.IsValid)
            {
                db.ResourceHistory.Add(resourceHistory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(resourceHistory);
        }

        // GET: ResourceHistories/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResourceHistory resourceHistory = await db.ResourceHistory.FindAsync(id);
            if (resourceHistory == null)
            {
                return HttpNotFound();
            }
            return View(resourceHistory);
        }

        // POST: ResourceHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AuditId,Id,Path,Name,Content,Active,AuditAction,AuditDate,AuditUser,AuditApp")] ResourceHistory resourceHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resourceHistory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(resourceHistory);
        }

        // GET: ResourceHistories/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResourceHistory resourceHistory = await db.ResourceHistory.FindAsync(id);
            if (resourceHistory == null)
            {
                return HttpNotFound();
            }
            return View(resourceHistory);
        }

        // POST: ResourceHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            ResourceHistory resourceHistory = await db.ResourceHistory.FindAsync(id);
            db.ResourceHistory.Remove(resourceHistory);
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
