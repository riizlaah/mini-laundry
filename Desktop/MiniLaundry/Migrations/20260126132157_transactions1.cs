using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace testWPF.Migrations
{
    /// <inheritdoc />
    public partial class transactions1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailTransactions_Packages_PackageId",
                table: "DetailTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailTransactions_Services_ServiceId",
                table: "DetailTransactions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompleteEstDate",
                table: "HeaderTransactions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "DetailTransactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PackageId",
                table: "DetailTransactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailTransactions_Packages_PackageId",
                table: "DetailTransactions",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailTransactions_Services_ServiceId",
                table: "DetailTransactions",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailTransactions_Packages_PackageId",
                table: "DetailTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailTransactions_Services_ServiceId",
                table: "DetailTransactions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompleteEstDate",
                table: "HeaderTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "DetailTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PackageId",
                table: "DetailTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailTransactions_Packages_PackageId",
                table: "DetailTransactions",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailTransactions_Services_ServiceId",
                table: "DetailTransactions",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
