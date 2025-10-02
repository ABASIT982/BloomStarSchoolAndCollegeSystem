using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloomStarSchoolAndCollegeSystem.Data;
using BloomStarSchoolAndCollegeSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BloomStarSchoolAndCollegeSystem.Pages.Students
{
    public class ManageStudentsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ManageStudentsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SchoolStudent> SchoolStudents { get; set; }
        public IList<CollegeStudent> CollegeStudents { get; set; }

        [BindProperty]
        public SchoolStudent SchoolStu { get; set; }

        [BindProperty]
        public CollegeStudent CollegeStu { get; set; }

        [BindProperty]
        public string SearchTerm { get; set; }

        public async Task OnGetAsync()
        {
            SchoolStudents = await _context.SchoolStudents.ToListAsync();
            CollegeStudents = await _context.CollegeStudents.ToListAsync();
        }

        // ------------------ SCHOOL --------------------
        public async Task<IActionResult> OnPostAddSchoolAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.SchoolStudents.Add(SchoolStu);
            await _context.SaveChangesAsync();
            TempData["Success"] = "School student added successfully!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateSchoolAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Attach(SchoolStu).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            TempData["Success"] = "School student updated successfully!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteSchoolAsync(int id)
        {
            var stu = await _context.SchoolStudents.FindAsync(id);
            if (stu != null)
            {
                _context.SchoolStudents.Remove(stu);
                await _context.SaveChangesAsync();
                TempData["Success"] = "School student deleted successfully!";
            }
            return RedirectToPage();
        }

        // ------------------ COLLEGE --------------------
        public async Task<IActionResult> OnPostAddCollegeAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.CollegeStudents.Add(CollegeStu);
            await _context.SaveChangesAsync();
            TempData["Success"] = "College student added successfully!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateCollegeAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Attach(CollegeStu).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            TempData["Success"] = "College student updated successfully!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteCollegeAsync(int id)
        {
            var stu = await _context.CollegeStudents.FindAsync(id);
            if (stu != null)
            {
                _context.CollegeStudents.Remove(stu);
                await _context.SaveChangesAsync();
                TempData["Success"] = "College student deleted successfully!";
            }
            return RedirectToPage();
        }
    }
}
