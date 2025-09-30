using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloomStarSchoolAndCollegeSystem.Data;
using BloomStarSchoolAndCollegeSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BloomStarSchoolAndCollegeSystem.Pages.Students.College
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CollegeStudent> FilteredStudents { get; set; } = new List<CollegeStudent>();

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public string GradeName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Department { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }

        public List<string> AllGrades { get; } = new()
        {
            "First Year", "Second Year"
        };

        public List<string> AllDepartments { get; } = new()
        {
            "Pre-Medical", "Pre-Engineering", "Computer Science", "Commerce", "Humanities"
        };

        public async Task OnGetAsync()
        {
            var query = _context.CollegeStudents.AsQueryable();

            // 🔍 Search by Id, Name or ParentPhone
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

            // 🔎 Filter by Department
            if (!string.IsNullOrWhiteSpace(Department))
            {
                query = query.Where(s => s.Department == Department);
            }

            // ↕ Sort
            query = SortOrder switch
            {
                "name_desc" => query.OrderByDescending(s => s.Name),
                "grade_asc" => query.OrderBy(s => s.GradeName),
                "grade_desc" => query.OrderByDescending(s => s.GradeName),
                _ => query.OrderBy(s => s.Name) // default name ascending
            };

            FilteredStudents = await query.AsNoTracking().ToListAsync();
        }
    }
}
