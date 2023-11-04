using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthControl.Migrations
{
    public partial class adding_operations_days : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "operationDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    StartBalance = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    EndBalance = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    QuantOperations = table.Column<int>(type: "int", nullable: false),
                    robotsAccuracy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gainIncoming = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operationDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_operationDays_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_operationDays_UserId",
                table: "operationDays",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "operationDays");
        }
    }
}
