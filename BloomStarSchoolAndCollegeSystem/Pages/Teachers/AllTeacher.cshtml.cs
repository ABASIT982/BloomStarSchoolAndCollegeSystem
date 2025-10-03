using System.Collections.Generic;
using BloomStarSchoolAndCollegeSystem.Data;
using BloomStarSchoolAndCollegeSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BloomStarSchoolAndCollegeSystem.Pages.Teachers
{
    public class AllTeachersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AllTeachersModel(ApplicationDbContext context)
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
