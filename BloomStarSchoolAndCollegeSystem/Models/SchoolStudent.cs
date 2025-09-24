using System;
using System.ComponentModel.DataAnnotations;

namespace BloomStarSchoolAndCollegeSystem.Models
{
    public class SchoolStudent
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string GradeName { get; set; } // Rose, Lily, Daffodil, Rose Model

        [Required]
        public string ParentName { get; set; }

        [Required, Phone]
        public string ParentPhone { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public bool IsActive { get; set; } = true;

        // Optional photo path if later needed
        public string? PhotoPath { get; set; }

        // Scholarship (nullable)
        public int? ScholarshipId { get; set; }
    }
}
