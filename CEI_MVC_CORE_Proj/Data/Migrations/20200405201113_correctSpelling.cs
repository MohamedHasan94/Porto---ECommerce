using Microsoft.EntityFrameworkCore.Migrations;

namespace CEI_MVC_CORE_Proj.Data.Migrations
{
    public partial class correctSpelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Product_FK_ProductId",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropColumn(
                name: "FK_PropductId",
                table: "Transaction");

            migrationBuilder.AlterColumn<int>(
                name: "FK_ProductId",
                table: "Transaction",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "OfferPrice",
                table: "Product",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Product_FK_ProductId",
                table: "Transaction",
                column: "FK_ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Product_FK_ProductId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "OfferPrice",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "FK_ProductId",
                table: "Transaction",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "FK_PropductId",
                table: "Transaction",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Product_FK_ProductId",
                table: "Transaction",
                column: "FK_ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
