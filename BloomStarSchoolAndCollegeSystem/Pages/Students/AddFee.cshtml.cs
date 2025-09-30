using BloomStarSchoolAndCollegeSystem.Data;
using BloomStarSchoolAndCollegeSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloomStarSchoolAndCollegeSystem.Pages.Students
{
    public class AddFeeModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public AddFeeModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string SelectedSection { get; set; } = string.Empty;

        [BindProperty]
        public int StudentId { get; set; }

        [BindProperty]
        public decimal PaidAmount { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Fetch student dynamically from DB
            object student = SelectedSection == "School"
                ? _context.SchoolStudents.Find(StudentId)
                : _context.CollegeStudents.Find(StudentId);

            if (student == null)
            {
                ModelState.AddModelError(string.Empty, "Student not found.");
                return Page();
            }

            // Cast student to dynamic to read properties (Name, RegNo, ClassYear, TotalFee)
            dynamic s = student;

            var fee = new Fee
            {
                RegNo = s.RegNo,
                Name = s.Name,
                Section = SelectedSection == "School" ? StudentSection.School : StudentSection.College,
                ClassYear = s.ClassYear,
                TotalFee = s.TotalFee,
                PaidAmount = PaidAmount,
                DueAmount = s.TotalFee - PaidAmount
            };

            _context.Fees.Add(fee);
            _context.SaveChanges();

            return RedirectToPage("/Students/FeeDetails/Index");
        }
    }
}
