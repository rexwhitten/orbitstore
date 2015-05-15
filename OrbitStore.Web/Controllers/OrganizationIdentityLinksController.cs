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
    public class OrganizationIdentityLinksController : Controller
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: OrganizationIdentityLinks
        public async Task<ActionResult> Index()
        {
            var organizationIdentityLink = db.OrganizationIdentityLink.Include(o => o.Identity).Include(o => o.LinkType).Include(o => o.Organization);
            return View(await organizationIdentityLink.ToListAsync());
        }

        // GET: OrganizationIdentityLinks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationIdentityLink organizationIdentityLink = await db.OrganizationIdentityLink.FindAsync(id);
            if (organizationIdentityLink == null)
            {
                return HttpNotFound();
            }
            return View(organizationIdentityLink);
        }

        // GET: OrganizationIdentityLinks/Create
        public ActionResult Create()
        {
            ViewBag.IdentityId = new SelectList(db.Identity, "Id", "Name");
            ViewBag.LinkTypeId = new SelectList(db.LinkType, "Id", "Path");
            ViewBag.OrganizationId = new SelectList(db.Organization, "Id", "Path");
            return View();
        }

        // POST: OrganizationIdentityLinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,OrganizationId,IdentityId,LinkTypeId,Active")] OrganizationIdentityLink organizationIdentityLink)
        {
            if (ModelState.IsValid)
            {
                db.OrganizationIdentityLink.Add(organizationIdentityLink);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdentityId = new SelectList(db.Identity, "Id", "Name", organizationIdentityLink.IdentityId);
            ViewBag.LinkTypeId = new SelectList(db.LinkType, "Id", "Path", organizationIdentityLink.LinkTypeId);
            ViewBag.OrganizationId = new SelectList(db.Organization, "Id", "Path", organizationIdentityLink.OrganizationId);
            return View(organizationIdentityLink);
        }

        // GET: OrganizationIdentityLinks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationIdentityLink organizationIdentityLink = await db.OrganizationIdentityLink.FindAsync(id);
            if (organizationIdentityLink == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdentityId = new SelectList(db.Identity, "Id", "Name", organizationIdentityLink.IdentityId);
            ViewBag.LinkTypeId = new SelectList(db.LinkType, "Id", "Path", organizationIdentityLink.LinkTypeId);
            ViewBag.OrganizationId = new SelectList(db.Organization, "Id", "Path", organizationIdentityLink.OrganizationId);
            return View(organizationIdentityLink);
        }

        // POST: OrganizationIdentityLinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,OrganizationId,IdentityId,LinkTypeId,Active")] OrganizationIdentityLink organizationIdentityLink)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organizationIdentityLink).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdentityId = new SelectList(db.Identity, "Id", "Name", organizationIdentityLink.IdentityId);
            ViewBag.LinkTypeId = new SelectList(db.LinkType, "Id", "Path", organizationIdentityLink.LinkTypeId);
            ViewBag.OrganizationId = new SelectList(db.Organization, "Id", "Path", organizationIdentityLink.OrganizationId);
            return View(organizationIdentityLink);
        }

        // GET: OrganizationIdentityLinks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationIdentityLink organizationIdentityLink = await db.OrganizationIdentityLink.FindAsync(id);
            if (organizationIdentityLink == null)
            {
                return HttpNotFound();
            }
            return View(organizationIdentityLink);
        }

        // POST: OrganizationIdentityLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OrganizationIdentityLink organizationIdentityLink = await db.OrganizationIdentityLink.FindAsync(id);
            db.OrganizationIdentityLink.Remove(organizationIdentityLink);
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
