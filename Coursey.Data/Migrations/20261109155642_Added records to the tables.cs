using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Coursey.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addedrecordstothetables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Address", "Age", "FirstName", "LastName", "MainSubject", "Years of Experience" },
                values: new object[,]
                {
                    { 1, "12 Main Street", 42, "John", "Smith", "Mathematics", 12 },
                    { 2, "25 Oak Avenue", 35, "Emily", "Johnson", "Programming", 8 },
                    { 3, "8 Green Road", 48, "Michael", "Brown", "Physics", 20 }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Difficulty", "EndDate", "Name", "StartDate", "Subject", "TeacherId" },
                values: new object[,]
                {
                    { 1, "Advanced", new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Advanced Mathematics", new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mathematics", 1 },
                    { 2, "Beginner", new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Introduction to C#", new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Programming", 2 },
                    { 3, "Intermediate", new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Classical Mechanics", new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Physics", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
