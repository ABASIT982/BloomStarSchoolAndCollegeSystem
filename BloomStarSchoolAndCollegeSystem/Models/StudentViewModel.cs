using System.ComponentModel.DataAnnotations;

namespace BloomStarSchoolAndCollegeSystem.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string StudentType { get; set; } = string.Empty; // "School" or "College"

        // School only
        public string? GradeName { get; set; } // Nursery … 10
        public string? SectionName { get; set; } // Rose, Lily, etc.

        // College only
        public string? Department { get; set; } // Bio, CS, Commerce
        public string? Year { get; set; } // First Year, Second Year

        [Required]
        public string ParentName { get; set; } = string.Empty;

        [Required, Phone]
        public string ParentPhone { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; } // Only college

        public string? Address { get; set; }

        public bool IsActive { get; set; } = true;

        public string? SearchTerm { get; set; }
    }
}
