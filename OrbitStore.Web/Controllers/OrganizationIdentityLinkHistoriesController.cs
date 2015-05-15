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
    public class OrganizationIdentityLinkHistoriesController : Controller
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: OrganizationIdentityLinkHistories
        public async Task<ActionResult> Index()
        {
            return View(await db.OrganizationIdentityLinkHistory.ToListAsync());
        }

        // GET: OrganizationIdentityLinkHistories/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationIdentityLinkHistory organizationIdentityLinkHistory = await db.OrganizationIdentityLinkHistory.FindAsync(id);
            if (organizationIdentityLinkHistory == null)
            {
                return HttpNotFound();
            }
            return View(organizationIdentityLinkHistory);
        }

        // GET: OrganizationIdentityLinkHistories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrganizationIdentityLinkHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AuditId,Id,OrganizationId,IdentityId,LinkTypeId,Active,AuditAction,AuditDate,AuditUser,AuditApp")] OrganizationIdentityLinkHistory organizationIdentityLinkHistory)
        {
            if (ModelState.IsValid)
            {
                db.OrganizationIdentityLinkHistory.Add(organizationIdentityLinkHistory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(organizationIdentityLinkHistory);
        }

        // GET: OrganizationIdentityLinkHistories/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationIdentityLinkHistory organizationIdentityLinkHistory = await db.OrganizationIdentityLinkHistory.FindAsync(id);
            if (organizationIdentityLinkHistory == null)
            {
                return HttpNotFound();
            }
            return View(organizationIdentityLinkHistory);
        }

        // POST: OrganizationIdentityLinkHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AuditId,Id,OrganizationId,IdentityId,LinkTypeId,Active,AuditAction,AuditDate,AuditUser,AuditApp")] OrganizationIdentityLinkHistory organizationIdentityLinkHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organizationIdentityLinkHistory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(organizationIdentityLinkHistory);
        }

        // GET: OrganizationIdentityLinkHistories/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationIdentityLinkHistory organizationIdentityLinkHistory = await db.OrganizationIdentityLinkHistory.FindAsync(id);
            if (organizationIdentityLinkHistory == null)
            {
                return HttpNotFound();
            }
            return View(organizationIdentityLinkHistory);
        }

        // POST: OrganizationIdentityLinkHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            OrganizationIdentityLinkHistory organizationIdentityLinkHistory = await db.OrganizationIdentityLinkHistory.FindAsync(id);
            db.OrganizationIdentityLinkHistory.Remove(organizationIdentityLinkHistory);
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
