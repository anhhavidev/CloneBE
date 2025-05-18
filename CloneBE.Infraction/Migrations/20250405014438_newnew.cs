using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloneBE.Infraction.Migrations
{
    public partial class newnew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0e73a723-fcdd-447c-8d28-03e75ac7c7ad", "97a9f8c6-ee77-4abf-a53a-1a2aaaaf2375" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e73a723-fcdd-447c-8d28-03e75ac7c7ad");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "97a9f8c6-ee77-4abf-a53a-1a2aaaaf2375");

            migrationBuilder.RenameColumn(
                name: "linkimages",
                table: "products",
                newName: "LinkImagesPath");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "LinkImagesPath",
                table: "products",
                newName: "linkimages");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0e73a723-fcdd-447c-8d28-03e75ac7c7ad", "32368046-d7b6-471a-83b7-2a69bb3a7308", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CartId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "97a9f8c6-ee77-4abf-a53a-1a2aaaaf2375", 0, 0, "0f6836cd-d0a5-4acf-b3a0-5b5a08abe3bd", "honganh@gmail.com", true, false, null, "HONGANH@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEEPRipFYOpu1QzA4vLfKnCeqhJEKKa3/KuBJu5U507rH0fViZ2pQ8/+y5fHBPgDFAA==", null, false, "7db63738-1182-4b19-82e4-6a968b198e6d", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0e73a723-fcdd-447c-8d28-03e75ac7c7ad", "97a9f8c6-ee77-4abf-a53a-1a2aaaaf2375" });
        }
    }
}
