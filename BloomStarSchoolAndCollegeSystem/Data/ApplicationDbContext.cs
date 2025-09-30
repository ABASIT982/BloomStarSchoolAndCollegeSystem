using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BloomStarSchoolAndCollegeSystem.Models; // make sure your model classes use this namespace

namespace BloomStarSchoolAndCollegeSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ---------------------
        // DbSets (tables)
        // ---------------------
        public DbSet<SchoolStudent> SchoolStudents { get; set; }
        public DbSet<CollegeStudent> CollegeStudents { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<SalaryPayment> SalaryPayments { get; set; }
        public DbSet<Scholarship> Scholarships { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // keep base Identity configuration
            base.OnModelCreating(builder);

            // ---------------------
            // Soft-delete defaults & indexes
            // ---------------------
            builder.Entity<SchoolStudent>().Property(s => s.IsActive).HasDefaultValue(true);
            builder.Entity<CollegeStudent>().Property(s => s.IsActive).HasDefaultValue(true);

            builder.Entity<SchoolStudent>().HasIndex(s => s.Name);
            builder.Entity<CollegeStudent>().HasIndex(c => c.Name);

            // If CollegeStudent has Email property - keep it unique
            builder.Entity<CollegeStudent>()
                   .HasIndex(c => c.Email)
                   .IsUnique()
                   .HasFilter("[Email] IS NOT NULL"); // SQL Server filter so nulls allowed

            // ---------------------
            // Relationships (basic)
            // ---------------------
            // Scholarship -> SchoolStudent / CollegeStudent (nullable FK)
            builder.Entity<SchoolStudent>()
                   .HasOne<Scholarship>()
                   .WithMany()
                   .HasForeignKey(s => s.ScholarshipId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<CollegeStudent>()
                   .HasOne<Scholarship>()
                   .WithMany()
                   .HasForeignKey(c => c.ScholarshipId)
                   .OnDelete(DeleteBehavior.SetNull);

            // Payment -> Fee
            builder.Entity<Payment>()
                   .HasOne<Fee>()
                   .WithMany()
                   .HasForeignKey(p => p.FeeId)
                   .OnDelete(DeleteBehavior.Cascade);

            // SalaryPayment -> Teacher
            builder.Entity<SalaryPayment>()
                   .HasOne<Teacher>()
                   .WithMany()
                   .HasForeignKey(sp => sp.TeacherId)
                   .OnDelete(DeleteBehavior.Cascade);

            // AuditLog -> IdentityUser (store UserId as string FK; nullable if user deleted)
            builder.Entity<AuditLog>()
                   .HasOne<IdentityUser>()
                   .WithMany()
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.SetNull);

            // ---------------------
            // Decimal precision fix (removes warnings)
            // ---------------------
            builder.Entity<Fee>()
                   .Property(f => f.TotalFee)
                   .HasColumnType("decimal(18,2)");

            builder.Entity<Payment>()
                   .Property(p => p.AmountPaid)
                   .HasColumnType("decimal(18,2)");

            builder.Entity<SalaryPayment>()
                   .Property(sp => sp.Amount)
                   .HasColumnType("decimal(18,2)");

            builder.Entity<Scholarship>()
                   .Property(s => s.Percentage)
                   .HasColumnType("decimal(5,2)"); // e.g. 99.99 max percentage

            // ---------------------
            // Table naming / conventions (optional)
            // ---------------------
            builder.Entity<SchoolStudent>().ToTable("SchoolStudents");
            builder.Entity<CollegeStudent>().ToTable("CollegeStudents");
            builder.Entity<Fee>().ToTable("Fees");
            builder.Entity<Payment>().ToTable("Payments");
            builder.Entity<Admission>().ToTable("Admissions");
            builder.Entity<Teacher>().ToTable("Teachers");
            builder.Entity<SalaryPayment>().ToTable("SalaryPayments");
            builder.Entity<Scholarship>().ToTable("Scholarships");
            builder.Entity<AuditLog>().ToTable("AuditLogs");
        }
    }
}
