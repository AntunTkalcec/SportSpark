using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportSparkInfrastructureLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditRadiusProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DesiredRadius",
                table: "User",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DesiredRadius",
                table: "User",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
