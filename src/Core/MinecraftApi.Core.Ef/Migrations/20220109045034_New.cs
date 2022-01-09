using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinecraftApi.Ef.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class New : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Required",
                table: "Arguments",
                type: "bit",
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
