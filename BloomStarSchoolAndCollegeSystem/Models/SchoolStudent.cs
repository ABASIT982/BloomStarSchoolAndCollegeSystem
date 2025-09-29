using System;
using System.ComponentModel.DataAnnotations;

namespace BloomStarSchoolAndCollegeSystem.Models
{
    public class SchoolStudent
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        // ✅ Grade: Nursery, KG, Prep, 1 … 10
        [Required]
        public string GradeName { get; set; }

        // ✅ Section: Rose Model, Rose, Lily, Daffodil
        [Required]
        public string SectionName { get; set; }

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
