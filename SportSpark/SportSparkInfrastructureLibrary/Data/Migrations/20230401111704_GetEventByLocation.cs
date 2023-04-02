using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportSparkInfrastructureLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class GetEventByLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[GetEventsByLocation] 
                    @latitude float, 
                    @longitude float, 
                    @radius float 
                AS
                BEGIN
                    SET NOCOUNT ON;

                    SELECT * 
                    FROM dbo.[Event]
                    WHERE (6371 * 2 * atn2(SQRT(POWER(SIN((RADIANS(lat) - RADIANS(@latitude)) / 2), 2) + COS(RADIANS(lat)) * COS(RADIANS(@latitude)) * POWER(SIN((RADIANS(long) - RADIANS(@longitude)) / 2), 2)), SQRT(1 - POWER(SIN((RADIANS(lat) - RADIANS(@latitude)) / 2), 2) + COS(RADIANS(lat)) * COS(RADIANS(@latitude)) * POWER(SIN((RADIANS(long) - RADIANS(@longitude)) / 2), 2)))) <= @radius;
                END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[GetEventsByLocation]");
        }
    }
}
