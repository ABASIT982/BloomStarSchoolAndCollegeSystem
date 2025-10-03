using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloomStarSchoolAndCollegeSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTeacherWithSalary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Teachers");

            migrationBuilder.RenameColumn(
                name: "Designation",
                table: "Teachers",
                newName: "Subject");

            migrationBuilder.RenameColumn(
                name: "Department",
                table: "Teachers",
                newName: "Qualification");

            migrationBuilder.AddColumn<decimal>(
                name: "Allowance",
                table: "Teachers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BasicSalary",
                table: "Teachers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Deductions",
                table: "Teachers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "SalaryStatus",
                table: "Teachers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Allowance",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "BasicSalary",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Deductions",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "SalaryStatus",
                table: "Teachers");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "Teachers",
                newName: "Designation");

            migrationBuilder.RenameColumn(
                name: "Qualification",
                table: "Teachers",
                newName: "Department");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
