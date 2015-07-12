using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementSystemApp.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }
        [Required(ErrorMessage = " Required")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = " Required")]
        //[Remote("HasCourse", "Enrollments", ErrorMessage = "This course already have a student")]
        public int CourseId { get; set; }
        [Display(Name = "Enrollment Date")]
        [Required(ErrorMessage = " Required")]

        public DateTime EnrollmentDate { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
    }
}