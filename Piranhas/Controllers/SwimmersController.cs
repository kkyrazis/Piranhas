using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Piranhas.Models;

namespace Piranhas.Controllers
{
    public class SwimmersController : Controller
    {
        private SwimmerContext db = new SwimmerContext();
        private ApplicationDbContext adb = new ApplicationDbContext();

        // GET: Swimmers
        public ActionResult Index()
        {
            return View(db.Swimmers.ToList());
        }

        // GET: Swimmers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Swimmer swimmer = db.Swimmers.Find(id);
            if (swimmer == null)
            {
                return HttpNotFound();
            }
            return View(swimmer);
        }

        // GET: Swimmers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Swimmers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SwimmerID,FirstName,LastName,Birthdate")] Swimmer swimmer)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var newPreference = new StrokePreference();
                    db.StrokePreferences.Add(newPreference);
                    db.SaveChanges();
                    swimmer.StrokePreferenceID = newPreference.StrokePreferenceID;
                    swimmer.UserID = adb.Users.First(u => u.UserName == User.Identity.Name).Id;
                    db.Swimmers.Add(swimmer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(swimmer);
        }

        // GET: Swimmers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Swimmer swimmer = db.Swimmers.Find(id);
            if (swimmer == null)
            {
                return HttpNotFound();
            }
            return View(swimmer);
        }

        // POST: Swimmers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SwimmerID,FirstName,LastName,Birthdate")] Swimmer swimmer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(swimmer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(swimmer);
        }

        // GET: Swimmers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Swimmer swimmer = db.Swimmers.Find(id);
            if (swimmer == null)
            {
                return HttpNotFound();
            }
            return View(swimmer);
        }

        // POST: Swimmers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Swimmer swimmer = db.Swimmers.Find(id);
            db.Swimmers.Remove(swimmer);
            db.SaveChanges();
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
