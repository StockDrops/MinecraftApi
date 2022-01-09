using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinecraftApi.Ef.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class NewArgument : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Arguments");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Plugins",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PluginId",
                table: "Commands",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CommandId",
                table: "Arguments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Plugins_Name",
                table: "Plugins",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Commands_PluginId",
                table: "Commands",
                column: "PluginId");

            migrationBuilder.CreateIndex(
                name: "IX_Arguments_CommandId",
                table: "Arguments",
                column: "CommandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Arguments_Commands_CommandId",
                table: "Arguments",
                column: "CommandId",
                principalTable: "Commands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Commands_Plugins_PluginId",
                table: "Commands",
                column: "PluginId",
                principalTable: "Plugins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
/// <inheritdoc/>

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arguments_Commands_CommandId",
                table: "Arguments");

            migrationBuilder.DropForeignKey(
                name: "FK_Commands_Plugins_PluginId",
                table: "Commands");

            migrationBuilder.DropIndex(
                name: "IX_Plugins_Name",
                table: "Plugins");

            migrationBuilder.DropIndex(
                name: "IX_Commands_PluginId",
                table: "Commands");

            migrationBuilder.DropIndex(
                name: "IX_Arguments_CommandId",
                table: "Arguments");

            migrationBuilder.DropColumn(
                name: "PluginId",
                table: "Commands");

            migrationBuilder.DropColumn(
                name: "CommandId",
                table: "Arguments");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Plugins",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "Arguments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member