using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementSystemApp.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [Display(Name = "Course Code")]
        [Required(ErrorMessage = " Required")]
        [Remote("CodeExist", "Courses", ErrorMessage = " Code already Exist")]
        public string Code { get; set; }
        [Required(ErrorMessage = " Required")]
        public int Credit { get; set; }
        [Remote("NameExist", "Courses", ErrorMessage = " Name already Exist")]
        [Display(Name = "Course Name")]
        [Required(ErrorMessage = " Required")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = " Required")]
        public string Semester { get; set; }
        [Display(Name = "Department")]
        [Required(ErrorMessage = " Required")]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }


    }
}