using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemApp.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Display(Name = "Registration No")]
        public string RegNo { get; set; }
         [Display(Name = "Student Name")]
         [Required(ErrorMessage = " Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = " Required")]
        public string Email { get; set; }
        [Display(Name = "Contact No")]
        [Required(ErrorMessage = " Required")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = " Required")]
        public DateTime DateTime { get; set; }
        public string Address { get; set; }
        [Display(Name = "Department")]
        [Required(ErrorMessage = " Required")]
        public int DepartmentId { get; set; }
         [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }  
    }
}