using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniLaundry.Migrations
{
    /// <inheritdoc />
    public partial class tryDiscount2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Value",
                table: "Discount",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Discount");
        }
    }
}
