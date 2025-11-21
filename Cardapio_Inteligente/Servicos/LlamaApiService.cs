using LLama;
using LLama.Common;
using Microsoft.Extensions.Logging;

namespace Cardapio_Inteligente.Servicos
{
    /// <summary>
    /// Implementação do serviço de IA para a API interna (Windows)
    /// Carrega o modelo LLama localmente
    /// </summary>
    public class LlamaApiService : ILlamaApiService
    {
        private readonly ILogger<LlamaApiService> _logger;
        private LLamaWeights? _model;
        private LLamaContext? _context;
        private InteractiveExecutor? _executor;
        private bool _isInitialized = false;

        public LlamaApiService(ILogger<LlamaApiService> logger)
        {
            _logger = logger;
            _ = InitializeAsync(); // Inicializa em background
        }

        private async Task InitializeAsync()
        {
            try
            {
                _logger.LogInformation("🤖 Carregando modelo de IA...");

                // Caminho do modelo (ajuste conforme necessário)
                var modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ModelosIA", "Phi-3-mini-4k-instruct-q4.gguf");

                if (!File.Exists(modelPath))
                {
                    _logger.LogWarning($"⚠️ Modelo não encontrado em: {modelPath}");
                    return;
                }

                var parameters = new ModelParams(modelPath)
                {
                    ContextSize = 2048,
                    GpuLayerCount = 0, // CPU apenas
                    Seed = 1337
                };

                _model = LLamaWeights.LoadFromFile(parameters);
                _context = _model.CreateContext(parameters);
                _executor = new InteractiveExecutor(_context);

                _isInitialized = true;
                _logger.LogInformation("✅ Modelo de IA carregado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Erro ao carregar modelo de IA");
            }
        }

        public async Task<bool> IsReadyAsync()
        {
            return await Task.FromResult(_isInitialized);
        }

        public async Task<string> ProcessarPerguntaAsync(string pergunta)
        {
            if (!_isInitialized || _executor == null)
            {
                return "Desculpe, o serviço de IA ainda está sendo inicializado. Por favor, tente novamente em alguns instantes.";
            }

            try
            {
                var prompt = $@"Você é um assistente especializado em culinária e cardápios de restaurantes.
Responda de forma breve, objetiva e útil.

Pergunta: {pergunta}
Resposta:";

                var inferenceParams = new InferenceParams
                {
                    MaxTokens = 256,
                    Temperature = 0.7f,
                    TopP = 0.9f,
                    AntiPrompts = new[] { "Pergunta:", "\n\n" }
                };

                var response = "";
                await foreach (var text in _executor.InferAsync(prompt, inferenceParams))
                {
                    response += text;
                }

                return response.Trim();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar pergunta");
                return "Desculpe, ocorreu um erro ao processar sua pergunta.";
            }
        }
    }
}
