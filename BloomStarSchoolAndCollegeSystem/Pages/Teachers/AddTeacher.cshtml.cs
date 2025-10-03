using BloomStarSchoolAndCollegeSystem.Data;
using BloomStarSchoolAndCollegeSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloomStarSchoolAndCollegeSystem.Pages.Teachers
{
    public class AddTeacherModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AddTeacherModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Teacher Teacher { get; set; } = new Teacher();

        [BindProperty]
        public IFormFile? Photo { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            // Handle photo upload
            if (Photo != null)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(Photo.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(stream);
                }

                Teacher.PhotoPath = "/uploads/" + uniqueFileName;
            }

            _context.Teachers.Add(Teacher);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Teacher added successfully!";
            return RedirectToPage("AddTeacher");
        }
    }
}
