using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportSparkInfrastructureLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRadiusProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DesiredRadius",
                table: "User",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DesiredRadius",
                table: "User");
        }
    }
}
