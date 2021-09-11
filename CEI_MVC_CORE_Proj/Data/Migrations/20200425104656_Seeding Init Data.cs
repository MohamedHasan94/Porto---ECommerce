using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CEI_MVC_CORE_Proj.Data.Migrations
{
    public partial class SeedingInitData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "e7ac0740-9d1c-4cc0-88db-77c13a0ec43b", "Admin", "ADMIN" },
                    { "2", "c4666af6-6a3c-45d6-86ad-6e1c54554374", "Vendor", "VENDOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureLink", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "e19cf102-d4ce-401c-94a5-76a72318641e", 0, "79899a15-a562-43fc-9f03-0f49371d7a4e", "admin@domain.com", false, "admin", "admin", false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEJUqvb1VVyeG+4hEdfgEpUxkczbB8BPziNQCQo6b/7OJ4paddrsFYGv5tomQvv0Gkg==", null, false, "/images/UserImages/admin.jpg", "SecurityStamp", false, "admin" },
                    { "0bcd78db-8663-46fd-874c-3b4fa91e63e8", 0, "01e6384d-5411-4489-a0fd-cb78faae47dd", "vendor@domain.com", false, "vendor", "vendor", false, null, null, "VENDOR", "AQAAAAEAACcQAAAAEKbnfiAqDVbt14hDRvW90iEH+EXjwoDSopqZouIoNTJsc7Q918y3TQR1MVhfL0CFiA==", null, false, "/images/UserImages/vendor.jpg", "SecurityStamp", false, "vendor" },
                    { "7fb8769f-1053-4153-b91f-7e1599a2f438", 0, "265f8a7a-4dda-4286-be60-19847172c55e", "customer1@domain.com", false, "customer", "customer", false, null, null, "CUSTOMER1", "AQAAAAEAACcQAAAAEMYYhGjlYQhtIWdyDtFgsuChldGEcLI23D9nFQeqsaxsp03RS5xDamQflyuPte+s8g==", null, false, "/images/UserImages/customer1.jpg", "SecurityStamp", false, "customer1" },
                    { "4e668f4c-31b6-487e-b343-83a7d3984cd5", 0, "43e4f8e9-cb08-4231-b591-63f768989814", "customer2@domain.com", false, "customer", "customer", false, null, null, "CUSTOMER2", "AQAAAAEAACcQAAAAENsKOmikf4wmpE0V0wagGCd8EqZmm1Z5OjE3dc6nGnqb/hhgAKYgVdOBY/r6fuXUFA==", null, false, "/images/UserImages/customer2.jpg", "SecurityStamp", false, "customer2" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 15, "Others" },
                    { 14, "Toys & Games" },
                    { 13, "Tools & Home Improvement" },
                    { 12, "Sports & Outdoors" },
                    { 11, "Movies & Television" },
                    { 10, "Military Accessories" },
                    { 9, "Home & Kitchen" },
                    { 6, "Women's Fashion" },
                    { 7, "Men's Fashion" },
                    { 5, "Eletronics" },
                    { 4, "Books" },
                    { 3, "Baby" },
                    { 2, "Automotive" },
                    { 1, "Arts & Crafts" },
                    { 8, "Health & Household" }
                });

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "AllPurpose" },
                    { 2, "TagExample" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "e19cf102-d4ce-401c-94a5-76a72318641e", "1" },
                    { "e19cf102-d4ce-401c-94a5-76a72318641e", "2" },
                    { "0bcd78db-8663-46fd-874c-3b4fa91e63e8", "2" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "FK_CategoryId", "FK_VendorId", "Name", "OfferPrice", "Price", "Quantity" },
                values: new object[,]
                {
                    { 12, "Placeholder Product for shwoing", 8, "0bcd78db-8663-46fd-874c-3b4fa91e63e8", "Package Fax", 311.0, 311.0, 45L },
                    { 11, "Placeholder Product for shwoing", 8, "0bcd78db-8663-46fd-874c-3b4fa91e63e8", "BrainWire", 100.0, 310.0, 95L },
                    { 4, "Placeholder Product for shwoing", 8, "0bcd78db-8663-46fd-874c-3b4fa91e63e8", "Norse Nurse", 303.0, 303.0, 91L },
                    { 8, "Placeholder Product for shwoing", 7, "0bcd78db-8663-46fd-874c-3b4fa91e63e8", "Candy Floss", 100.0, 307.0, 59L },
                    { 6, "Placeholder Product for shwoing", 5, "0bcd78db-8663-46fd-874c-3b4fa91e63e8", "Media Pet", 100.0, 305.0, 43L },
                    { 9, "Placeholder Product for shwoing", 4, "0bcd78db-8663-46fd-874c-3b4fa91e63e8", "Transcoder", 100.0, 308.0, 97L },
                    { 7, "Placeholder Product for shwoing", 3, "0bcd78db-8663-46fd-874c-3b4fa91e63e8", "EdWeb", 100.0, 306.0, 18L },
                    { 10, "Placeholder Product for shwoing", 9, "0bcd78db-8663-46fd-874c-3b4fa91e63e8", "Pubisphere", 100.0, 309.0, 77L },
                    { 5, "Placeholder Product for shwoing", 1, "0bcd78db-8663-46fd-874c-3b4fa91e63e8", "Mineral Massage", 100.0, 304.0, 15L },
                    { 1, "Placeholder Product for shwoing", 1, "0bcd78db-8663-46fd-874c-3b4fa91e63e8", "MaintainIT", 100.0, 300.0, 71L },
                    { 3, "Placeholder Product for shwoing", 4, "0bcd78db-8663-46fd-874c-3b4fa91e63e8", "Mixed Feelings", 302.0, 302.0, 16L },
                    { 2, "Placeholder Product for shwoing", 12, "0bcd78db-8663-46fd-874c-3b4fa91e63e8", "KinderZoo", 301.0, 301.0, 89L }
                });

            migrationBuilder.InsertData(
                table: "RequestToAdmin",
                columns: new[] { "Id", "Data", "FK_UserId", "Status", "Type" },
                values: new object[,]
                {
                    { 3, "This user is requesting vendor role", "4e668f4c-31b6-487e-b343-83a7d3984cd5", 0, 1 },
                    { 2, "This user is requesting vendor role", "7fb8769f-1053-4153-b91f-7e1599a2f438", 0, 1 },
                    { 1, "Summer Sports Wear", "0bcd78db-8663-46fd-874c-3b4fa91e63e8", 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "ProductImage",
                columns: new[] { "FK_ProductId", "Path" },
                values: new object[,]
                {
                    { 9, "/images/ProductImages/8_1.jpg" },
                    { 11, "/images/ProductImages/10_1.jpg" },
                    { 11, "/images/ProductImages/10_0.jpg" },
                    { 4, "/images/ProductImages/3_1.jpg" },
                    { 4, "/images/ProductImages/3_0.jpg" },
                    { 8, "/images/ProductImages/7_1.jpg" },
                    { 8, "/images/ProductImages/7_0.jpg" },
                    { 10, "/images/ProductImages/9_0.jpg" },
                    { 6, "/images/ProductImages/5_1.jpg" },
                    { 6, "/images/ProductImages/5_0.jpg" },
                    { 10, "/images/ProductImages/9_1.jpg" },
                    { 12, "/images/ProductImages/11_0.jpg" },
                    { 9, "/images/ProductImages/8_0.jpg" },
                    { 3, "/images/ProductImages/2_1.jpg" },
                    { 3, "/images/ProductImages/2_0.jpg" },
                    { 12, "/images/ProductImages/11_1.jpg" },
                    { 7, "/images/ProductImages/6_1.jpg" },
                    { 5, "/images/ProductImages/4_0.jpg" },
                    { 7, "/images/ProductImages/6_0.jpg" },
                    { 5, "/images/ProductImages/4_1.jpg" },
                    { 1, "/images/ProductImages/0_0.jpg" },
                    { 2, "/images/ProductImages/1_1.jpg" },
                    { 1, "/images/ProductImages/0_1.jpg" },
                    { 2, "/images/ProductImages/1_0.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Product_PaymentMethod",
                columns: new[] { "FK_ProductId", "Method" },
                values: new object[,]
                {
                    { 10, 4 },
                    { 4, 1 },
                    { 4, 3 },
                    { 10, 1 },
                    { 11, 4 },
                    { 1, 3 },
                    { 12, 0 },
                    { 12, 4 },
                    { 11, 0 },
                    { 2, 1 },
                    { 8, 3 },
                    { 8, 0 },
                    { 5, 4 },
                    { 6, 3 },
                    { 6, 0 },
                    { 2, 4 },
                    { 7, 1 },
                    { 9, 3 },
                    { 9, 0 },
                    { 7, 4 },
                    { 3, 3 },
                    { 3, 0 },
                    { 5, 0 },
                    { 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "Product_Tag",
                columns: new[] { "FK_ProductId", "FK_TagId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 10, 1 },
                    { 12, 1 },
                    { 6, 1 },
                    { 4, 1 },
                    { 8, 1 },
                    { 9, 1 },
                    { 3, 1 },
                    { 7, 1 },
                    { 5, 1 },
                    { 1, 1 },
                    { 11, 1 }
                });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "Id", "CheckOutId", "DateTime", "FK_CustomerId", "FK_ProductId", "PaymentMethod", "Quantity", "Status" },
                values: new object[,]
                {
                    { 2, "1587811615922_7fb8769f", new DateTime(2020, 4, 25, 12, 46, 55, 886, DateTimeKind.Local), "7fb8769f-1053-4153-b91f-7e1599a2f438", 2, 3, 1, 0 },
                    { 3, "1587811615922_4e668f4c", new DateTime(2020, 4, 25, 12, 46, 55, 886, DateTimeKind.Local), "4e668f4c-31b6-487e-b343-83a7d3984cd5", 5, 2, 3, 0 },
                    { 1, "1587811615922_7fb8769f", new DateTime(2020, 4, 25, 12, 46, 55, 886, DateTimeKind.Local), "7fb8769f-1053-4153-b91f-7e1599a2f438", 1, 4, 3, 0 },
                    { 4, "1587811615922_4e668f4c", new DateTime(2020, 4, 25, 12, 46, 55, 886, DateTimeKind.Local), "4e668f4c-31b6-487e-b343-83a7d3984cd5", 2, 5, 4, 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "0bcd78db-8663-46fd-874c-3b4fa91e63e8", "2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "e19cf102-d4ce-401c-94a5-76a72318641e", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "e19cf102-d4ce-401c-94a5-76a72318641e", "2" });

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 1, "/images/ProductImages/0_0.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 1, "/images/ProductImages/0_1.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 2, "/images/ProductImages/1_0.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 2, "/images/ProductImages/1_1.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 3, "/images/ProductImages/2_0.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 3, "/images/ProductImages/2_1.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 4, "/images/ProductImages/3_0.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 4, "/images/ProductImages/3_1.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 5, "/images/ProductImages/4_0.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 5, "/images/ProductImages/4_1.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 6, "/images/ProductImages/5_0.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 6, "/images/ProductImages/5_1.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 7, "/images/ProductImages/6_0.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 7, "/images/ProductImages/6_1.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 8, "/images/ProductImages/7_0.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 8, "/images/ProductImages/7_1.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 9, "/images/ProductImages/8_0.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 9, "/images/ProductImages/8_1.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 10, "/images/ProductImages/9_0.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 10, "/images/ProductImages/9_1.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 11, "/images/ProductImages/10_0.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 11, "/images/ProductImages/10_1.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 12, "/images/ProductImages/11_0.jpg" });

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumns: new[] { "FK_ProductId", "Path" },
                keyValues: new object[] { 12, "/images/ProductImages/11_1.jpg" });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 1, 0 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 3, 0 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 5, 0 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 6, 0 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 8, 0 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 8, 3 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 9, 0 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 9, 3 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 10, 4 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 11, 0 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 11, 4 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 12, 0 });

            migrationBuilder.DeleteData(
                table: "Product_PaymentMethod",
                keyColumns: new[] { "FK_ProductId", "Method" },
                keyValues: new object[] { 12, 4 });

            migrationBuilder.DeleteData(
                table: "Product_Tag",
                keyColumns: new[] { "FK_ProductId", "FK_TagId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Product_Tag",
                keyColumns: new[] { "FK_ProductId", "FK_TagId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Product_Tag",
                keyColumns: new[] { "FK_ProductId", "FK_TagId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "Product_Tag",
                keyColumns: new[] { "FK_ProductId", "FK_TagId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "Product_Tag",
                keyColumns: new[] { "FK_ProductId", "FK_TagId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "Product_Tag",
                keyColumns: new[] { "FK_ProductId", "FK_TagId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "Product_Tag",
                keyColumns: new[] { "FK_ProductId", "FK_TagId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "Product_Tag",
                keyColumns: new[] { "FK_ProductId", "FK_TagId" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                table: "Product_Tag",
                keyColumns: new[] { "FK_ProductId", "FK_TagId" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                table: "Product_Tag",
                keyColumns: new[] { "FK_ProductId", "FK_TagId" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(
                table: "Product_Tag",
                keyColumns: new[] { "FK_ProductId", "FK_TagId" },
                keyValues: new object[] { 11, 1 });

            migrationBuilder.DeleteData(
                table: "Product_Tag",
                keyColumns: new[] { "FK_ProductId", "FK_TagId" },
                keyValues: new object[] { 12, 1 });

            migrationBuilder.DeleteData(
                table: "RequestToAdmin",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RequestToAdmin",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RequestToAdmin",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "1", "e7ac0740-9d1c-4cc0-88db-77c13a0ec43b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "2", "c4666af6-6a3c-45d6-86ad-6e1c54554374" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "4e668f4c-31b6-487e-b343-83a7d3984cd5", "43e4f8e9-cb08-4231-b591-63f768989814" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "7fb8769f-1053-4153-b91f-7e1599a2f438", "265f8a7a-4dda-4286-be60-19847172c55e" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "e19cf102-d4ce-401c-94a5-76a72318641e", "79899a15-a562-43fc-9f03-0f49371d7a4e" });

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "0bcd78db-8663-46fd-874c-3b4fa91e63e8", "01e6384d-5411-4489-a0fd-cb78faae47dd" });

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
