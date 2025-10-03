using System.Linq;
using BloomStarSchoolAndCollegeSystem.Data;
using BloomStarSchoolAndCollegeSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public int TeacherId { get; set; }

        [BindProperty]
        public decimal PaidAmount { get; set; }

        public SelectList TeacherList { get; set; } = default!;

        public void OnGet()
        {
            TeacherList = new SelectList(_context.Teachers, "Id", "Name");
        }

        public IActionResult OnPost()
        {
            var teacher = _context.Teachers.FirstOrDefault(t => t.Id == TeacherId);
            if (teacher == null) return Page();

            if (PaidAmount >= teacher.NetSalary)
            {
                teacher.SalaryStatus = "Paid";
            }
            else if (PaidAmount > 0)
            {
                teacher.SalaryStatus = "Partial";
            }
            else
            {
                teacher.SalaryStatus = "Pending";
            }

            _context.SaveChanges();
            return RedirectToPage("TeacherSalaries");
        }
    }
}
