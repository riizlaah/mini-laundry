using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace testWPF.Migrations
{
    /// <inheritdoc />
    public partial class transacs3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "Name", "PhoneNum" },
                values: new object[] { 3, "Tegal", "Budiono", "+6280766498076" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "DateOfBirth", "Email", "JobId", "Name", "Password", "PhoneNum", "Salary" },
                values: new object[,]
                {
                    { 2, "Mars", new DateTime(2003, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "hartono09@gmail.com", 2, "Hartono", "p4s?", "+62876533876532", 2500000m },
                    { 3, "Ds. Klewer, Kec. Tulis", new DateTime(2002, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ubed25@gmail.com", 3, "Ubed", "p4s?", "+6286544987320", 2500000m }
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Kurir" });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "Description", "Duration", "Name", "Price" },
                values: new object[] { 2, "Paket yang mendekati kecepatan cahaya", 6, "Paket Kilat", 150000 });

            migrationBuilder.InsertData(
                table: "DetailPackages",
                columns: new[] { "Id", "PackageId", "ServiceId", "TotalUnitService" },
                values: new object[,]
                {
                    { 4, 2, 3, 3 },
                    { 5, 2, 4, 3 }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "DateOfBirth", "Email", "JobId", "Name", "Password", "PhoneNum", "Salary" },
                values: new object[] { 4, "Ds. Sengon, Kec. Subah", new DateTime(2003, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "komar@gmail.com", 4, "Komar", "p4s?", "+6289267530098", 2500000m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DetailPackages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DetailPackages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
