using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CEI_MVC_CORE_Proj.Data.Migrations
{
    public partial class Entities01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestToAdmin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(nullable: false),
                    Data = table.Column<string>(maxLength: 100, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    FK_UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestToAdmin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestToAdmin_AspNetUsers_FK_UserId",
                        column: x => x.FK_UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Quantity = table.Column<long>(nullable: false),
                    FK_VendorId = table.Column<string>(nullable: true),
                    FK_CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category_FK_CategoryId",
                        column: x => x.FK_CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_AspNetUsers_FK_VendorId",
                        column: x => x.FK_VendorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product_PaymentMethod",
                columns: table => new
                {
                    FK_ProductId = table.Column<int>(nullable: false),
                    Method = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_PaymentMethod", x => x.FK_ProductId);
                    table.ForeignKey(
                        name: "FK_Product_PaymentMethod_Product_FK_ProductId",
                        column: x => x.FK_ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_Tag",
                columns: table => new
                {
                    FK_ProductId = table.Column<int>(nullable: false),
                    FK_TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Tag", x => new { x.FK_ProductId, x.FK_TagId });
                    table.ForeignKey(
                        name: "FK_Product_Tag_Product_FK_ProductId",
                        column: x => x.FK_ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Tag_Tag_FK_TagId",
                        column: x => x.FK_TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    FK_ProductId = table.Column<int>(nullable: false),
                    Path = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.FK_ProductId);
                    table.ForeignKey(
                        name: "FK_ProductImage_Product_FK_ProductId",
                        column: x => x.FK_ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    FK_ProductId = table.Column<int>(nullable: false),
                    Discount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.FK_ProductId);
                    table.ForeignKey(
                        name: "FK_Sales_Product_FK_ProductId",
                        column: x => x.FK_ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PaymentMethod = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    FK_PropductId = table.Column<int>(nullable: false),
                    FK_CustomerId = table.Column<string>(nullable: true),
                    FK_ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_AspNetUsers_FK_CustomerId",
                        column: x => x.FK_CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Product_FK_ProductId",
                        column: x => x.FK_ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_FK_CategoryId",
                table: "Product",
                column: "FK_CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_FK_VendorId",
                table: "Product",
                column: "FK_VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Tag_FK_TagId",
                table: "Product_Tag",
                column: "FK_TagId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestToAdmin_FK_UserId",
                table: "RequestToAdmin",
                column: "FK_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_FK_CustomerId",
                table: "Transaction",
                column: "FK_CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_FK_ProductId",
                table: "Transaction",
                column: "FK_ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product_PaymentMethod");

            migrationBuilder.DropTable(
                name: "Product_Tag");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropTable(
                name: "RequestToAdmin");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
