using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foundation.Business.Migrations
{
    /// <inheritdoc />
    public partial class LatestMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LatestMessageAt",
                schema: "auth",
                table: "conversation",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LatestMessageId",
                schema: "auth",
                table: "conversation",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LatestMessageAt",
                schema: "auth",
                table: "conversation");

            migrationBuilder.DropColumn(
                name: "LatestMessageId",
                schema: "auth",
                table: "conversation");
        }
    }
}
