using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloneBE.Infraction.Migrations
{
    public partial class dh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a96b60f8-9a00-4363-967c-839567325c18", "b99432be-ddeb-44b2-9d26-827d328d3d3b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a96b60f8-9a00-4363-967c-839567325c18");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b99432be-ddeb-44b2-9d26-827d328d3d3b");

            migrationBuilder.RenameColumn(
                name: "Quanlity",
                table: "products",
                newName: "stock");

            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "cartItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "quanlity",
                table: "cartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "price",
                table: "cartItems");

            migrationBuilder.DropColumn(
                name: "quanlity",
                table: "cartItems");

            migrationBuilder.RenameColumn(
                name: "stock",
                table: "products",
                newName: "Quanlity");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a96b60f8-9a00-4363-967c-839567325c18", "65398988-6252-4c93-8f40-270688ff622a", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CartId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b99432be-ddeb-44b2-9d26-827d328d3d3b", 0, 0, "cb785f7b-245a-44b5-bd3e-a586ddf46176", "honganh@gmail.com", true, false, null, "HONGANH@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEHPp3d7V0k4hVGwh8VzfEazqvHUmssSEooTgQ9k2uMvXDypwQGYz79/zBMqUX7wf7w==", null, false, "edf0754d-cc7a-4824-8c2b-1f82b5798872", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a96b60f8-9a00-4363-967c-839567325c18", "b99432be-ddeb-44b2-9d26-827d328d3d3b" });
        }
    }
}
