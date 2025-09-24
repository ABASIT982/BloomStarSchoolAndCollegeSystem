using System;

namespace BloomStarSchoolAndCollegeSystem.Models
{
    public class AuditLog
    {
        public int Id { get; set; }

        public string? UserId { get; set; } // from IdentityUser

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public string Action { get; set; }  // e.g. "Created Fee Record"

        public string? Details { get; set; }
    }
}
