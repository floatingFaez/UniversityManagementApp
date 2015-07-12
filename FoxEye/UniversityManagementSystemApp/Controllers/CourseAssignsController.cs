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

namespace UniversityManagementSystemApp.Controllers
{
    public class CourseAssignsController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        // GET: CourseAssigns
        public ActionResult Index()
        {
            var courseAssigns = db.CourseAssigns.Include(c => c.Course).Include(c => c.Department).Include(c => c.Teacher);
            return View(courseAssigns.ToList());
        }

        // GET: CourseAssigns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssign courseAssign = db.CourseAssigns.Find(id);
            if (courseAssign == null)
            {
                return HttpNotFound();
            }
            return View(courseAssign);
        }

        // GET: CourseAssigns/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name");
            return View();
        }

        // POST: CourseAssigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DepartmentId,TeacherId,CourseId,Credit")] CourseAssign courseAssign)
        {
            if (ModelState.IsValid)
            {
                db.CourseAssigns.Add(courseAssign);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", courseAssign.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", courseAssign.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", courseAssign.TeacherId);
            return View(courseAssign);
        }

        // GET: CourseAssigns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssign courseAssign = db.CourseAssigns.Find(id);
            if (courseAssign == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", courseAssign.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", courseAssign.DepartmentId);
            //ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", courseAssign.TeacherId);
            return View(courseAssign);
        }

        // POST: CourseAssigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DepartmentId,TeacherId,CourseId,Credit")] CourseAssign courseAssign)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseAssign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", courseAssign.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", courseAssign.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", courseAssign.TeacherId);
            return View(courseAssign);
        }

        // GET: CourseAssigns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssign courseAssign = db.CourseAssigns.Find(id);
            if (courseAssign == null)
            {
                return HttpNotFound();
            }
            return View(courseAssign);
        }

        // POST: CourseAssigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseAssign courseAssign = db.CourseAssigns.Find(id);
            db.CourseAssigns.Remove(courseAssign);
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

        public ActionResult GetCourseInfo(int courseId)
        {
            var courseInfo = db.Courses.FirstOrDefault(s => s.CourseId == courseId);

            return Json(courseInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllTeacher(int departmentId)
        {
            var teachers = db.Teachers.Where(x => x.DepartmentId == departmentId);
            return Json(teachers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsAssigned(int? teacherId, int? courseId)
        {
            var isCourse = db.CourseAssigns.Where(c => c.TeacherId == teacherId && c.CourseId == courseId).FirstOrDefault();
            if (isCourse != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
