using BloomStarSchoolAndCollegeSystem.Data;
using BloomStarSchoolAndCollegeSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace BloomStarSchoolAndCollegeSystem.Pages.Teachers
{
    public class TeacherSalariesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public TeacherSalariesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Teacher> Teachers { get; set; } = new();

        public void OnGet()
        {
            Teachers = _context.Teachers.ToList();
        }
    }
}
