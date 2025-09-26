using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloomStarSchoolAndCollegeSystem.Pages
{
    public class ErrorModel : PageModel
    {
        public string RequestId { get; set; } = string.Empty;
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public void OnGet()
        {
            RequestId = HttpContext.TraceIdentifier;
        }
    }
}
