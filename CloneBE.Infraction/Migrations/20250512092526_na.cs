using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloneBE.Infraction.Migrations
{
    public partial class na : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "stock",
                table: "products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "27e41a1b-4d16-4f23-8e11-872dea85d0df", "068553ec-c9ed-4392-8d61-d346221450d4", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CartId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "18a32fcb-b756-4a9d-a9ad-54f62234e841", 0, 0, "8ae0dee2-e25f-43b8-9ec9-b85c98a1d15b", "honganh@gmail.com", true, false, null, "HONGANH@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEDK06ROq938hA2b93XZf9vC4V3OLbpqDX0ABKWarQh2nfK+m3jrJw7RQ1ab0k/K28Q==", null, false, "c3dbbc1e-2b21-4e26-ad8c-0fcbd8aa35d8", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "27e41a1b-4d16-4f23-8e11-872dea85d0df", "18a32fcb-b756-4a9d-a9ad-54f62234e841" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "27e41a1b-4d16-4f23-8e11-872dea85d0df", "18a32fcb-b756-4a9d-a9ad-54f62234e841" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27e41a1b-4d16-4f23-8e11-872dea85d0df");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "18a32fcb-b756-4a9d-a9ad-54f62234e841");

            migrationBuilder.AlterColumn<int>(
                name: "stock",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
