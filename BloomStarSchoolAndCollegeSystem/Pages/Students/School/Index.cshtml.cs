using System;
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
        public string Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public string GradeName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SectionName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }

        public List<string> AllGrades { get; } = new()
        {
            "Nursery","KG","Prep",
            "1","2","3","4","5","6","7","8","9","10"
        };

        public List<string> AllSections { get; } = new()
        {
            "Rose Model","Rose","Lily","Daffodil"
        };

        public async Task OnGetAsync()
        {
            var query = _context.SchoolStudents.AsQueryable();

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

            // 🔎 Filter by Section
            if (!string.IsNullOrWhiteSpace(SectionName))
            {
                query = query.Where(s => s.SectionName == SectionName);
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
