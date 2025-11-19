using System.Text.Json.Serialization;

namespace Cardapio_Inteligente.Modelos
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [JsonIgnore]
        public string Senha { get; set; } = string.Empty;

        public string? Telefone { get; set; }
        
        // ✅ CORRIGIDO: Agora usa o mesmo nome da API
        public string? IngredientesNaoGosta { get; set; }
        
        public string? Alergias { get; set; }
        public string? Token { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
