using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgenciaDeTours.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguraRelacionPaisDestinos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Destinos_PaisId",
                table: "Destinos",
                column: "PaisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Destinos_Paises_PaisId",
                table: "Destinos",
                column: "PaisId",
                principalTable: "Paises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Destinos_Paises_PaisId",
                table: "Destinos");

            migrationBuilder.DropIndex(
                name: "IX_Destinos_PaisId",
                table: "Destinos");
        }
    }
}
