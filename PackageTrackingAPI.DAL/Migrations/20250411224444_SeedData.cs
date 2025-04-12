using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PackageTrackingAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Packages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "Name", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 1, "alice.johnson@example.com", "Alice Johnson", "hashed_password_1", "Admin" },
                    { 2, "bob.smith@example.com", "Bob Smith", "hashed_password_2", "User" },
                    { 3, "charlie.brown@example.com", "Charlie Brown", "hashed_password_3", "User" },
                    { 4, "david.wilson@example.com", "David Wilson", "hashed_password_4", "Manager" },
                    { 5, "emma.davis@example.com", "Emma Davis", "hashed_password_5", "User" }
                });

            migrationBuilder.InsertData(
                table: "Alerts",
                columns: new[] { "AlertID", "Message", "PackageID", "Timestamp", "UserID" },
                values: new object[,]
                {
                    { 1, "Package delayed due to weather conditions.", 101, new DateTime(2025, 4, 11, 5, 30, 0, 0, DateTimeKind.Local), 1 },
                    { 2, "Package out for delivery.", 102, new DateTime(2025, 4, 11, 7, 45, 0, 0, DateTimeKind.Local), 2 },
                    { 3, "Package delivered successfully.", 103, new DateTime(2025, 4, 10, 11, 20, 0, 0, DateTimeKind.Local), 3 },
                    { 4, "Package returned to sender.", 104, new DateTime(2025, 4, 9, 9, 5, 0, 0, DateTimeKind.Local), 4 },
                    { 5, "Package pending at pickup location.", 105, new DateTime(2025, 4, 8, 13, 30, 0, 0, DateTimeKind.Local), 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Alerts",
                keyColumn: "AlertID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Alerts",
                keyColumn: "AlertID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Alerts",
                keyColumn: "AlertID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Alerts",
                keyColumn: "AlertID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Alerts",
                keyColumn: "AlertID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Packages");
        }
    }
}
