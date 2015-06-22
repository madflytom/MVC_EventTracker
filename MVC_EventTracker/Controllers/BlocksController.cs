using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_EventTracker.DAL;
using MVC_EventTracker.Models;

namespace MVC_EventTracker.Controllers
{
    public class BlocksController : Controller
    {
        private EventTrackerContext db = new EventTrackerContext();

        // GET: Blocks
        public async Task<ActionResult> Index()
        {
            return View(await db.Blocks.ToListAsync());
        }

        // GET: Blocks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Block block = await db.Blocks.FindAsync(id);
            if (block == null)
            {
                return HttpNotFound();
            }
            return View(block);
        }

        // GET: Blocks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BlockId,EventID,BlockStart,BlockEnd")] Block block)
        {
            if (ModelState.IsValid)
            {
                db.Blocks.Add(block);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(block);
        }

        // GET: Blocks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Block block = await db.Blocks.FindAsync(id);
            if (block == null)
            {
                return HttpNotFound();
            }
            return View(block);
        }

        // POST: Blocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BlockId,EventID,BlockStart,BlockEnd")] Block block)
        {
            if (ModelState.IsValid)
            {
                db.Entry(block).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(block);
        }

        // GET: Blocks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Block block = await db.Blocks.FindAsync(id);
            if (block == null)
            {
                return HttpNotFound();
            }
            return View(block);
        }

        // POST: Blocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Block block = await db.Blocks.FindAsync(id);
            db.Blocks.Remove(block);
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
