using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09b3e023-40b3-4327-93cf-87d93171d3a1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5248553-ea3d-4e28-8312-5ed79b076af7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "872c7c6e-ca56-4fa4-8e92-0d26e467e71d", null, "admin", "ADMIN" },
                    { "d486088d-f92b-4520-95c4-5a3fc78fc09e", null, "user", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "872c7c6e-ca56-4fa4-8e92-0d26e467e71d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d486088d-f92b-4520-95c4-5a3fc78fc09e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "09b3e023-40b3-4327-93cf-87d93171d3a1", null, "admin", "ADMIN" },
                    { "d5248553-ea3d-4e28-8312-5ed79b076af7", null, "user", "USER" }
                });
        }
    }
}
