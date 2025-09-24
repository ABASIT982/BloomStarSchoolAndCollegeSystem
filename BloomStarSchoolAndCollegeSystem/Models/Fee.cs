using System;
using System.ComponentModel.DataAnnotations;

namespace BloomStarSchoolAndCollegeSystem.Models
{
    public class Fee
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; } // e.g. Monthly Fee, Admission Fee

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        // You may later add StudentType + StudentId if you want to link to either School or College
    }
}
