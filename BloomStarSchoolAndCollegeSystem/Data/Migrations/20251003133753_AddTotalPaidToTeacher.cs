using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloomStarSchoolAndCollegeSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalPaidToTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPaid",
                table: "Teachers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPaid",
                table: "Teachers");
        }
    }
}
