using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemApp.Models
{
    public class Result
    {
        [Key]
        public int ResultId { get; set; }
        [Required(ErrorMessage = " Required")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = " Required")]
        public int CourseId { get; set; }
         [Display(Name = "Grade Letter")]
        [Required(ErrorMessage = " Required")]
        public string GradeLetter { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
    }
}