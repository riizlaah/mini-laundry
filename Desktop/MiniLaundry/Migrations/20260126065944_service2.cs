using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace testWPF.Migrations
{
    /// <inheritdoc />
    public partial class service2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeaderTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompleteEstDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeaderTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeaderTransactions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeaderTransactions_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetailTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    HeaderTransactionId = table.Column<int>(type: "int", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    TotalUnit = table.Column<float>(type: "real", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailTransactions_HeaderTransactions_HeaderTransactionId",
                        column: x => x.HeaderTransactionId,
                        principalTable: "HeaderTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailTransactions_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailTransactions_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "Name", "PhoneNum" },
                values: new object[,]
                {
                    { 1, "Batang", "Alok", "+6256488976532" },
                    { 2, "Pekalongan", "Bowo", "+6287655387653" }
                });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "Description", "Duration", "Name", "Price" },
                values: new object[] { 1, "Paket untuk hari raya", 14, "Paket Hari Raya", 100000 });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "CategoryId", "EstimationDuration", "Name", "Price", "UnitId" },
                values: new object[,]
                {
                    { 2, 1, 6, "Cuci Setrika", 30000, 1 },
                    { 3, 1, 1, "Cuci Kilat", 30000, 1 },
                    { 4, 1, 1, "Setrika Kilat", 20000, 1 },
                    { 5, 2, 2, "Cuci Korden", 10000, 2 }
                });

            migrationBuilder.InsertData(
                table: "DetailPackages",
                columns: new[] { "Id", "PackageId", "ServiceId", "TotalUnitService" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 1, 2, 2 },
                    { 3, 1, 5, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetailTransactions_HeaderTransactionId",
                table: "DetailTransactions",
                column: "HeaderTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailTransactions_PackageId",
                table: "DetailTransactions",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailTransactions_ServiceId",
                table: "DetailTransactions",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_HeaderTransactions_CustomerId",
                table: "HeaderTransactions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_HeaderTransactions_EmployeeId",
                table: "HeaderTransactions",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailTransactions");

            migrationBuilder.DropTable(
                name: "HeaderTransactions");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DetailPackages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DetailPackages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DetailPackages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
