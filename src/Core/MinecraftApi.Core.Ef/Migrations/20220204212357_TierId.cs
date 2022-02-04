using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace MinecraftApi.Ef.Migrations
{
    public partial class TierId : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TierUrl",
                table: "Roles");

            migrationBuilder.AddColumn<long>(
                name: "TierId",
                table: "Roles",
                type: "bigint",
                nullable: true);
        }


        protected override void Down(MigrationBuilder migrationBuilder) 
        { 

            migrationBuilder.DropColumn(
                name: "TierId",
                table: "Roles");

            migrationBuilder.AddColumn<string>(
                name: "TierUrl",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
