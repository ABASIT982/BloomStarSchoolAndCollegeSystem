using BloomStarSchoolAndCollegeSystem.Data;
using BloomStarSchoolAndCollegeSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BloomStarSchoolAndCollegeSystem.Pages.Teachers
{
    public class PaySalariesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public PaySalariesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Teacher Teacher { get; set; } = new Teacher();

        [BindProperty]
        [Required(ErrorMessage = "Enter the amount to pay.")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be positive.")]
        public decimal PaymentAmount { get; set; }

        public decimal RemainingSalary { get; set; }

        public string? SuccessMessage { get; set; } // Store success message

        public IActionResult OnGet(int id)
        {
            Teacher = _context.Teachers.FirstOrDefault(t => t.Id == id);
            if (Teacher == null)
                return RedirectToPage("TeacherSalaries");

            RemainingSalary = Teacher.NetSalary - Teacher.TotalPaid;
            return Page();
        }

        public IActionResult OnPost()
        {
            var teacher = _context.Teachers.FirstOrDefault(t => t.Id == Teacher.Id);
            if (teacher == null)
                return RedirectToPage("TeacherSalaries");

            decimal remainingBefore = teacher.NetSalary - teacher.TotalPaid;

            if (PaymentAmount <= 0 || PaymentAmount > remainingBefore)
            {
                ModelState.AddModelError("PaymentAmount", "Invalid payment amount.");
                Teacher = teacher;
                RemainingSalary = remainingBefore;
                return Page();
            }

            // Update TotalPaid
            teacher.TotalPaid += PaymentAmount;

            // Update Salary Status
            var remainingAfter = teacher.NetSalary - teacher.TotalPaid;
            if (remainingAfter == 0) teacher.SalaryStatus = "Paid";
            else if (remainingAfter < teacher.NetSalary) teacher.SalaryStatus = "Partial";
            else teacher.SalaryStatus = "Pending";

            _context.SaveChanges();

            // Show message on the same page
            Teacher = teacher;
            RemainingSalary = remainingAfter;
            SuccessMessage = $"Paid {PaymentAmount:C} to {teacher.Name}. Remaining: {remainingAfter:C}";

            return Page();
        }
    }
}
