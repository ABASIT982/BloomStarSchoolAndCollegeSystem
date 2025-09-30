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
            SchoolStudent = new SchoolStudent();
            CollegeStudent = new CollegeStudent();
        }

        // ✅ Lists for displaying all students
        public List<SchoolStudent> SchoolStudents { get; set; } = new();
        public List<CollegeStudent> CollegeStudents { get; set; } = new();

        // ✅ Single student objects for Add/Edit forms
        [BindProperty]
        public SchoolStudent SchoolStudent { get; set; }

        [BindProperty]
        public CollegeStudent CollegeStudent { get; set; }

        [TempData]
        public string? SuccessMessage { get; set; }

        // ✅ Load data into lists
        public async Task OnGetAsync(int? schoolId, int? collegeId)
        {
            // Load all students for display
            SchoolStudents = await _context.SchoolStudents.ToListAsync();
            CollegeStudents = await _context.CollegeStudents.ToListAsync();

            // If editing
            if (schoolId.HasValue)
            {
                var s = await _context.SchoolStudents.FindAsync(schoolId.Value);
                if (s != null) SchoolStudent = s;
            }

            if (collegeId.HasValue)
            {
                var c = await _context.CollegeStudents.FindAsync(collegeId.Value);
                if (c != null) CollegeStudent = c;
            }
        }

        // ---------------------------
        // Add
        // ---------------------------
        public async Task<IActionResult> OnPostAddSchoolAsync()
        {
            _context.SchoolStudents.Add(SchoolStudent);
            await _context.SaveChangesAsync();
            SuccessMessage = "School student added.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAddCollegeAsync()
        {
            _context.CollegeStudents.Add(CollegeStudent);
            await _context.SaveChangesAsync();
            SuccessMessage = "College student added.";
            return RedirectToPage();
        }

        // ---------------------------
        // Update
        // ---------------------------
        public async Task<IActionResult> OnPostEditSchoolAsync()
        {
            _context.Attach(SchoolStudent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            SuccessMessage = "School student updated.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditCollegeAsync()
        {
            _context.Attach(CollegeStudent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            SuccessMessage = "College student updated.";
            return RedirectToPage();
        }

        // ---------------------------
        // Delete
        // ---------------------------
        public async Task<IActionResult> OnPostDeleteSchoolAsync(int id)
        {
            var student = await _context.SchoolStudents.FindAsync(id);
            if (student != null)
            {
                _context.SchoolStudents.Remove(student);
                await _context.SaveChangesAsync();
                SuccessMessage = "School student deleted.";
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteCollegeAsync(int id)
        {
            var student = await _context.CollegeStudents.FindAsync(id);
            if (student != null)
            {
                _context.CollegeStudents.Remove(student);
                await _context.SaveChangesAsync();
                SuccessMessage = "College student deleted.";
            }
            return RedirectToPage();
        }
    }
}
