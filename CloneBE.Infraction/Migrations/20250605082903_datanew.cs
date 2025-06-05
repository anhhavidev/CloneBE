using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloneBE.Infraction.Migrations
{
    public partial class datanew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "125e9ac4-c829-4c85-963f-573d091ef5f7", "0bde8134-a9c5-45ca-828a-8cb31efb4470", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "CartId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "39f18a9b-ebfa-44ff-b5ee-b54febe86ae2", 0, " g", 0, "60023917-7d7a-4655-b75b-08f223f53f01", "honganh@gmail.com", true, "User", false, null, "HONGANH@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEDU9mYq4FmjSZILaz8GCxnTVgPiPOdRYrt1pcfnkYQDLfAYW5WPn1ZQD+qN4c2//uQ==", "0123456789", false, "14e57540-30a9-4c53-bd8b-ea1108137898", false, "admin" });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Lightweight and portable laptops", "Ultrabooks" });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "High performance laptops for gaming", "Gaming Laptops" });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Laptops designed for business use", "Business Laptops" });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "CategoryId", "Description", "Name" },
                values: new object[,]
                {
                    { 4, "Convertible laptops with touchscreen", "2-in-1 Laptops" },
                    { 5, "Affordable laptops for everyday use", "Budget Laptops" },
                    { 6, "Powerful laptops for professional work", "Workstations" }
                });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[] { "High quality Ultrabook with excellent features, model 1.", "images/ultrabook_model_1.jpg", "Ultrabook Model 1", 1983.6300000000001, 12 });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "CategoryId", "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[] { 1, "High quality Ultrabook with excellent features, model 2.", "images/ultrabook_model_2.jpg", "Ultrabook Model 2", 2611.0599999999999, 8 });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[] { "High quality Ultrabook with excellent features, model 3.", "images/ultrabook_model_3.jpg", "Ultrabook Model 3", 2747.5599999999999, 67 });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "CategoryId", "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[] { 1, "High quality Ultrabook with excellent features, model 4.", "images/ultrabook_model_4.jpg", "Ultrabook Model 4", 1634.98, 93 });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "CategoryId", "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[] { 1, "High quality Ultrabook with excellent features, model 5.", "images/ultrabook_model_5.jpg", "Ultrabook Model 5", 2414.77, 68 });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[] { "High quality Ultrabook with excellent features, model 6.", "images/ultrabook_model_6.jpg", "Ultrabook Model 6", 2845.9099999999999, 49 });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 7,
                columns: new[] { "CategoryId", "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[] { 1, "High quality Ultrabook with excellent features, model 7.", "images/ultrabook_model_7.jpg", "Ultrabook Model 7", 1497.6199999999999, 17 });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "CategoryId", "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[] { 1, "High quality Ultrabook with excellent features, model 8.", "images/ultrabook_model_8.jpg", "Ultrabook Model 8", 1736.77, 86 });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 9,
                columns: new[] { "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[] { "High quality Ultrabook with excellent features, model 9.", "images/ultrabook_model_9.jpg", "Ultrabook Model 9", 574.34000000000003, 78 });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 10,
                columns: new[] { "CategoryId", "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[] { 1, "High quality Ultrabook with excellent features, model 10.", "images/ultrabook_model_10.jpg", "Ultrabook Model 10", 510.61000000000001, 39 });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "ProductId", "CategoryId", "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[,]
                {
                    { 11, 2, "High quality Gaming Laptop with excellent features, model 1.", "images/gaming_laptop_model_1.jpg", "Gaming Laptop Model 1", 1295.2, 71 },
                    { 12, 2, "High quality Gaming Laptop with excellent features, model 2.", "images/gaming_laptop_model_2.jpg", "Gaming Laptop Model 2", 624.09000000000003, 41 },
                    { 13, 2, "High quality Gaming Laptop with excellent features, model 3.", "images/gaming_laptop_model_3.jpg", "Gaming Laptop Model 3", 1842.6199999999999, 80 },
                    { 14, 2, "High quality Gaming Laptop with excellent features, model 4.", "images/gaming_laptop_model_4.jpg", "Gaming Laptop Model 4", 1710.27, 8 },
                    { 15, 2, "High quality Gaming Laptop with excellent features, model 5.", "images/gaming_laptop_model_5.jpg", "Gaming Laptop Model 5", 1318.6600000000001, 33 },
                    { 16, 2, "High quality Gaming Laptop with excellent features, model 6.", "images/gaming_laptop_model_6.jpg", "Gaming Laptop Model 6", 1568.75, 74 },
                    { 17, 2, "High quality Gaming Laptop with excellent features, model 7.", "images/gaming_laptop_model_7.jpg", "Gaming Laptop Model 7", 988.94000000000005, 46 },
                    { 18, 2, "High quality Gaming Laptop with excellent features, model 8.", "images/gaming_laptop_model_8.jpg", "Gaming Laptop Model 8", 1274.4300000000001, 62 },
                    { 19, 2, "High quality Gaming Laptop with excellent features, model 9.", "images/gaming_laptop_model_9.jpg", "Gaming Laptop Model 9", 1579.5999999999999, 25 },
                    { 20, 2, "High quality Gaming Laptop with excellent features, model 10.", "images/gaming_laptop_model_10.jpg", "Gaming Laptop Model 10", 926.39999999999998, 89 },
                    { 21, 3, "High quality Business Laptop with excellent features, model 1.", "images/business_laptop_model_1.jpg", "Business Laptop Model 1", 575.89999999999998, 41 },
                    { 22, 3, "High quality Business Laptop with excellent features, model 2.", "images/business_laptop_model_2.jpg", "Business Laptop Model 2", 1667.8299999999999, 20 },
                    { 23, 3, "High quality Business Laptop with excellent features, model 3.", "images/business_laptop_model_3.jpg", "Business Laptop Model 3", 1272.54, 88 },
                    { 24, 3, "High quality Business Laptop with excellent features, model 4.", "images/business_laptop_model_4.jpg", "Business Laptop Model 4", 2006.95, 29 },
                    { 25, 3, "High quality Business Laptop with excellent features, model 5.", "images/business_laptop_model_5.jpg", "Business Laptop Model 5", 2869.5500000000002, 32 },
                    { 26, 3, "High quality Business Laptop with excellent features, model 6.", "images/business_laptop_model_6.jpg", "Business Laptop Model 6", 1269.6800000000001, 75 },
                    { 27, 3, "High quality Business Laptop with excellent features, model 7.", "images/business_laptop_model_7.jpg", "Business Laptop Model 7", 1909.8199999999999, 34 },
                    { 28, 3, "High quality Business Laptop with excellent features, model 8.", "images/business_laptop_model_8.jpg", "Business Laptop Model 8", 2945.2800000000002, 97 },
                    { 29, 3, "High quality Business Laptop with excellent features, model 9.", "images/business_laptop_model_9.jpg", "Business Laptop Model 9", 2730.5700000000002, 46 },
                    { 30, 3, "High quality Business Laptop with excellent features, model 10.", "images/business_laptop_model_10.jpg", "Business Laptop Model 10", 1130.72, 92 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "125e9ac4-c829-4c85-963f-573d091ef5f7", "39f18a9b-ebfa-44ff-b5ee-b54febe86ae2" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "ProductId", "CategoryId", "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[,]
                {
                    { 31, 4, "High quality 2-in-1 Laptop with excellent features, model 1.", "images/2in1_laptop_model_1.jpg", "2-in-1 Laptop Model 1", 852.25, 90 },
                    { 32, 4, "High quality 2-in-1 Laptop with excellent features, model 2.", "images/2in1_laptop_model_2.jpg", "2-in-1 Laptop Model 2", 1602.3099999999999, 29 },
                    { 33, 4, "High quality 2-in-1 Laptop with excellent features, model 3.", "images/2in1_laptop_model_3.jpg", "2-in-1 Laptop Model 3", 727.42999999999995, 31 },
                    { 34, 4, "High quality 2-in-1 Laptop with excellent features, model 4.", "images/2in1_laptop_model_4.jpg", "2-in-1 Laptop Model 4", 2049.5500000000002, 48 },
                    { 35, 4, "High quality 2-in-1 Laptop with excellent features, model 5.", "images/2in1_laptop_model_5.jpg", "2-in-1 Laptop Model 5", 649.75, 86 },
                    { 36, 4, "High quality 2-in-1 Laptop with excellent features, model 6.", "images/2in1_laptop_model_6.jpg", "2-in-1 Laptop Model 6", 722.19000000000005, 40 },
                    { 37, 4, "High quality 2-in-1 Laptop with excellent features, model 7.", "images/2in1_laptop_model_7.jpg", "2-in-1 Laptop Model 7", 583.40999999999997, 96 },
                    { 38, 4, "High quality 2-in-1 Laptop with excellent features, model 8.", "images/2in1_laptop_model_8.jpg", "2-in-1 Laptop Model 8", 1451.1700000000001, 87 },
                    { 39, 4, "High quality 2-in-1 Laptop with excellent features, model 9.", "images/2in1_laptop_model_9.jpg", "2-in-1 Laptop Model 9", 1356.5699999999999, 25 },
                    { 40, 4, "High quality 2-in-1 Laptop with excellent features, model 10.", "images/2in1_laptop_model_10.jpg", "2-in-1 Laptop Model 10", 2028.5799999999999, 12 },
                    { 41, 5, "High quality Budget Laptop with excellent features, model 1.", "images/budget_laptop_model_1.jpg", "Budget Laptop Model 1", 2016.03, 48 },
                    { 42, 5, "High quality Budget Laptop with excellent features, model 2.", "images/budget_laptop_model_2.jpg", "Budget Laptop Model 2", 538.02999999999997, 52 },
                    { 43, 5, "High quality Budget Laptop with excellent features, model 3.", "images/budget_laptop_model_3.jpg", "Budget Laptop Model 3", 1384.6900000000001, 59 },
                    { 44, 5, "High quality Budget Laptop with excellent features, model 4.", "images/budget_laptop_model_4.jpg", "Budget Laptop Model 4", 2717.1199999999999, 85 },
                    { 45, 5, "High quality Budget Laptop with excellent features, model 5.", "images/budget_laptop_model_5.jpg", "Budget Laptop Model 5", 2545.6900000000001, 98 },
                    { 46, 5, "High quality Budget Laptop with excellent features, model 6.", "images/budget_laptop_model_6.jpg", "Budget Laptop Model 6", 2220.4400000000001, 42 },
                    { 47, 5, "High quality Budget Laptop with excellent features, model 7.", "images/budget_laptop_model_7.jpg", "Budget Laptop Model 7", 2810.1500000000001, 48 },
                    { 48, 5, "High quality Budget Laptop with excellent features, model 8.", "images/budget_laptop_model_8.jpg", "Budget Laptop Model 8", 2488.79, 63 },
                    { 49, 5, "High quality Budget Laptop with excellent features, model 9.", "images/budget_laptop_model_9.jpg", "Budget Laptop Model 9", 2006.98, 89 },
                    { 50, 5, "High quality Budget Laptop with excellent features, model 10.", "images/budget_laptop_model_10.jpg", "Budget Laptop Model 10", 845.15999999999997, 60 },
                    { 51, 6, "High quality Workstation with excellent features, model 1.", "images/workstation_model_1.jpg", "Workstation Model 1", 2996.8400000000001, 32 },
                    { 52, 6, "High quality Workstation with excellent features, model 2.", "images/workstation_model_2.jpg", "Workstation Model 2", 2994.5, 89 },
                    { 53, 6, "High quality Workstation with excellent features, model 3.", "images/workstation_model_3.jpg", "Workstation Model 3", 1060.25, 37 },
                    { 54, 6, "High quality Workstation with excellent features, model 4.", "images/workstation_model_4.jpg", "Workstation Model 4", 873.73000000000002, 87 },
                    { 55, 6, "High quality Workstation with excellent features, model 5.", "images/workstation_model_5.jpg", "Workstation Model 5", 627.95000000000005, 13 },
                    { 56, 6, "High quality Workstation with excellent features, model 6.", "images/workstation_model_6.jpg", "Workstation Model 6", 1242.46, 68 },
                    { 57, 6, "High quality Workstation with excellent features, model 7.", "images/workstation_model_7.jpg", "Workstation Model 7", 2767.7600000000002, 78 },
                    { 58, 6, "High quality Workstation with excellent features, model 8.", "images/workstation_model_8.jpg", "Workstation Model 8", 1671.53, 77 },
                    { 59, 6, "High quality Workstation with excellent features, model 9.", "images/workstation_model_9.jpg", "Workstation Model 9", 1171.49, 64 },
                    { 60, 6, "High quality Workstation with excellent features, model 10.", "images/workstation_model_10.jpg", "Workstation Model 10", 1760.3499999999999, 87 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "125e9ac4-c829-4c85-963f-573d091ef5f7", "39f18a9b-ebfa-44ff-b5ee-b54febe86ae2" });

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "125e9ac4-c829-4c85-963f-573d091ef5f7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "39f18a9b-ebfa-44ff-b5ee-b54febe86ae2");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "CategoryId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "CategoryId",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "27e41a1b-4d16-4f23-8e11-872dea85d0df", "068553ec-c9ed-4392-8d61-d346221450d4", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CartId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "18a32fcb-b756-4a9d-a9ad-54f62234e841", 0, 0, "8ae0dee2-e25f-43b8-9ec9-b85c98a1d15b", "honganh@gmail.com", true, false, null, "HONGANH@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEDK06ROq938hA2b93XZf9vC4V3OLbpqDX0ABKWarQh2nfK+m3jrJw7RQ1ab0k/K28Q==", null, false, "c3dbbc1e-2b21-4e26-ad8c-0fcbd8aa35d8", false, "admin" });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Dành cho đường núi", "Xe đạp địa hình" });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Dành cho đường bằng", "Xe đạp đường phố" });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Dành cho trẻ em", "Xe đạp trẻ em" });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[] { "Xe cho dân chuyên nghiệp", "img1.jpg", "Xe đạp thể thao", 1200.5, 10 });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "CategoryId", "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[] { 2, "Nhẹ nhàng, dễ đi", "img2.jpg", "Xe đạp đường phố", 800.0, 5 });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[] { "Dành cho địa hình gồ ghề", "img3.jpg", "Xe đạp leo núi", 1500.75, 7 });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "CategoryId", "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[] { 2, "Tiện lợi, gấp gọn dễ dàng", "img4.jpg", "Xe đạp gấp", 950.0, 3 });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "CategoryId", "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[] { 3, "An toàn, thiết kế cho trẻ nhỏ", "img5.jpg", "Xe đạp trẻ em", 500.0, 15 });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[] { "Tốc độ cao, dành cho đường đua", "img6.jpg", "Xe đạp đua", 1800.0, 4 });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 7,
                columns: new[] { "CategoryId", "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[] { 2, "Thiết kế nhẹ nhàng, phù hợp cho nữ", "img7.jpg", "Xe đạp nữ", 750.0, 6 });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "CategoryId", "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[] { 3, "Xe đạp có hỗ trợ điện dành cho trẻ em", "img8.jpg", "Xe đạp điện trẻ em", 1200.0, 8 });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 9,
                columns: new[] { "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[] { "Xe phù hợp cho du lịch đường dài", "img9.jpg", "Xe đạp touring", 2000.0, 2 });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 10,
                columns: new[] { "CategoryId", "Description", "LinkImagesPath", "Name", "Price", "stock" },
                values: new object[] { 2, "Xe đạp nhỏ gọn, phù hợp cho học sinh", "img10.jpg", "Xe đạp mini", 650.0, 9 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "27e41a1b-4d16-4f23-8e11-872dea85d0df", "18a32fcb-b756-4a9d-a9ad-54f62234e841" });
        }
    }
}
