using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cardapio_Inteligente.Api.Migrations
{
    public partial class RenomearPreferenciasParaIngredientesNaoGosta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Preferencias",
                table: "usuarios",
                newName: "IngredientesNaoGosta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IngredientesNaoGosta",
                table: "usuarios",
                newName: "Preferencias");
        }
    }
}
