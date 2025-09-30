using System.ComponentModel.DataAnnotations;

namespace BloomStarSchoolAndCollegeSystem.Models
{
    public enum StudentSection
    {
        School,
        College
    }

    public class Fee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RegNo { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string ClassYear { get; set; } = string.Empty;

        [Required]
        public StudentSection Section { get; set; } = StudentSection.School;

        [Required]
        public decimal TotalFee { get; set; }

        [Required]
        public decimal PaidAmount { get; set; }

        public decimal DueAmount { get; set; }
    }
}
