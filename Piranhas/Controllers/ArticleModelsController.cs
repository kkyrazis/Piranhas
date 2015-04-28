using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Piranhas.Models;
using System.IO;

namespace Piranhas.Controllers
{
    public class ArticleModelsController : Controller
    {
        private SwimmerContext db = new SwimmerContext();

        // GET: ArticleModels
        public ActionResult Index()
        {
            return View(db.Articles.ToList());
        }

        // GET: ArticleModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleModel articleModel = db.Articles.Find(id);
            if (articleModel == null)
            {
                return HttpNotFound();
            }
            return View(articleModel);
        }

        // GET: ArticleModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArticleModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleModelID,ArticleTitle,ArticleData")] ArticleModel articleModel, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var file = new FilePath
                    {
                        FileTitle = upload.FileName,
                        FileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(upload.FileName)
                    };
                    string pathToSave = Server.MapPath("~/Files/");
                    using (var fileStream = System.IO.File.Create(pathToSave + file.FileName))
                    {
                        upload.InputStream.Seek(0, SeekOrigin.Begin);
                        upload.InputStream.CopyTo(fileStream);
                    }
                    articleModel.FilePaths = new List<FilePath>();
                    articleModel.FilePaths.Add(file);
                }
                db.Articles.Add(articleModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(articleModel);
        }

        // GET: ArticleModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleModel articleModel = db.Articles.Find(id);
            if (articleModel == null)
            {
                return HttpNotFound();
            }
            return View(articleModel);
        }

        // POST: ArticleModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleModelID,ArticleTitle,ArticleData")] ArticleModel articleModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articleModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(articleModel);
        }

        // GET: ArticleModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleModel articleModel = db.Articles.Find(id);
            if (articleModel == null)
            {
                return HttpNotFound();
            }
            return View(articleModel);
        }

        // POST: ArticleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArticleModel articleModel = db.Articles.Find(id);
            db.Articles.Remove(articleModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Download(int? id)
        {
            var file = db.FilePaths.Find(id);
            var path = "~/Files/";
            return File(path + file.FileName, "application/pdf", file.FileTitle);
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
