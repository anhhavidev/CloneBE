using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloneBE.Infraction.Migrations
{
    public partial class nt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "9676794a-27c1-46a9-ac53-7817c133aed7", "abbd69a6-8aa0-4803-bb42-b011cc153be9", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CartId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f4da1afc-b9ea-4f65-bfd5-36a029d86e3c", 0, 0, "82f428fa-80a6-4a9b-b3f3-0585c51d7846", "honganh@gmail.com", true, false, null, "HONGANH@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEKeg7d0uOQIv/je5otsosBjxPp3+nQPSgm214PivC1mexL8c/Ovd0dj5Gf4PdLlKRw==", null, false, "d8d4432b-fc91-419c-b43c-8241a12103e4", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "9676794a-27c1-46a9-ac53-7817c133aed7", "f4da1afc-b9ea-4f65-bfd5-36a029d86e3c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9676794a-27c1-46a9-ac53-7817c133aed7", "f4da1afc-b9ea-4f65-bfd5-36a029d86e3c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9676794a-27c1-46a9-ac53-7817c133aed7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f4da1afc-b9ea-4f65-bfd5-36a029d86e3c");

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
    }
}
