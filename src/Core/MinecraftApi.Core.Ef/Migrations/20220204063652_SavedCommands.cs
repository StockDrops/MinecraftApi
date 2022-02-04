using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace MinecraftApi.Ef.Migrations
{
    public partial class SavedCommands : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder)

        {
            migrationBuilder.AddColumn<long>(
                name: "RanCommandId",
                table: "RanArguments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CommandId",
                table: "BaseRanCommands",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "BaseRanCommands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RanArguments_RanCommandId",
                table: "RanArguments",
                column: "RanCommandId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseRanCommands_CommandId",
                table: "BaseRanCommands",
                column: "CommandId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseRanCommands_Commands_CommandId",
                table: "BaseRanCommands",
                column: "CommandId",
                principalTable: "Commands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RanArguments_BaseRanCommands_RanCommandId",
                table: "RanArguments",
                column: "RanCommandId",
                principalTable: "BaseRanCommands",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseRanCommands_Commands_CommandId",
                table: "BaseRanCommands");

            migrationBuilder.DropForeignKey(
                name: "FK_RanArguments_BaseRanCommands_RanCommandId",
                table: "RanArguments");

            migrationBuilder.DropIndex(
                name: "IX_RanArguments_RanCommandId",
                table: "RanArguments");

            migrationBuilder.DropIndex(
                name: "IX_BaseRanCommands_CommandId",
                table: "BaseRanCommands");

            migrationBuilder.DropColumn(
                name: "RanCommandId",
                table: "RanArguments");

            migrationBuilder.DropColumn(
                name: "CommandId",
                table: "BaseRanCommands");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "BaseRanCommands");
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member