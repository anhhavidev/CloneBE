using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloneBE.Infraction.Migrations
{
    public partial class th : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "b41e47a9-08a1-4b55-b703-d6cf13be09e6", "3cae9799-232e-4386-9b92-ca06ae0ac787", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CartId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0fbedca0-6589-42df-828a-a5b703f635b6", 0, 0, "7dcfcc4a-7c15-493f-8cbf-66acc2ed6df4", "honganh@gmail.com", true, false, null, "HONGANH@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEC+I/fMEtnrNPCkUFdf7KRU12EtvsGR5ZjvjxeSTMVcgwr+aG1NJViaId4h3aFqd2Q==", null, false, "4d3b3dd4-a38b-474c-944e-4826c146ee2f", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b41e47a9-08a1-4b55-b703-d6cf13be09e6", "0fbedca0-6589-42df-828a-a5b703f635b6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b41e47a9-08a1-4b55-b703-d6cf13be09e6", "0fbedca0-6589-42df-828a-a5b703f635b6" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b41e47a9-08a1-4b55-b703-d6cf13be09e6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0fbedca0-6589-42df-828a-a5b703f635b6");

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
    }
}
