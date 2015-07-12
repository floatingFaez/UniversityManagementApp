using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemApp.Context;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Controllers
{
    public class ClassRoomsController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        // GET: ClassRooms
        public ActionResult Index()
        {
            var classRooms = db.ClassRooms.Include(c => c.Course).Include(c => c.Department);
            return View(classRooms.ToList());
        }

        // GET: ClassRooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassRoom classRoom = db.ClassRooms.Find(id);
            if (classRoom == null)
            {
                return HttpNotFound();
            }
            return View(classRoom);
        }

        // GET: ClassRooms/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
            return View();
        }

        // POST: ClassRooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassRoomId,DepartmentId,CourseId,RoomNo,Days,StartTime,StartAmpm,EndTime,EndAmpm")] ClassRoom classRoom)
        {
            //var check =
            //db.ClassRooms.Where(c => c.RoomNo == classRoom.RoomNo)
            //    .Where(c => c.Days == classRoom.Days)
            //    .Where(c => classRoom.StartTime >= c.StartAmpm || classRoom.StartTime <= );
            
           
            if (ModelState.IsValid)
            {
                db.ClassRooms.Add(classRoom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", classRoom.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", classRoom.DepartmentId);
            return View(classRoom);
        }

        // GET: ClassRooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassRoom classRoom = db.ClassRooms.Find(id);
            if (classRoom == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", classRoom.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", classRoom.DepartmentId);
            return View(classRoom);
        }

        // POST: ClassRooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassRoomId,DepartmentId,CourseId,RoomNo,Days,StartTime,EndTime")] ClassRoom classRoom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classRoom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", classRoom.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", classRoom.DepartmentId);
            return View(classRoom);
        }

        // GET: ClassRooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassRoom classRoom = db.ClassRooms.Find(id);
            if (classRoom == null)
            {
                return HttpNotFound();
            }
            return View(classRoom);
        }

        // POST: ClassRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassRoom classRoom = db.ClassRooms.Find(id);
            db.ClassRooms.Remove(classRoom);
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
        

        public ActionResult ClassShedule(int? departmentId)
        {

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name");
            var shedule = db.Courses.Where(s => s.DepartmentId == departmentId).Distinct().ToList();
            return View(shedule);
        }
    }
}
