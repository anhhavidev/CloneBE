using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloneBE.Infraction.Migrations
{
    public partial class gfa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "345da3fb-75e1-4c79-874f-b8f4df613f3f", "30a4972f-3835-4915-bc78-6b1dfb525645" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "345da3fb-75e1-4c79-874f-b8f4df613f3f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "30a4972f-3835-4915-bc78-6b1dfb525645");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0e73a723-fcdd-447c-8d28-03e75ac7c7ad", "32368046-d7b6-471a-83b7-2a69bb3a7308", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CartId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "97a9f8c6-ee77-4abf-a53a-1a2aaaaf2375", 0, 0, "0f6836cd-d0a5-4acf-b3a0-5b5a08abe3bd", "honganh@gmail.com", true, false, null, "HONGANH@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEEPRipFYOpu1QzA4vLfKnCeqhJEKKa3/KuBJu5U507rH0fViZ2pQ8/+y5fHBPgDFAA==", null, false, "7db63738-1182-4b19-82e4-6a968b198e6d", false, "admin" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "ProductId", "CategoryId", "Description", "Name", "Price", "linkimages", "stock" },
                values: new object[,]
                {
                    { 3, 1, "Dành cho địa hình gồ ghề", "Xe đạp leo núi", 1500.75, "img3.jpg", 7 },
                    { 4, 2, "Tiện lợi, gấp gọn dễ dàng", "Xe đạp gấp", 950.0, "img4.jpg", 3 },
                    { 5, 3, "An toàn, thiết kế cho trẻ nhỏ", "Xe đạp trẻ em", 500.0, "img5.jpg", 15 },
                    { 6, 1, "Tốc độ cao, dành cho đường đua", "Xe đạp đua", 1800.0, "img6.jpg", 4 },
                    { 7, 2, "Thiết kế nhẹ nhàng, phù hợp cho nữ", "Xe đạp nữ", 750.0, "img7.jpg", 6 },
                    { 8, 3, "Xe đạp có hỗ trợ điện dành cho trẻ em", "Xe đạp điện trẻ em", 1200.0, "img8.jpg", 8 },
                    { 9, 1, "Xe phù hợp cho du lịch đường dài", "Xe đạp touring", 2000.0, "img9.jpg", 2 },
                    { 10, 2, "Xe đạp nhỏ gọn, phù hợp cho học sinh", "Xe đạp mini", 650.0, "img10.jpg", 9 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0e73a723-fcdd-447c-8d28-03e75ac7c7ad", "97a9f8c6-ee77-4abf-a53a-1a2aaaaf2375" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0e73a723-fcdd-447c-8d28-03e75ac7c7ad", "97a9f8c6-ee77-4abf-a53a-1a2aaaaf2375" });

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e73a723-fcdd-447c-8d28-03e75ac7c7ad");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "97a9f8c6-ee77-4abf-a53a-1a2aaaaf2375");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "345da3fb-75e1-4c79-874f-b8f4df613f3f", "d23b7fa2-6cc8-4d21-882c-e9c3e762fa74", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CartId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "30a4972f-3835-4915-bc78-6b1dfb525645", 0, 0, "b6627077-bd3a-45cf-9664-cb9ca58c48aa", "honganh@gmail.com", true, false, null, "HONGANH@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEB9PD4oB4y6ktvioi+sPrXG8EScNSFIqxxM+ycPW4ElHVglyOUcF9B247ZbJdLlqwQ==", null, false, "dda9a358-6817-4f2e-9d3f-cd3f0c79a433", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "345da3fb-75e1-4c79-874f-b8f4df613f3f", "30a4972f-3835-4915-bc78-6b1dfb525645" });
        }
    }
}
