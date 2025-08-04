using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgenciaDeTours.Migrations
{
    /// <inheritdoc />
    public partial class AgregaColumnasDuracionDestino : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DuracionDias",
                table: "Destinos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DuracionHoras",
                table: "Destinos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DuracionDias",
                table: "Destinos");

            migrationBuilder.DropColumn(
                name: "DuracionHoras",
                table: "Destinos");
        }
    }
}
