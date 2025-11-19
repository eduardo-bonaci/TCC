using Cardapio_Inteligente.Api.Dados;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cardapio_Inteligente.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IngredientesController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna lista única de todos os ingredientes cadastrados nos pratos
        /// </summary>
        [HttpGet]
        [AllowAnonymous] // Permite acesso sem autenticação para facilitar cadastro
        [ProducesResponseType(typeof(List<string>), 200)]
        public async Task<ActionResult<List<string>>> GetIngredientes()
        {
            try
            {
                // Busca todos os pratos e extrai ingredientes únicos
                var pratos = await _context.Pratos
                    .AsNoTracking()
                    .Select(p => p.Ingredientes)
                    .ToListAsync();

                // Separa ingredientes por vírgula e remove duplicatas
                var ingredientesUnicos = pratos
                    .Where(i => !string.IsNullOrWhiteSpace(i))
                    .SelectMany(i => i.Split(',', ';'))
                    .Select(i => i.Trim())
                    .Where(i => !string.IsNullOrWhiteSpace(i))
                    .Distinct()
                    .OrderBy(i => i)
                    .ToList();

                // Se não houver pratos cadastrados, retorna lista padrão
                if (ingredientesUnicos.Count == 0)
                {
                    ingredientesUnicos = new List<string>
                    {
                        "Leite", "Queijo", "Manteiga", "Creme de Leite",
                        "Tomate", "Alface", "Cebola", "Alho",
                        "Frango", "Carne Bovina", "Peixe", "Camarão",
                        "Arroz", "Feijão", "Batata", "Macarrão"
                    };
                }

                return Ok(ingredientesUnicos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao buscar ingredientes: {ex.Message}");
                return StatusCode(500, new { mensagem = "Erro ao buscar ingredientes.", detalhe = ex.Message });
            }
        }
    }
}
