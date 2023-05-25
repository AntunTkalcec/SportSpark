using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportSparkInfrastructureLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class EventVisibilityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ValidUserIds",
                table: "Event",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidUserIds",
                table: "Event");
        }
    }
}
