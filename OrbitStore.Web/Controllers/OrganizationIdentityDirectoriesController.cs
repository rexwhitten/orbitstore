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
    public class OrganizationIdentityDirectoriesController : Controller
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: OrganizationIdentityDirectories
        public async Task<ActionResult> Index()
        {
            return View(await db.OrganizationIdentityDirectory.ToListAsync());
        }

        // GET: OrganizationIdentityDirectories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationIdentityDirectory organizationIdentityDirectory = await db.OrganizationIdentityDirectory.FindAsync(id);
            if (organizationIdentityDirectory == null)
            {
                return HttpNotFound();
            }
            return View(organizationIdentityDirectory);
        }

        // GET: OrganizationIdentityDirectories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrganizationIdentityDirectories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrganizationId,OrganizationPath,OrganizationName,OrganizationActive,IdentityId,IdentityName,IdentityActive,LinkPath,linkName,LinkActive,DirectoryPath")] OrganizationIdentityDirectory organizationIdentityDirectory)
        {
            if (ModelState.IsValid)
            {
                db.OrganizationIdentityDirectory.Add(organizationIdentityDirectory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(organizationIdentityDirectory);
        }

        // GET: OrganizationIdentityDirectories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationIdentityDirectory organizationIdentityDirectory = await db.OrganizationIdentityDirectory.FindAsync(id);
            if (organizationIdentityDirectory == null)
            {
                return HttpNotFound();
            }
            return View(organizationIdentityDirectory);
        }

        // POST: OrganizationIdentityDirectories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrganizationId,OrganizationPath,OrganizationName,OrganizationActive,IdentityId,IdentityName,IdentityActive,LinkPath,linkName,LinkActive,DirectoryPath")] OrganizationIdentityDirectory organizationIdentityDirectory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organizationIdentityDirectory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(organizationIdentityDirectory);
        }

        // GET: OrganizationIdentityDirectories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationIdentityDirectory organizationIdentityDirectory = await db.OrganizationIdentityDirectory.FindAsync(id);
            if (organizationIdentityDirectory == null)
            {
                return HttpNotFound();
            }
            return View(organizationIdentityDirectory);
        }

        // POST: OrganizationIdentityDirectories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OrganizationIdentityDirectory organizationIdentityDirectory = await db.OrganizationIdentityDirectory.FindAsync(id);
            db.OrganizationIdentityDirectory.Remove(organizationIdentityDirectory);
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
