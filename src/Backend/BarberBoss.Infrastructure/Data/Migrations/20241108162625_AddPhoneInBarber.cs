﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberBoss.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneInBarber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Barbers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Barbers");
        }
    }
}
