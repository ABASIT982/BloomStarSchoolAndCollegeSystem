using System.ComponentModel.DataAnnotations;

namespace BloomStarSchoolAndCollegeSystem.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }

        // School or College
        [Required]
        public string StudentType { get; set; } = string.Empty; // "School" or "College"

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string GradeName { get; set; } = string.Empty;

        // For School Students
        public string? SectionName { get; set; }

        // For College Students
        public string? Department { get; set; }

        [Required]
        public string ParentName { get; set; } = string.Empty;

        [Required, Phone]
        public string ParentPhone { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }   // Nullable for school students

        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        // ✅ Student Photo
        public string? PhotoPath { get; set; }

        // ✅ For file upload binding
        public IFormFile? Photo { get; set; }

        // ✅ Scholarship (nullable)
        public int? ScholarshipId { get; set; }

        // ✅ Success Message (nullable)
        public string? SuccessMessage { get; set; }
    }
}
