using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace MinecraftApi.Ef.Migrations
{
    public partial class TokenPerPlayerUnique : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tokens_LinkedPlayerId",
                table: "Tokens");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_LinkedPlayerId",
                table: "Tokens",
                column: "LinkedPlayerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tokens_LinkedPlayerId",
                table: "Tokens");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_LinkedPlayerId",
                table: "Tokens",
                column: "LinkedPlayerId");
        }
    }
}
