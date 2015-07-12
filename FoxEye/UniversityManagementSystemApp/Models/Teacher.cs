using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementSystemApp.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
         [Display(Name = "Teacher Name")]
         [Required(ErrorMessage = " Required")]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = " Required")]
        [Remote("EmailExist", "Teachers", ErrorMessage = " Email already Exist")]
        public string Email { get; set; }
        [Required(ErrorMessage = " Required")]
        public string Designation { get; set; }
        [Display(Name = "Contact No")]
        [Required(ErrorMessage = " Required")]
        public string Contact { get; set; }
        [Display(Name = "Department")]
        [Required(ErrorMessage = " Required")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = " Required")]
        public int Credit { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        [NotMapped]
        public List<Course> Courses { get; set; }
    }
}