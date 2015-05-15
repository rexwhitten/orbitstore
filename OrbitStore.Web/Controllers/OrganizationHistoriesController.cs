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
    public class OrganizationHistoriesController : Controller
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: OrganizationHistories
        public async Task<ActionResult> Index()
        {
            return View(await db.OrganizationHistory.ToListAsync());
        }

        // GET: OrganizationHistories/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationHistory organizationHistory = await db.OrganizationHistory.FindAsync(id);
            if (organizationHistory == null)
            {
                return HttpNotFound();
            }
            return View(organizationHistory);
        }

        // GET: OrganizationHistories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrganizationHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AuditId,Id,Path,Name,Active,AuditAction,AuditDate,AuditUser,AuditApp")] OrganizationHistory organizationHistory)
        {
            if (ModelState.IsValid)
            {
                db.OrganizationHistory.Add(organizationHistory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(organizationHistory);
        }

        // GET: OrganizationHistories/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationHistory organizationHistory = await db.OrganizationHistory.FindAsync(id);
            if (organizationHistory == null)
            {
                return HttpNotFound();
            }
            return View(organizationHistory);
        }

        // POST: OrganizationHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AuditId,Id,Path,Name,Active,AuditAction,AuditDate,AuditUser,AuditApp")] OrganizationHistory organizationHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organizationHistory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(organizationHistory);
        }

        // GET: OrganizationHistories/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationHistory organizationHistory = await db.OrganizationHistory.FindAsync(id);
            if (organizationHistory == null)
            {
                return HttpNotFound();
            }
            return View(organizationHistory);
        }

        // POST: OrganizationHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            OrganizationHistory organizationHistory = await db.OrganizationHistory.FindAsync(id);
            db.OrganizationHistory.Remove(organizationHistory);
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
