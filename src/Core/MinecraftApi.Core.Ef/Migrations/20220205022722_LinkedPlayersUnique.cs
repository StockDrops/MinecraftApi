using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinecraftApi.Ef.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class LinkedPlayersUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LinkedPlayers_PlayerId",
                table: "LinkedPlayers");

            migrationBuilder.CreateIndex(
                name: "IX_LinkedPlayers_PlayerId",
                table: "LinkedPlayers",
                column: "PlayerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LinkedPlayers_PlayerId",
                table: "LinkedPlayers");

            migrationBuilder.CreateIndex(
                name: "IX_LinkedPlayers_PlayerId",
                table: "LinkedPlayers",
                column: "PlayerId");
        }
    }
}
