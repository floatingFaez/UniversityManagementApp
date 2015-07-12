using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemApp.Models
{
    public class ClassRoom
    {
        [Key]
        [Display(Name = "Class Room")]
        public int ClassRoomId { get; set; }
        [Display(Name = "Department")]
        [Required(ErrorMessage = " Required")]
        public int DepartmentId { get; set; }
        [Display(Name = "Course")]
        [Required(ErrorMessage = " Required")]
        public int CourseId { get; set; }
        [Display(Name = "Room No")]
        [Required(ErrorMessage = " Required")]
        public string RoomNo { get; set; }
        [Required(ErrorMessage = " Required")]
        public string Days { get; set; }
        [Display(Name = "Start Time")]
        [Required(ErrorMessage = " Required")]
        public string StartTime { get; set; } 
        [Display(Name = "End Time")]
        [Required(ErrorMessage = " Required")]
        public string EndTime { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }


    }
}