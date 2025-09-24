using System;
using System.ComponentModel.DataAnnotations;

namespace BloomStarSchoolAndCollegeSystem.Models
{
    public class SalaryPayment
    {
        public int Id { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaidOn { get; set; }

        public string? Remarks { get; set; }
    }
}
