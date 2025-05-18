using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloneBE.Infraction.Migrations
{
    public partial class nit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "99f13267-1229-477f-88df-c55a617ac247", "4832ad40-d1f4-4f6b-b651-7223e677d545" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99f13267-1229-477f-88df-c55a617ac247");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4832ad40-d1f4-4f6b-b651-7223e677d545");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7023acd3-a5fb-4181-a7b2-cda4a85a80f3", "31506f98-69ae-430c-9026-c725b405ee14", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CartId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a1f8fc49-0b42-4fca-8d11-bd7b64f929e2", 0, 0, "82a44453-26a8-40cf-94d9-8b1f745e8951", "honganh@gmail.com", true, false, null, "HONGANH@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEAQYBxkXoZ4V9RXZdY8yuGrIdKV+4iR8hR/OZyCy/UOtbIGVX7MC3/3k4vw4asIOZw==", null, false, "cfbd61f1-3d25-413c-95cc-1ca4d8013ed5", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7023acd3-a5fb-4181-a7b2-cda4a85a80f3", "a1f8fc49-0b42-4fca-8d11-bd7b64f929e2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7023acd3-a5fb-4181-a7b2-cda4a85a80f3", "a1f8fc49-0b42-4fca-8d11-bd7b64f929e2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7023acd3-a5fb-4181-a7b2-cda4a85a80f3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1f8fc49-0b42-4fca-8d11-bd7b64f929e2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "99f13267-1229-477f-88df-c55a617ac247", "53a8a0aa-235b-45f0-9425-4234805c7a72", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CartId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4832ad40-d1f4-4f6b-b651-7223e677d545", 0, 0, "2c41cb16-4b5b-4ce8-b4f1-7862aa2723ed", "honganh@gmail.com", true, false, null, "HONGANH@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEKLOkiXKgvFOdqI+CRYGlz6tkKk2IIi18lNfqW+Cf60pz9yV9IOF862SEH3X8uzqwg==", null, false, "cfc5086d-b424-4d02-b34d-67a9f295634e", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "99f13267-1229-477f-88df-c55a617ac247", "4832ad40-d1f4-4f6b-b651-7223e677d545" });
        }
    }
}
