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
    public class EventsController : Controller
    {
        private EventTrackerContext db = new EventTrackerContext();

        // GET: Events
        public async Task<ActionResult> Index()
        {
            return View(await db.Events.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = await db.Events.FindAsync(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EventID,Title,Description,Location,StartDateTime,EndDateTime,OwnerENT,BlockDuration,Seats")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                await db.SaveChangesAsync();
                BuildBlocks(@event.EventID);
            }

            return View(@event);
        }

        public void BuildBlocks(int? id)
        {
            List<Block> blockList = new List<Block>();
            Event selectedEvent = db.Events.Find(id);

            var duration = (selectedEvent.EndDateTime - selectedEvent.StartDateTime).TotalMinutes;
            var slots = duration / selectedEvent.BlockDuration;
            for (int i = 0; i < slots; i++)
            {
                DateTime startTime = selectedEvent.StartDateTime.AddMinutes(selectedEvent.BlockDuration * i);
                db.Blocks.Add(new Block
                {
                    BlockID = i,
                    EventID = selectedEvent.EventID,
                    BlockStart = startTime,
                    BlockEnd = startTime.AddMinutes(selectedEvent.BlockDuration)
                });
                db.SaveChanges();
            }

            //return blockList;
        }

        // GET: Events/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = await db.Events.FindAsync(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EventID,Title,Description,Location,StartDateTime,EndDateTime,OwnerENT,BlockDuration,Seats")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = await db.Events.FindAsync(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Event @event = await db.Events.FindAsync(id);
            db.Events.Remove(@event);
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
