using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloomStarSchoolAndCollegeSystem.Data;
using BloomStarSchoolAndCollegeSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

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
        public string? Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? GradeName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SectionName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }

        // grade order from lowest to highest
        public List<string> AllGrades { get; } = new()
        {
            "Nursery", "KG", "Prep", "1st", "2nd", "3rd", "4th",
            "5th", "6th", "7th", "8th", "9th", "10th"
        };

        public List<string> AllSections { get; } = new()
        {
            "Rose Model", "Rose", "Lily", "Daffodil"
        };

        public async Task OnGetAsync()
        {
            var query = _context.SchoolStudents.AsQueryable();

            // search
            if (!string.IsNullOrWhiteSpace(Search))
            {
                query = query.Where(s =>
                    s.Name.Contains(Search) ||
                    s.ParentPhone.Contains(Search) ||
                    s.Id.ToString().Contains(Search));
            }

            // grade filter
            if (!string.IsNullOrWhiteSpace(GradeName))
            {
                query = query.Where(s => s.GradeName == GradeName);
            }

            // section filter
            if (!string.IsNullOrWhiteSpace(SectionName))
            {
                query = query.Where(s => s.SectionName == SectionName);
            }

            var students = await query.AsNoTracking().ToListAsync();

            // custom grade order dictionary
            var gradeOrder = AllGrades.Select((g, i) => new { g, i })
                                      .ToDictionary(x => x.g, x => x.i);

            // sorting
            students = SortOrder switch
            {
                "name_desc" => students.OrderByDescending(s => s.Name).ToList(),
                "grade_asc" => students.OrderBy(s => gradeOrder.ContainsKey(s.GradeName) ? gradeOrder[s.GradeName] : int.MaxValue).ToList(),
                "grade_desc" => students.OrderByDescending(s => gradeOrder.ContainsKey(s.GradeName) ? gradeOrder[s.GradeName] : -1).ToList(),
                "name_asc" or null => students.OrderBy(s => s.Name).ToList(),
                _ => students.OrderBy(s => s.Name).ToList()
            };

            FilteredStudents = students;
            // return Page() implicitly
        }
    }
}
