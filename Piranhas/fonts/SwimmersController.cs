﻿using System;
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
    public class SwimmerController : Controller
    {
        private SwimmerContext db = new SwimmerContext();
        private ApplicationDbContext adb = new ApplicationDbContext();

        // GET: Swimmers
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                return View(db.Swimmers.ToList().FindAll(sw => sw.UserID == userId));
            }
            else
            {
                return ForceLogin();
            }
        }

        private ActionResult ForceLogin()
        {
            return RedirectToAction("Login", "Account");
        }

        // GET: Swimmers/Details/5
        public ActionResult Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
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
                if (swimmer.UserID != User.Identity.GetUserId())
                {
                    //TODO go somewhere else
                    return RedirectToAction("Index", "Home");
                }
                return View(swimmer);
            }
            else
            {
                return ForceLogin();
            }
        }

        // GET: Swimmers/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return ForceLogin();
            }
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
                return View(swimmer);
            }
            else
            {
                return ForceLogin();
            }

        }

        // GET: Swimmers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
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
                if (swimmer.UserID != User.Identity.GetUserId())
                {
                    return RedirectToAction("Index", "Home");
                }
                return View(swimmer);
            }
            else
            {
                return ForceLogin();
            }
        }

        // POST: Swimmers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SwimmerID,FirstName,LastName,Birthdate")] Swimmer swimmer)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(swimmer).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(swimmer);
            }
            else
            {
                return ForceLogin();
            }
        }

        // GET: Swimmers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
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
                if (swimmer.UserID != User.Identity.GetUserId())
                {
                    return RedirectToAction("Index", "Home");
                }
                return View(swimmer);
            }
            else
            {
                return ForceLogin();
            }

        }

        // POST: Swimmers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Swimmer swimmer = db.Swimmers.Find(id);
                if (swimmer.UserID != User.Identity.GetUserId())
                {
                    return RedirectToAction("Index", "Home");
                }
                StrokePreference preference =  db.StrokePreferences.Find(swimmer.StrokePreferenceID);
                db.StrokePreferences.Remove(preference);
                db.Swimmers.Remove(swimmer);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return ForceLogin();
            }
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
