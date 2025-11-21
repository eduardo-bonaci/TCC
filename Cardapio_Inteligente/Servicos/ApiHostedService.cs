using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cardapio_Inteligente.Servicos
{
    /// <summary>
    /// Serviço que roda a API WebAPI internamente no MAUI (apenas Windows)
    /// Isso elimina a necessidade de rodar a API separadamente
    /// </summary>
    public class ApiHostedService : IHostedService
    {
        private IHost? _apiHost;
        private readonly ILogger<ApiHostedService> _logger;

        public ApiHostedService(ILogger<ApiHostedService> logger)
        {
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("🚀 Iniciando API interna...");

                // Cria o host da API WebAPI
                _apiHost = Host.CreateDefaultBuilder()
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseUrls("http://localhost:5068");
                        webBuilder.UseStartup<ApiStartup>();
                    })
                    .Build();

                // Inicia a API em background
                await _apiHost.StartAsync(cancellationToken);

                _logger.LogInformation("✅ API interna iniciada em http://localhost:5068");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Erro ao iniciar API interna");
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                if (_apiHost != null)
                {
                    _logger.LogInformation("🛑 Parando API interna...");
                    await _apiHost.StopAsync(cancellationToken);
                    _apiHost.Dispose();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Erro ao parar API interna");
            }
        }
    }
}
