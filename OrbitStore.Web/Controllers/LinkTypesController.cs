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
    public class LinkTypesController : Controller
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: LinkTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.LinkType.ToListAsync());
        }

        // GET: LinkTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkType linkType = await db.LinkType.FindAsync(id);
            if (linkType == null)
            {
                return HttpNotFound();
            }
            return View(linkType);
        }

        // GET: LinkTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LinkTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Path,Name,Active")] LinkType linkType)
        {
            if (ModelState.IsValid)
            {
                db.LinkType.Add(linkType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(linkType);
        }

        // GET: LinkTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkType linkType = await db.LinkType.FindAsync(id);
            if (linkType == null)
            {
                return HttpNotFound();
            }
            return View(linkType);
        }

        // POST: LinkTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Path,Name,Active")] LinkType linkType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(linkType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(linkType);
        }

        // GET: LinkTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkType linkType = await db.LinkType.FindAsync(id);
            if (linkType == null)
            {
                return HttpNotFound();
            }
            return View(linkType);
        }

        // POST: LinkTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LinkType linkType = await db.LinkType.FindAsync(id);
            db.LinkType.Remove(linkType);
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
