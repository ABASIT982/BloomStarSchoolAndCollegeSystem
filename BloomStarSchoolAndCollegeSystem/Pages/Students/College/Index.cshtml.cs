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
        public string? Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? GradeName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Department { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }

        public List<string> AllGrades { get; } = new() { "First Year", "Second Year" };

        public List<string> AllDepartments { get; } = new()
        {
            "Pre-Medical", "Pre-Engineering", "Computer Science", "Commerce", "Humanities"
        };

        public async Task OnGetAsync()
        {
            var query = _context.CollegeStudents.AsQueryable();

            if (!string.IsNullOrWhiteSpace(Search))
            {
                query = query.Where(s =>
                    s.Name.Contains(Search) ||
                    s.ParentPhone.Contains(Search) ||
                    s.Id.ToString().Contains(Search));
            }

            if (!string.IsNullOrWhiteSpace(GradeName))
                query = query.Where(s => s.GradeName == GradeName);

            if (!string.IsNullOrWhiteSpace(Department))
                query = query.Where(s => s.Department == Department);

            var students = await query.AsNoTracking().ToListAsync();

            var gradeOrder = AllGrades
                .Select((g, i) => new { g, i })
                .ToDictionary(x => x.g, x => x.i);

            students = SortOrder switch
            {
                "name_desc" => students.OrderByDescending(s => s.Name).ToList(),
                "grade_asc" => students.OrderBy(s => gradeOrder.ContainsKey(s.GradeName) ? gradeOrder[s.GradeName] : int.MaxValue).ToList(),
                "grade_desc" => students.OrderByDescending(s => gradeOrder.ContainsKey(s.GradeName) ? gradeOrder[s.GradeName] : -1).ToList(),
                "name_asc" or null => students.OrderBy(s => s.Name).ToList(),
                _ => students.OrderBy(s => s.Name).ToList()
            };

            FilteredStudents = students;
        }
    }
}
