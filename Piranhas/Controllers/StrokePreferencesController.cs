using Piranhas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Net;

namespace Piranhas.Controllers
{
    public class StrokePreferencesController : Controller
    {
        SwimmerContext db = new SwimmerContext();
        // GET: StrokePreferences
        public ActionResult Index(int? active)
        {
            active = active == null ? 0 : active;
            return Details(active);
        }

        private ActionResult Details(int? Active)
        {
            var userId = User.Identity.GetUserId();
            var swimmers = db.Swimmers.ToList().FindAll(sw => sw.UserID == userId);
            int count = 0;
            var swimmerView = swimmers.Join(
                db.StrokePreferences,
                swim => swim.StrokePreferenceID,
                pref => pref.StrokePreferenceID,
                (swim, pref) => new SwimmerViewModel(swim, new StrokePreferenceViewModel(pref, count++))).ToList();
            ViewBag.Active = Active;

            return View(swimmerView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StrokePreferenceID,Butterfly,Backstroke,Breaststroke,Freestyle,IndividualMedley")]
            StrokePreference strokePreference)
        {
            if (ModelState.IsValid)
            {
                db.Entry(strokePreference).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { active = strokePreference});
            }
            
            return View(strokePreference);
        }
        // GET: StrokePreferences1/Edit/5
        public ActionResult Edit(int? id, int? active)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StrokePreference strokePreference = db.StrokePreferences.Find(id);
            if (strokePreference == null)
            {
                return HttpNotFound();
            }
            int test = (int) (active == null ? 0 : active);
            ViewBag.Active = test;
            return View(strokePreference);
        }
    }
}