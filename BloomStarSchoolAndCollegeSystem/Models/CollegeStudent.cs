using System;
using System.ComponentModel.DataAnnotations;

namespace BloomStarSchoolAndCollegeSystem.Models
{
    public class CollegeStudent
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [Required]
        public string Department { get; set; } // Medical, Arts, Social Sciences, Engineering, Computer Science

        [StringLength(200)]
        public string Address { get; set; }

        public bool IsActive { get; set; } = true;

        public string? ParentName { get; set; }
        [Phone]
        public string? ParentPhone { get; set; }

        public string? PhotoPath { get; set; }

        public int? ScholarshipId { get; set; }
    }
}
