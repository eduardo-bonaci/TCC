using Cardapio_Inteligente.Modelos;
using System.Threading.Tasks;

namespace Cardapio_Inteligente.Servicos
{
    public class RepositorioUsuario
    {
        private readonly ApiService _apiService;

        public RepositorioUsuario()
        {
            _apiService = new ApiService();
        }

        // Expor ApiService para manter token após login
        public ApiService ApiService => _apiService;

        public async Task<Usuario?> RealizarLoginAsync(string email, string senha)
        {
            var loginResponse = await _apiService.LoginAsync(email, senha);
            if (loginResponse == null || loginResponse.Token == null)
                return null;

            var usuario = loginResponse.Usuario ?? new Usuario { Email = email };
            usuario.Token = loginResponse.Token;

            // Seta token no ApiService
            _apiService.SetToken(usuario.Token);

            return usuario;
        }

        public async Task<bool> SalvarUsuarioAsync(Usuario novoUsuario)
        {
            return await _apiService.CadastrarUsuarioAsync(novoUsuario);
        }
    }
}
