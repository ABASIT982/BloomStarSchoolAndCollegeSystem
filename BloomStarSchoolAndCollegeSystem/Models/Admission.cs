using System;
using System.ComponentModel.DataAnnotations;

namespace BloomStarSchoolAndCollegeSystem.Models
{
    public class Admission
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string StudentName { get; set; }

        [Required]
        public DateTime AppliedOn { get; set; }

        [Required, StringLength(50)]
        public string Section { get; set; } // School or College

        [StringLength(50)]
        public string? Status { get; set; } // Pending, Approved, Rejected
    }
}
