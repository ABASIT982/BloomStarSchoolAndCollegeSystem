using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BloomStarSchoolAndCollegeSystem.Data;
using BloomStarSchoolAndCollegeSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BloomStarSchoolAndCollegeSystem.Pages.Admissions
{
    public class NewAdmissionModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public NewAdmissionModel(ApplicationDbContext context)
        {
            _context = context;
            StudentType = "";
            SelectedGrade = "";
            SelectedSection = "";
            SuccessMessage = "";
            GradeLabel = "Class / Year";
            SectionLabel = "Section / Department";
            SchoolStudent = new SchoolStudent();
            CollegeStudent = new CollegeStudent();
        }

        [BindProperty]
        public string StudentType { get; set; }

        [BindProperty]
        public string SelectedGrade { get; set; }

        [BindProperty]
        public string SelectedSection { get; set; }

        [BindProperty]
        public SchoolStudent SchoolStudent { get; set; }

        [BindProperty]
        public CollegeStudent CollegeStudent { get; set; }

        [BindProperty]
        public IFormFile UploadedPhoto { get; set; }

        public string SuccessMessage { get; set; }

        public string GradeLabel { get; set; }
        public string SectionLabel { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (StudentType == "School")
            {
                SchoolStudent.GradeName = SelectedGrade;
                SchoolStudent.SectionName = SelectedSection;

                if (UploadedPhoto != null)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(UploadedPhoto.FileName);
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);

                    var filePath = Path.Combine(uploadPath, fileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await UploadedPhoto.CopyToAsync(stream);
                    SchoolStudent.PhotoPath = "/uploads/" + fileName;
                }

                _context.SchoolStudents.Add(SchoolStudent);
                await _context.SaveChangesAsync();

                SuccessMessage = $"School student {SchoolStudent.Name} added successfully!";
            }
            else if (StudentType == "College")
            {
                CollegeStudent.GradeName = SelectedGrade;
                CollegeStudent.Department = SelectedSection;

                if (UploadedPhoto != null)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(UploadedPhoto.FileName);
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);

                    var filePath = Path.Combine(uploadPath, fileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await UploadedPhoto.CopyToAsync(stream);
                    CollegeStudent.PhotoPath = "/uploads/" + fileName;
                }

                _context.CollegeStudents.Add(CollegeStudent);
                await _context.SaveChangesAsync();

                SuccessMessage = $"College student {CollegeStudent.Name} added successfully!";
            }
            else
            {
                SuccessMessage = "Please select a valid Student Type.";
            }

            // Reset form
            SchoolStudent = new SchoolStudent();
            CollegeStudent = new CollegeStudent();
            StudentType = "";
            SelectedGrade = "";
            SelectedSection = "";

            return Page();
        }
    }
}
