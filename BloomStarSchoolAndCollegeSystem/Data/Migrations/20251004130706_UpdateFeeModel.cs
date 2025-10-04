using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloomStarSchoolAndCollegeSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFeeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "Fees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SectionOrDept",
                table: "Fees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "SectionOrDept",
                table: "Fees");
        }
    }
}
