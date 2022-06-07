using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Commerce_App.Migrations
{
    public partial class Add_EditorAndAdmin_Accounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ad376a8f", "3e92001f-f5ac-4c23-9654-977b0032cde8", "Admin", "Admin" },
                    { "bd586a8f", "a986b043-b538-4242-9dcf-5e214d8c7088", "Editor", "Editor" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a18be9c0", 0, "c3c686c8-573d-40bc-ac9e-77cf69ad9433", "admin@gmail.com", false, false, null, "admin@gmail.com", "admin", "AQAAAAEAACcQAAAAEH8K+SkukcRqtnTZSTk8mp+KhnX2YMueM2zkFVssakL+2xmq3kkUUfZOR+IqdcHO6A==", null, false, "", false, "admin" },
                    { "a50ze710", 0, "8c31c88e-a2f4-4ced-8eb9-c9806ef6bdbe", "editor@gmail.com", false, false, null, "editor@gmail.com", "editor", "AQAAAAEAACcQAAAAEMmx68jQJXajOpQ0gx6QN5bFQD9A+gE+nFUfRjw6PGzNLstXBf8MvKmSAaIE83gH5A==", null, false, "", false, "editor" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ad376a8f", "a18be9c0" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "bd586a8f", "a50ze710" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ad376a8f", "a18be9c0" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bd586a8f", "a50ze710" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd586a8f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a50ze710");
        }
    }
}
