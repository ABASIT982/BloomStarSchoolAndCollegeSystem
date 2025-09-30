using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloomStarSchoolAndCollegeSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFeeSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Fees");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Fees",
                newName: "TotalFee");

            migrationBuilder.AddColumn<string>(
                name: "ClassYear",
                table: "Fees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Fees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PaidAmount",
                table: "Fees",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "RegNo",
                table: "Fees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Section",
                table: "Fees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassYear",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "PaidAmount",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "RegNo",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "Section",
                table: "Fees");

            migrationBuilder.RenameColumn(
                name: "TotalFee",
                table: "Fees",
                newName: "Amount");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Fees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Fees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
