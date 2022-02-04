using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace MinecraftApi.Ef.Migrations
{
    public partial class SavedItems : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DefaultValue",
                table: "Arguments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LinkedPlayerRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckRoleBy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LinkedPlayerId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkedPlayerRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkedPlayerRoles_LinkedPlayers_LinkedPlayerId",
                        column: x => x.LinkedPlayerId,
                        principalTable: "LinkedPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkedPlayerRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkedPlayerRoles_LinkedPlayerId",
                table: "LinkedPlayerRoles",
                column: "LinkedPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkedPlayerRoles_RoleId",
                table: "LinkedPlayerRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkedPlayerRoles");

            migrationBuilder.DropColumn(
                name: "DefaultValue",
                table: "Arguments");
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member