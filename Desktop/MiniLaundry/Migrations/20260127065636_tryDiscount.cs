using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniLaundry.Migrations
{
    /// <inheritdoc />
    public partial class tryDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiscountToken",
                table: "HeaderTransactions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.Token);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeaderTransactions_DiscountToken",
                table: "HeaderTransactions",
                column: "DiscountToken");

            migrationBuilder.AddForeignKey(
                name: "FK_HeaderTransactions_Discount_DiscountToken",
                table: "HeaderTransactions",
                column: "DiscountToken",
                principalTable: "Discount",
                principalColumn: "Token");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeaderTransactions_Discount_DiscountToken",
                table: "HeaderTransactions");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropIndex(
                name: "IX_HeaderTransactions_DiscountToken",
                table: "HeaderTransactions");

            migrationBuilder.DropColumn(
                name: "DiscountToken",
                table: "HeaderTransactions");
        }
    }
}
