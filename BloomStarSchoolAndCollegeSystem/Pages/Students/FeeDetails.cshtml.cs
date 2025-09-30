using BloomStarSchoolAndCollegeSystem.Data;
using BloomStarSchoolAndCollegeSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BloomStarSchoolAndCollegeSystem.Pages.Students
{
    public class FeeDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public FeeDetailsModel(ApplicationDbContext context)
        {
            _context = context;
            FeeRecords = new List<Fee>();
            SearchTerm = string.Empty;
            SelectedSection = string.Empty;
        }

        public IList<Fee> FeeRecords { get; set; }

        public string SearchTerm { get; set; }

        public string SelectedSection { get; set; }

        public async Task OnGetAsync()
        {
            // Start with all Fee records
            var query = _context.Fees.AsQueryable();

            // Filter by search term if provided
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(f => f.Name.Contains(SearchTerm) || f.RegNo.Contains(SearchTerm));
            }

            // Filter by section if provided
            if (!string.IsNullOrEmpty(SelectedSection))
            {
                if (Enum.TryParse<StudentSection>(SelectedSection, out var section))
                {
                    query = query.Where(f => f.Section == section);
                }
            }

            // Load the results ordered by Section and ClassYear
            FeeRecords = await query
                .OrderBy(f => f.Section)
                .ThenBy(f => f.ClassYear)
                .ToListAsync();
        }
    }
}
