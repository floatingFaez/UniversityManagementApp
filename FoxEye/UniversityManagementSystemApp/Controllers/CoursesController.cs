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
    public class CoursesController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        // GET: Courses
        public ActionResult Index(int? departmentId)
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name");
            if (departmentId != null)
            {
                var coursesByDepartment = db.Courses.Include(c => c.Department).Where(c => c.DepartmentId == departmentId);
                return View(coursesByDepartment.ToList());
            }
            else
            {
                return View(db.Courses.Include(c => c.Department).ToList());
            }
        }

        

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,Code,Credit,Name,Description,Semester,DepartmentId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", course.DepartmentId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", course.DepartmentId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,Code,Credit,Name,Description,Semester,DepartmentId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", course.DepartmentId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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

        public JsonResult CodeExist(string code)
        {
            var courseName = db.Courses.Where(m => m.Code == code).FirstOrDefault();
            if (courseName != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);

        }

        public JsonResult NameExist(string name)
        {
            var courseName = db.Courses.Where(m => m.Name == name).FirstOrDefault();
            if (courseName != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetAllCourseByDepartment(int departmentId)
        {
            var teachers = db.Courses.Where(x => x.DepartmentId == departmentId);
            return Json(teachers, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult FilteredSection(int? deptId)
        {
            if (deptId != null)
            {
                var model = db.Courses.Include(s => s.Department).Where(s => s.Department.DepartmentId == deptId);
                return PartialView("~/Views/Shared/_FilteredSection.cshtml", model);
            }
            else
            {
                return PartialView("~/Views/Shared/_FilteredSection.cshtml", db.Courses.Include(s => s.Department));
            }
        }
    }
}
