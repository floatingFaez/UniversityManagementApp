using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemApp.Context;
using UniversityManagementSystemApp.Models;
using UniversityManagementSystemApp.Views.viewModel;

namespace UniversityManagementSystemApp.Controllers
{
    public class ResultsController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        // GET: Results
        public ActionResult Index()
        {
            var results = db.Results.Include(r => r.Course).Include(r => r.Student);
            return View(results.ToList());
        }

        // GET: Results/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: Results/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code");
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentId");
            return View();
        }

        // POST: Results/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResultId,StudentId,CourseId,GradeLetter")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Results.Add(result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", result.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", result.StudentId);
            return View(result);
        }

        // GET: Results/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", result.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", result.StudentId);
            return View(result);
        }

        // POST: Results/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResultId,StudentId,CourseId,GradeLetter")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", result.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", result.StudentId);
            return View(result);
        }

        // GET: Results/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Result result = db.Results.Find(id);
            db.Results.Remove(result);
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

        public ActionResult GetResultById(int? studentId)
        {
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentId");
            
            return View();


          
        }

        public JsonResult GetResultInfo(int studentId)
        {

            var resultInfo =
                db.Results.Include(n => n.Course)
                    .Where(s => s.StudentId == studentId)
                    .Select(
                        s =>
                            new ResultView
                            {
                                CourseCode = s.Course.Code,
                                CourseName = s.Course.Name,
                                GradeLetter = s.GradeLetter
                            });
            return Json(resultInfo, JsonRequestBehavior.AllowGet);
        }
    }
}
