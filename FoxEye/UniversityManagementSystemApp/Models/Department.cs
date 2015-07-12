using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementSystemApp.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
         [Display(Name = "Department Code")]
         [Remote("CodeExist", "Departments", ErrorMessage = " Code already Exist")]
         [Required(ErrorMessage = " Required")]
        public string Code { get; set; }

         [Display(Name = "Department Name")]
         [Remote("NameExist", "Departments", ErrorMessage = " Name already Exist")]
         [Required(ErrorMessage = " Required")]
        public string Name { get; set; }

        [NotMapped]
        public List<Student> Students { get; set; }
        [NotMapped]
        public List<Course> Courses { get; set; }
        [NotMapped]
        public List<Teacher> Teachers { get; set; }

    }
}