using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloomStarSchoolAndCollegeSystem.Data;
using BloomStarSchoolAndCollegeSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BloomStarSchoolAndCollegeSystem.Pages.Students.School
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SchoolStudent> FilteredStudents { get; set; } = new List<SchoolStudent>();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }   // nullable to fix warning

        [BindProperty(SupportsGet = true)]
        public string? GradeName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SectionName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }

        // 📌 All Grades
        public List<string> AllGrades { get; } = new()
        {
            "Nursery", "KG", "Prep", "1st", "2nd", "3rd", "4th",
            "5th", "6th", "7th", "8th", "9th", "10th"
        };

        // 📌 All Sections
        public List<string> AllSections { get; } = new()
        {
            "Rose Model", "Rose", "Lily", "Daffodil"
        };

        public async Task OnGetAsync()
        {
            var query = _context.SchoolStudents.AsQueryable();

            // 🔍 Search
            if (!string.IsNullOrWhiteSpace(Search))
            {
                query = query.Where(s =>
                    s.Name.Contains(Search) ||
                    s.ParentPhone.Contains(Search) ||
                    s.Id.ToString().Contains(Search));
            }

            // 🔎 Filter by Grade
            if (!string.IsNullOrWhiteSpace(GradeName))
            {
                query = query.Where(s => s.GradeName == GradeName);
            }

            // 🔎 Filter by Section
            if (!string.IsNullOrWhiteSpace(SectionName))
            {
                query = query.Where(s => s.SectionName == SectionName);
            }

            // ↕ Sorting
            query = SortOrder switch
            {
                "name_desc" => query.OrderByDescending(s => s.Name),
                "grade_asc" => query.OrderBy(s => s.GradeName),
                "grade_desc" => query.OrderByDescending(s => s.GradeName),
                _ => query.OrderBy(s => s.Name) // default sort by Name ascending
            };

            FilteredStudents = await query.AsNoTracking().ToListAsync();
        }
    }
}
