using System;
using System.ComponentModel.DataAnnotations;

namespace BloomStarSchoolAndCollegeSystem.Models
{
    public class CollegeStudent
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string GradeName { get; set; } // First Year, Second Year

        [Required]
        public string Department { get; set; } // Bio, CS, Commerce, etc.

        [Required]
        public string ParentName { get; set; }

        [Required, Phone]
        public string ParentPhone { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }  // ⚡ Add Email here

        [StringLength(200)]
        public string Address { get; set; }

        public bool IsActive { get; set; } = true;

        // Optional photo path
        public string? PhotoPath { get; set; }

        public int? ScholarshipId { get; set; }
    }
}
