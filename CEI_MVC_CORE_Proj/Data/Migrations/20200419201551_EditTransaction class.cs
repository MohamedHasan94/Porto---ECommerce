using Microsoft.EntityFrameworkCore.Migrations;

namespace CEI_MVC_CORE_Proj.Data.Migrations
{
    public partial class EditTransactionclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CheckOutId",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Transaction",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Transaction",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckOutId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transaction");
        }
    }
}
