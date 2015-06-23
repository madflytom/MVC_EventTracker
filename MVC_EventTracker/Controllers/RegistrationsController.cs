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
    public class RegistrationsController : Controller
    {
        private EventTrackerContext db = new EventTrackerContext();

        // GET: Registrations
        public async Task<ActionResult> Index()
        {
            var registrations = db.Registrations.Include(r => r.Event).Include(r => r.Participant);
            return View(await registrations.ToListAsync());
        }

        // GET: Registrations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = await db.Registrations.FindAsync(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // GET: Registrations/Create
        public ActionResult Create()
        {
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Title");
            // create linq query above to filter out those blocks that already have a username associated with them
            var blocks = db.Blocks.Select(e => new SelectListItem { Text = e.BlockStart.ToString(), Value = e.BlockID.ToString() });
            ViewData["Blocks"] = blocks;
            ViewBag.ParticipantID = new SelectList(db.Participants, "ParticipantID", "ParticipantENT");
            return View();
        }

        // TODO:  Figure out how to insert the selected block id into the registration table.

       

        // POST: Registrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RegistrationID,EventID,ParticipantID,BlockID")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Registrations.Add(registration);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EventID = new SelectList(db.Events, "EventID", "Title", registration.EventID);
            ViewBag.ParticipantID = new SelectList(db.Participants, "ParticipantID", "ParticipantENT", registration.ParticipantID);
            var blocks = db.Blocks.Select(e => new SelectListItem { Text = e.BlockStart.ToString(), Value = e.BlockID.ToString() });
            ViewData["Blocks"] = blocks;
            return View(registration);
        }

        // GET: Registrations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = await db.Registrations.FindAsync(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Title", registration.EventID);
            ViewBag.ParticipantID = new SelectList(db.Participants, "ParticipantID", "ParticipantENT", registration.ParticipantID);
            return View(registration);
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RegistrationID,EventID,ParticipantID")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Title", registration.EventID);
            ViewBag.ParticipantID = new SelectList(db.Participants, "ParticipantID", "ParticipantENT", registration.ParticipantID);
            return View(registration);
        }

        // GET: Registrations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = await db.Registrations.FindAsync(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Registration registration = await db.Registrations.FindAsync(id);
            db.Registrations.Remove(registration);
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
