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
    public class LinkTypeHistoriesController : Controller
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: LinkTypeHistories
        public async Task<ActionResult> Index()
        {
            return View(await db.LinkTypeHistory.ToListAsync());
        }

        // GET: LinkTypeHistories/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkTypeHistory linkTypeHistory = await db.LinkTypeHistory.FindAsync(id);
            if (linkTypeHistory == null)
            {
                return HttpNotFound();
            }
            return View(linkTypeHistory);
        }

        // GET: LinkTypeHistories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LinkTypeHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AuditId,Id,Path,Name,Active,AuditAction,AuditDate,AuditUser,AuditApp")] LinkTypeHistory linkTypeHistory)
        {
            if (ModelState.IsValid)
            {
                db.LinkTypeHistory.Add(linkTypeHistory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(linkTypeHistory);
        }

        // GET: LinkTypeHistories/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkTypeHistory linkTypeHistory = await db.LinkTypeHistory.FindAsync(id);
            if (linkTypeHistory == null)
            {
                return HttpNotFound();
            }
            return View(linkTypeHistory);
        }

        // POST: LinkTypeHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AuditId,Id,Path,Name,Active,AuditAction,AuditDate,AuditUser,AuditApp")] LinkTypeHistory linkTypeHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(linkTypeHistory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(linkTypeHistory);
        }

        // GET: LinkTypeHistories/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkTypeHistory linkTypeHistory = await db.LinkTypeHistory.FindAsync(id);
            if (linkTypeHistory == null)
            {
                return HttpNotFound();
            }
            return View(linkTypeHistory);
        }

        // POST: LinkTypeHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            LinkTypeHistory linkTypeHistory = await db.LinkTypeHistory.FindAsync(id);
            db.LinkTypeHistory.Remove(linkTypeHistory);
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
