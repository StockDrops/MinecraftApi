using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinecraftApi.Ef.Migrations.MySql
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class New : Migration
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Required",
                table: "Arguments",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
/// <inheritdoc/>

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Required",
                table: "Arguments");
        }
    }
}
