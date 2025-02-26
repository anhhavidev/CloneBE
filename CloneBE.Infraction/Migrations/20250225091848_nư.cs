using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloneBE.Infraction.Migrations
{
    public partial class nư : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "556b6245-7109-4aad-a83d-97afaf8891ad", "8e834649-38ea-4d22-bee6-333cdb4538bb", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e77ccc94-77ef-4ad4-9951-038c0841d751", 0, "a7221e30-3949-4a8f-8859-3e4aaf5e672e", "honganh@gmail.com", true, false, null, "HONGANH@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAED8eUgQURUK9IiN/T1eeKSKGae1pjRiGsb8dpRfryMVTw2KrE1qyAw8XylVo/Lmjxg==", null, false, "75a04ba5-3fb7-480f-8036-c91becf816db", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "556b6245-7109-4aad-a83d-97afaf8891ad", "e77ccc94-77ef-4ad4-9951-038c0841d751" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "556b6245-7109-4aad-a83d-97afaf8891ad", "e77ccc94-77ef-4ad4-9951-038c0841d751" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "556b6245-7109-4aad-a83d-97afaf8891ad");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e77ccc94-77ef-4ad4-9951-038c0841d751");
        }
    }
}
