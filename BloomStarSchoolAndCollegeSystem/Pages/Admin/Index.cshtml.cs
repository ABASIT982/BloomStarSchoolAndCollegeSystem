using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BloomStarSchoolAndCollegeSystem.Data;

namespace BloomStarSchoolAndCollegeSystem.Pages.Admin
{
    /// <summary>
    /// Admin Dashboard – shows key statistics such as
    /// total students, fees collected and scholarships count.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>Total active school students.</summary>
        public int SchoolStudentCount { get; private set; }

        /// <summary>Total active college students.</summary>
        public int CollegeStudentCount { get; private set; }

        /// <summary>Total amount of fees collected (PKR).</summary>
        public decimal TotalFeesCollected { get; private set; }

        /// <summary>Total scholarships awarded.</summary>
        public int ScholarshipsCount { get; private set; }

        /// <summary>
        /// Loads dashboard statistics from the database.
        /// </summary>
        public async Task OnGetAsync()
        {
            // Ensure database is available before querying
            if (_context == null) return;

            SchoolStudentCount = await _context.SchoolStudents
                                               .CountAsync(s => s.IsActive);
            CollegeStudentCount = await _context.CollegeStudents
                                               .CountAsync(c => c.IsActive);
            TotalFeesCollected = await _context.Payments
                                               .SumAsync(p => (decimal?)p.AmountPaid) ?? 0m;
            ScholarshipsCount = await _context.Scholarships.CountAsync();
        }
    }
}
