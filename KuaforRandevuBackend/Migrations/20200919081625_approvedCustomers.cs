using Microsoft.EntityFrameworkCore.Migrations;

namespace KuaforRandevuBackend.Migrations
{
    public partial class approvedCustomers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApprovedCustomers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    Hour = table.Column<string>(nullable: true),
                    Transactions = table.Column<string>(nullable: true),
                    Price = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovedCustomers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedCustomers_UserId",
                table: "ApprovedCustomers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovedCustomers");
        }
    }
}
