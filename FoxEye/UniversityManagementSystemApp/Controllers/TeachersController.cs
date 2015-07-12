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
    public class TeachersController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        // GET: Teachers
        public ActionResult Index()
        {
            var teachers = db.Teachers.Include(t => t.Department);
            return View(teachers.ToList());
        }

        // GET: Teachers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeacherId,Name,Address,Email,Designation,Contact,DepartmentId,Credit")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", teacher.DepartmentId);
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", teacher.DepartmentId);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeacherId,Name,Address,Email,Designation,Contact,DepartmentId,Credit")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", teacher.DepartmentId);
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
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

        public JsonResult EmailExist(string email)
        {
            var hasEmail = db.Teachers.Where(m => m.Email == email).FirstOrDefault();
            if (hasEmail != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetTeacherCreditInfo(int? teacherId)
        {

            var query = db.CourseAssigns.FirstOrDefault(c => c.TeacherId == teacherId);
            if (query != null)
            {
                var teacherInfo1 =
                db.Teachers.Where(m => m.TeacherId == teacherId)
                    .Select(
                        c =>
                            new CreditView()
                            {
                                AssignCredit = c.Credit,
                                RemainingCredit = c.Credit - db.CourseAssigns.Where(x => x.TeacherId == teacherId).Sum(s => s.Credit)
                            }).FirstOrDefault();
                return Json(teacherInfo1, JsonRequestBehavior.AllowGet);
            }
            var teacherInfo2 =
                db.Teachers.Where(m => m.TeacherId == teacherId)
                    .Select(
                        c =>
                            new CreditView()
                            {
                                AssignCredit = c.Credit,
                                RemainingCredit = c.Credit
                            }).FirstOrDefault();
            return Json(teacherInfo2, JsonRequestBehavior.AllowGet);
            return null;

           

        }

        public JsonResult UpdateCredit(int? teacherId, int? updateCradit)
        {
            var updateCradits = db.Teachers.First(x => x.TeacherId == teacherId).Credit += Convert.ToInt16(updateCradit);
            db.SaveChanges();
            return Json(updateCradits, JsonRequestBehavior.AllowGet);
        }



    }
}
