using System;
using System.ComponentModel.DataAnnotations;

namespace BloomStarSchoolAndCollegeSystem.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public int FeeId { get; set; }

        [Required]
        public decimal AmountPaid { get; set; }

        [Required]
        public DateTime PaidOn { get; set; }

        public string? Remarks { get; set; }
    }
}
