using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Data.Migrations
{
    /// <inheritdoc />
    public partial class portifolio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3d918b7-8003-45ec-ba31-ac105e1d786c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7ec3925-fc75-4a83-96e5-e8a2015f9cc7");

            migrationBuilder.CreateTable(
                name: "Portifolios",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portifolios", x => new { x.UserId, x.StockId });
                    table.ForeignKey(
                        name: "FK_Portifolios_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Portifolios_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d229b7eb-02ed-40f2-92ee-b8070b51712f", null, "admin", "ADMIN" },
                    { "d3e34bd4-389e-4486-bb88-d9a7643a2f08", null, "user", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Portifolios_StockId",
                table: "Portifolios",
                column: "StockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Portifolios");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d229b7eb-02ed-40f2-92ee-b8070b51712f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3e34bd4-389e-4486-bb88-d9a7643a2f08");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a3d918b7-8003-45ec-ba31-ac105e1d786c", null, "user", "USER" },
                    { "d7ec3925-fc75-4a83-96e5-e8a2015f9cc7", null, "admin", "ADMIN" }
                });
        }
    }
}
