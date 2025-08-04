using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgenciaDeTours.Migrations
{
    /// <inheritdoc />
    public partial class ColumnaEstadoEsCalculada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EstadoTour",
                table: "Tours",
                type: "int",
                nullable: false,
                computedColumnSql: "CASE WHEN [Fecha] > CAST(GETDATE() AS date) THEN 1 ELSE 2 END",
                stored: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EstadoTour",
                table: "Tours",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComputedColumnSql: "CASE WHEN [Fecha] > CAST(GETDATE() AS date) THEN 1 ELSE 2 END");
        }
    }
}
