using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinecraftApi.Ef.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class SavedArgs : Migration

    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Prefix",
                table: "Commands",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "BaseRanCommands",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RanTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RawCommand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseRanCommands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RanArguments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SavedArgumentId = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RanArguments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RanArguments_Arguments_SavedArgumentId",
                        column: x => x.SavedArgumentId,
                        principalTable: "Arguments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            

            migrationBuilder.CreateIndex(
                name: "IX_Commands_Prefix",
                table: "Commands",
                column: "Prefix",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RanArguments_SavedArgumentId",
                table: "RanArguments",
                column: "SavedArgumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseRanCommands");

          

            migrationBuilder.DropTable(
                name: "RanArguments");

            migrationBuilder.DropIndex(
                name: "IX_Commands_Prefix",
                table: "Commands");

            migrationBuilder.AlterColumn<string>(
                name: "Prefix",
                table: "Commands",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member