using Microsoft.EntityFrameworkCore.Migrations;

namespace KuaforRandevuBackend.Migrations
{
    public partial class paymentChoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentChoice",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentChoice",
                table: "ApprovedCustomers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentChoice",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PaymentChoice",
                table: "ApprovedCustomers");
        }
    }
}
