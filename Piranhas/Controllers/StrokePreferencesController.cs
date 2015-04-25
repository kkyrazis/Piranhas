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
            ViewBag.Swimmers = db.Swimmers.ToList().FindAll(sw => sw.UserID == userId);
            return View();
        }
    }
}