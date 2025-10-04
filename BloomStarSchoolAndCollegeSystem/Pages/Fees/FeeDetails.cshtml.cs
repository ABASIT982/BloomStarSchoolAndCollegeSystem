using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BloomStarSchoolAndCollegeSystem.Data;
using BloomStarSchoolAndCollegeSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloomStarSchoolAndCollegeSystem.Pages.Fees
{
    public class FeeDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public FeeDetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SelectedSection { get; set; } = "";

        [BindProperty(SupportsGet = true)]
        public string SelectedClassYear { get; set; } = "";

        [BindProperty(SupportsGet = true)]
        public string SelectedSectionOrDept { get; set; } = "";

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; } = "";

        public List<Fee> FilteredFees { get; set; } = new();

        public async Task OnGetAsync()
        {
            var query = _context.Fees.AsQueryable();

            if (!string.IsNullOrEmpty(SelectedSection)) query = query.Where(f => f.Section.ToString() == SelectedSection);
            if (!string.IsNullOrEmpty(SelectedClassYear)) query = query.Where(f => f.ClassYear == SelectedClassYear);
            if (!string.IsNullOrEmpty(SelectedSectionOrDept)) query = query.Where(f => f.SectionOrDept == SelectedSectionOrDept);
            if (!string.IsNullOrEmpty(SearchQuery)) query = query.Where(f => f.Name.Contains(SearchQuery) || f.FatherName.Contains(SearchQuery));

            FilteredFees = await query.ToListAsync();
        }
    }
}
