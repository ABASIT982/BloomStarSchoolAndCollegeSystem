using System;
using System.ComponentModel.DataAnnotations;

namespace BloomStarSchoolAndCollegeSystem.Models
{
    public class Scholarship
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, Range(0, 100)]
        public decimal Percentage { get; set; } // e.g. 50% fee off

        public string? Description { get; set; }
    }
}
