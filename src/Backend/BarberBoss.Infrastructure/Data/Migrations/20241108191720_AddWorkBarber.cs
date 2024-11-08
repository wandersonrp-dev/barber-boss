using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberBoss.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkBarber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Barbers_BarberShops_BarberShopId",
                table: "Barbers");

            migrationBuilder.DropIndex(
                name: "IX_Barbers_BarberShopId",
                table: "Barbers");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "Barbers");

            migrationBuilder.CreateTable(
                name: "WorkBarbers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BarberId = table.Column<Guid>(type: "uuid", nullable: false),
                    BarberShopId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkBarbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkBarbers_BarberShops_BarberShopId",
                        column: x => x.BarberShopId,
                        principalTable: "BarberShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkBarbers_Barbers_BarberId",
                        column: x => x.BarberId,
                        principalTable: "Barbers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkBarbers_BarberId",
                table: "WorkBarbers",
                column: "BarberId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkBarbers_BarberShopId",
                table: "WorkBarbers",
                column: "BarberShopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkBarbers");

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "Barbers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Barbers_BarberShopId",
                table: "Barbers",
                column: "BarberShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Barbers_BarberShops_BarberShopId",
                table: "Barbers",
                column: "BarberShopId",
                principalTable: "BarberShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
