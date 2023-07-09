using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketShop.Repository.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketInOrder_Tickets_ProductId",
                table: "TicketInOrder");

            migrationBuilder.DropIndex(
                name: "IX_TicketInOrder_ProductId",
                table: "TicketInOrder");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "TicketInOrder");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketId",
                table: "TicketInOrder",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TicketInOrder_TicketId",
                table: "TicketInOrder",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketInOrder_Tickets_TicketId",
                table: "TicketInOrder",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketInOrder_Tickets_TicketId",
                table: "TicketInOrder");

            migrationBuilder.DropIndex(
                name: "IX_TicketInOrder_TicketId",
                table: "TicketInOrder");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "TicketInOrder");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "TicketInOrder",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TicketInOrder_ProductId",
                table: "TicketInOrder",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketInOrder_Tickets_ProductId",
                table: "TicketInOrder",
                column: "ProductId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
