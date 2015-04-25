using Piranhas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Piranhas.Controllers
{
    public class StrokePreferencesController : Controller
    {
        SwimmerContext db = new SwimmerContext();
        // GET: StrokePreferences
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var swimmers = db.Swimmers.ToList().FindAll(sw => sw.UserID == userId);
            ViewBag.Swimmers = swimmers;
            var strokePreferences = (from swim in swimmers
                                     join pref in db.StrokePreferences
                                     on swim.StrokePreferenceID equals pref.StrokePreferenceID
                                     select pref).ToList();
            var result = swimmers.Join(
                db.StrokePreferences,
                swim => swim.StrokePreferenceID,
                pref => pref.StrokePreferenceID,
                (swim, pref) => pref).ToList();

            var result2 = swimmers.Join(
                db.StrokePreferences,
                swim => swim.StrokePreferenceID,
                pref => pref.StrokePreferenceID,
                (swim, pref) => new SwimmerViewModel(swim, pref)).ToList();

            return View(result);
        }
    }
}