using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BloomStarSchoolAndCollegeSystem.Data;
using BloomStarSchoolAndCollegeSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace BloomStarSchoolAndCollegeSystem.Pages.Admissions
{
    public class ManageAdmissionsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ManageAdmissionsModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public string SuccessMessage { get; set; } = "";

        [BindProperty]
        public StudentViewModel EditStudent { get; set; } = new();

        [BindProperty]
        public IFormFile UploadedPhoto { get; set; }

        public List<StudentViewModel> FilteredStudents { get; set; } = new();

        public List<string> AvailableGrades { get; set; } = new();
        public List<string> AvailableSections { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public StudentFilter Filter { get; set; } = new();

        private readonly string[] schoolGrades = { "Nursery", "KG", "Prep", "1st", "2nd", "3rd", "4th", "5th", "6th", "7th", "8th", "9th", "10th" };
        private readonly string[] schoolSections = { "Rose Model", "Rose", "Lily", "Daffodil" };
        private readonly string[] collegeYears = { "First Year", "Second Year" };
        private readonly string[] collegeDepts = { "Pre-Medical", "Pre-Engineering", "Computer Science", "Commerce", "Humanities" };

        public async Task OnGetAsync()
        {
            // show any success message passed by TempData
            if (TempData.ContainsKey("SuccessMessage"))
                SuccessMessage = TempData["SuccessMessage"]?.ToString() ?? "";

            // populate dropdowns with "All" + appropriate lists
            if (Filter.StudentType == "School")
            {
                AvailableGrades = new List<string> { "All Class/Year" }.Concat(schoolGrades).ToList();
                AvailableSections = new List<string> { "All Section/Dept" }.Concat(schoolSections).ToList();
            }
            else if (Filter.StudentType == "College")
            {
                AvailableGrades = new List<string> { "All Class/Year" }.Concat(collegeYears).ToList();
                AvailableSections = new List<string> { "All Section/Dept" }.Concat(collegeDepts).ToList();
            }
            else
            {
                // no specific type chosen -> show union (so filter lists are usable)
                AvailableGrades = new List<string> { "All Class/Year" }
                    .Concat(schoolGrades)
                    .Concat(collegeYears)
                    .ToList();

                AvailableSections = new List<string> { "All Section/Dept" }
                    .Concat(schoolSections)
                    .Concat(collegeDepts)
                    .ToList();
            }

            // base queries
            var schoolQuery = _context.SchoolStudents.Select(s => new StudentViewModel
            {
                Id = s.Id,
                StudentType = "School",
                Name = s.Name,
                ParentName = s.ParentName,
                ParentPhone = s.ParentPhone,
                Address = s.Address,
                GradeName = s.GradeName,
                SectionOrDept = s.SectionName,
                PhotoPath = s.PhotoPath
            });

            var collegeQuery = _context.CollegeStudents.Select(c => new StudentViewModel
            {
                Id = c.Id,
                StudentType = "College",
                Name = c.Name,
                ParentName = c.ParentName,
                ParentPhone = c.ParentPhone,
                Address = c.Address,
                GradeName = c.GradeName,
                SectionOrDept = c.Department,
                PhotoPath = c.PhotoPath
            });

            var all = schoolQuery.Union(collegeQuery);

            // filtering
            if (!string.IsNullOrEmpty(Filter.StudentType) && Filter.StudentType != "All Students")
                all = all.Where(x => x.StudentType == Filter.StudentType);

            if (!string.IsNullOrEmpty(Filter.Grade) && Filter.Grade != "All Class/Year")
                all = all.Where(x => x.GradeName == Filter.Grade);

            if (!string.IsNullOrEmpty(Filter.SectionOrDept) && Filter.SectionOrDept != "All Section/Dept")
                all = all.Where(x => x.SectionOrDept == Filter.SectionOrDept);

            if (!string.IsNullOrEmpty(Filter.SearchQuery))
                all = all.Where(x => x.Name.Contains(Filter.SearchQuery) || x.ParentName.Contains(Filter.SearchQuery));

            FilteredStudents = await all.ToListAsync();
        }

        // Returns JSON options for a given student type -> used by AJAX to re-populate Grade/Section selects
        public IActionResult OnGetOptions(string type)
        {
            if (type == "School")
            {
                return new JsonResult(new { grades = schoolGrades, sections = schoolSections });
            }
            else if (type == "College")
            {
                return new JsonResult(new { grades = collegeYears, sections = collegeDepts });
            }
            // default combined
            return new JsonResult(new { grades = schoolGrades.Concat(collegeYears).ToArray(), sections = schoolSections.Concat(collegeDepts).ToArray() });
        }

        // Returns JSON details for a student (used to prefill update modal)
        public async Task<IActionResult> OnGetDetailsAsync(int id, string type)
        {
            if (string.IsNullOrEmpty(type)) return new JsonResult(new { });

            if (type == "School")
            {
                var s = await _context.SchoolStudents.FindAsync(id);
                if (s == null) return new JsonResult(new { });
                return new JsonResult(new
                {
                    id = s.Id,
                    name = s.Name,
                    parentName = s.ParentName,
                    parentPhone = s.ParentPhone,
                    address = s.Address,
                    grade = s.GradeName,
                    section = s.SectionName,
                    photoPath = s.PhotoPath,
                    grades = schoolGrades,
                    sections = schoolSections
                });
            }
            else
            {
                var c = await _context.CollegeStudents.FindAsync(id);
                if (c == null) return new JsonResult(new { });
                return new JsonResult(new
                {
                    id = c.Id,
                    name = c.Name,
                    parentName = c.ParentName,
                    parentPhone = c.ParentPhone,
                    address = c.Address,
                    grade = c.GradeName,
                    section = c.Department,
                    photoPath = c.PhotoPath,
                    grades = collegeYears,
                    sections = collegeDepts
                });
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, string type)
        {
            if (type == "School")
            {
                var s = await _context.SchoolStudents.FindAsync(id);
                if (s != null) _context.SchoolStudents.Remove(s);
            }
            else if (type == "College")
            {
                var c = await _context.CollegeStudents.FindAsync(id);
                if (c != null) _context.CollegeStudents.Remove(c);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Student deleted successfully.";
            return RedirectToPage(new
            {
                Filter.StudentType,
                Filter.Grade,
                Filter.SectionOrDept,
                Filter.SearchQuery
            });
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (EditStudent.StudentType == "School")
            {
                var s = await _context.SchoolStudents.FindAsync(EditStudent.Id);
                if (s != null)
                {
                    s.Name = EditStudent.Name;
                    s.ParentName = EditStudent.ParentName;
                    s.ParentPhone = EditStudent.ParentPhone;
                    s.Address = EditStudent.Address;
                    s.GradeName = EditStudent.GradeName;
                    s.SectionName = EditStudent.SectionOrDept;

                    if (UploadedPhoto != null)
                    {
                        string fileName = Path.GetFileName(UploadedPhoto.FileName);
                        string path = Path.Combine(_env.WebRootPath, "uploads", fileName);
                        using var stream = new FileStream(path, FileMode.Create);
                        await UploadedPhoto.CopyToAsync(stream);
                        s.PhotoPath = "/uploads/" + fileName;
                    }
                }
            }
            else if (EditStudent.StudentType == "College")
            {
                var c = await _context.CollegeStudents.FindAsync(EditStudent.Id);
                if (c != null)
                {
                    c.Name = EditStudent.Name;
                    c.ParentName = EditStudent.ParentName;
                    c.ParentPhone = EditStudent.ParentPhone;
                    c.Address = EditStudent.Address;
                    c.GradeName = EditStudent.GradeName;
                    c.Department = EditStudent.SectionOrDept;

                    if (UploadedPhoto != null)
                    {
                        string fileName = Path.GetFileName(UploadedPhoto.FileName);
                        string path = Path.Combine(_env.WebRootPath, "uploads", fileName);
                        using var stream = new FileStream(path, FileMode.Create);
                        await UploadedPhoto.CopyToAsync(stream);
                        c.PhotoPath = "/uploads/" + fileName;
                    }
                }
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Student updated successfully.";
            return RedirectToPage(new
            {
                Filter.StudentType,
                Filter.Grade,
                Filter.SectionOrDept,
                Filter.SearchQuery
            });
        }
    }

    public class StudentFilter
    {
        public string StudentType { get; set; } = "";
        public string Grade { get; set; } = "";
        public string SectionOrDept { get; set; } = "";
        public string SearchQuery { get; set; } = "";
    }

    public class StudentViewModel
    {
        public int Id { get; set; }
        public string StudentType { get; set; } = "";
        public string Name { get; set; } = "";
        public string ParentName { get; set; } = "";
        public string ParentPhone { get; set; } = "";
        public string Address { get; set; } = "";
        public string GradeName { get; set; } = "";
        public string SectionOrDept { get; set; } = "";
        public string PhotoPath { get; set; } = "";
    }
}
