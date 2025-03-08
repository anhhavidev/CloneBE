using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloneBE.Infraction.Migrations
{
    public partial class hh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f57bd87e-b8bf-48cb-83f4-0b589a56eda3", "ee6c733c-8ea0-4361-ae55-83b68452b1f7" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f57bd87e-b8bf-48cb-83f4-0b589a56eda3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ee6c733c-8ea0-4361-ae55-83b68452b1f7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b8f4cbc3-8230-40f8-9c7d-d9c8be10e538", "d8c14b08-4f3c-499e-932c-aa8f6cf2c69b", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CartId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "82b53a91-523a-4a49-a3dd-7b4b7c378ef4", 0, 0, "5bb886cd-64ca-4b07-9d04-2e7eb9232418", "honganh@gmail.com", true, false, null, "HONGANH@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEPmb7bDC2sn53G4YL68On/6fdrYO0donhe6RkHZepqTG00kPaWH3cqMnRIhuYW9sXA==", null, false, "e5b5c250-c543-4005-a897-b74d04be1a17", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b8f4cbc3-8230-40f8-9c7d-d9c8be10e538", "82b53a91-523a-4a49-a3dd-7b4b7c378ef4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b8f4cbc3-8230-40f8-9c7d-d9c8be10e538", "82b53a91-523a-4a49-a3dd-7b4b7c378ef4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8f4cbc3-8230-40f8-9c7d-d9c8be10e538");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "82b53a91-523a-4a49-a3dd-7b4b7c378ef4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f57bd87e-b8bf-48cb-83f4-0b589a56eda3", "497701df-165f-4cc0-82ea-7c3d1f6742d1", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CartId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ee6c733c-8ea0-4361-ae55-83b68452b1f7", 0, 0, "af069983-1a82-4c06-a250-218af56b7062", "honganh@gmail.com", true, false, null, "HONGANH@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAECizkNUhIJu8GaxXKg6sfhRZ/M6yHWLB2hhpT2b20cOnn7ZaSE83aQpvwlBlMrfI3w==", null, false, "20027e60-d011-46b5-b02a-f1d7c977f814", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f57bd87e-b8bf-48cb-83f4-0b589a56eda3", "ee6c733c-8ea0-4361-ae55-83b68452b1f7" });
        }
    }
}
