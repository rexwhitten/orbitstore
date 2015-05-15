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
    public class BinderResourceDirectoriesController : Controller
    {
        private OrbitStoreEntities db = new OrbitStoreEntities();

        // GET: BinderResourceDirectories
        public async Task<ActionResult> Index()
        {
            return View(await db.BinderResourceDirectory.ToListAsync());
        }

        // GET: BinderResourceDirectories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinderResourceDirectory binderResourceDirectory = await db.BinderResourceDirectory.FindAsync(id);
            if (binderResourceDirectory == null)
            {
                return HttpNotFound();
            }
            return View(binderResourceDirectory);
        }

        // GET: BinderResourceDirectories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BinderResourceDirectories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BinderId,BinderName,BinderPath,BinderActive,ResourceId,ResourcePath,ResourceName,ResourceActive,LinkName,LinkPath,LinkActive,DirectoryPath")] BinderResourceDirectory binderResourceDirectory)
        {
            if (ModelState.IsValid)
            {
                db.BinderResourceDirectory.Add(binderResourceDirectory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(binderResourceDirectory);
        }

        // GET: BinderResourceDirectories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinderResourceDirectory binderResourceDirectory = await db.BinderResourceDirectory.FindAsync(id);
            if (binderResourceDirectory == null)
            {
                return HttpNotFound();
            }
            return View(binderResourceDirectory);
        }

        // POST: BinderResourceDirectories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BinderId,BinderName,BinderPath,BinderActive,ResourceId,ResourcePath,ResourceName,ResourceActive,LinkName,LinkPath,LinkActive,DirectoryPath")] BinderResourceDirectory binderResourceDirectory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(binderResourceDirectory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(binderResourceDirectory);
        }

        // GET: BinderResourceDirectories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinderResourceDirectory binderResourceDirectory = await db.BinderResourceDirectory.FindAsync(id);
            if (binderResourceDirectory == null)
            {
                return HttpNotFound();
            }
            return View(binderResourceDirectory);
        }

        // POST: BinderResourceDirectories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BinderResourceDirectory binderResourceDirectory = await db.BinderResourceDirectory.FindAsync(id);
            db.BinderResourceDirectory.Remove(binderResourceDirectory);
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
