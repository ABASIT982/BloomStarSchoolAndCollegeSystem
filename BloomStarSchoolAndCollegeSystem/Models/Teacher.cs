using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloomStarSchoolAndCollegeSystem.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Subject { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Qualification { get; set; } = string.Empty;

        [Required, StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required, Phone]
        public string Contact { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = "Male"; // Male/Female

        public string? PhotoPath { get; set; } // File path of uploaded photo

        [Required, Range(0, double.MaxValue)]
        public decimal BasicSalary { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Allowance { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Deductions { get; set; }

        [Required, StringLength(20)]
        public string SalaryStatus { get; set; } = "Pending"; // Paid / Pending / Partial

        // Computed, not mapped to DB
        [NotMapped]
        public decimal NetSalary => (BasicSalary + Allowance) - Deductions;
        // Total salary already paid
        [Range(0, double.MaxValue)]
        public decimal TotalPaid { get; set; } = 0;

    }
}
