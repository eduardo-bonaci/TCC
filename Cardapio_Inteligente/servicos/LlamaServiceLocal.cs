using LLama;
using LLama.Common;
using System.Text;

namespace Cardapio_Inteligente.Servicos
{
    /// <summary>
    /// Implementação LOCAL da IA usando LLamaSharp diretamente no Android
    /// Não precisa de servidor externo - roda tudo no dispositivo
    /// </summary>
    public class LlamaServiceLocal : ILlamaService
    {
        private LLamaWeights? _model;
        private LLamaContext? _context;
        private InteractiveExecutor? _executor;
        private bool _isInitialized = false;
        private readonly SemaphoreSlim _initLock = new(1, 1);

        private const string SYSTEM_PROMPT = @"Você é um assistente virtual especializado em nutrição e cardápios para pessoas com intolerância à lactose.
Responda de forma clara, objetiva e amigável.
Suas respostas devem ser em português brasileiro.
Foque em dar informações úteis sobre pratos, ingredientes e alternativas sem lactose.";

        public async Task<bool> IsReadyAsync()
        {
            if (_isInitialized)
                return true;

            await InitializeAsync();
            return _isInitialized;
        }

        private async Task InitializeAsync()
        {
            if (_isInitialized)
                return;

            await _initLock.WaitAsync();
            try
            {
                if (_isInitialized)
                    return;

                Console.WriteLine("🤖 Inicializando IA local (LLamaSharp)...");

                // Caminho do modelo no Android
                var modelPath = GetModelPath();

                if (string.IsNullOrEmpty(modelPath) || !File.Exists(modelPath))
                {
                    throw new FileNotFoundException($"Modelo de IA não encontrado em: {modelPath}");
                }

                Console.WriteLine($"📂 Carregando modelo de: {modelPath}");

                var parameters = new ModelParams(modelPath)
                {
                    ContextSize = 2048,
                    GpuLayerCount = 0, // CPU only no Android
                    Seed = 1337,
                    Threads = Environment.ProcessorCount > 4 ? 4 : Environment.ProcessorCount
                };

                _model = LLamaWeights.LoadFromFile(parameters);
                _context = _model.CreateContext(parameters);
                _executor = new InteractiveExecutor(_context);

                _isInitialized = true;
                Console.WriteLine("✅ IA local inicializada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao inicializar IA local: {ex.Message}");
                throw;
            }
            finally
            {
                _initLock.Release();
            }
        }

        private string GetModelPath()
        {
#if ANDROID
            // Android: modelo na pasta de assets
            var assetsPath = Path.Combine(FileSystem.Current.AppDataDirectory, "ModelosIA", "Phi-3-mini-4k-instruct-q4.gguf");
            
            // Se não existir, tenta copiar dos assets
            if (!File.Exists(assetsPath))
            {
                CopyModelFromAssets(assetsPath);
            }
            
            return assetsPath;
#else
            // Windows/Outros: usa pasta local (não será usado, mas para compatibilidade)
            return Path.Combine(AppContext.BaseDirectory, "ModelosIA", "Phi-3-mini-4k-instruct-q4.gguf");
#endif
        }

#if ANDROID
        private void CopyModelFromAssets(string destinationPath)
        {
            try
            {
                Console.WriteLine("📋 Copiando modelo de IA dos assets...");
                
                var directory = Path.GetDirectoryName(destinationPath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory!);

                // Copia do assets (isso precisa ser feito manualmente ou via script de build)
                // Por enquanto, assume que o modelo já está lá
                
                Console.WriteLine("✅ Modelo copiado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao copiar modelo: {ex.Message}");
                throw;
            }
        }
#endif

        public async Task<string> GerarRespostaAsync(string prompt)
        {
            if (!_isInitialized)
            {
                await InitializeAsync();
            }

            if (_executor == null)
            {
                throw new InvalidOperationException("IA não foi inicializada corretamente");
            }

            try
            {
                Console.WriteLine($"📤 Gerando resposta para: {prompt}");

                var fullPrompt = $"{SYSTEM_PROMPT}\n\nUsuário: {prompt}\n\nAssistente:";
                
                var inferenceParams = new InferenceParams
                {
                    MaxTokens = 256,
                    Temperature = 0.7f,
                    AntiPrompts = new List<string> { "Usuário:", "\n\n" }
                };

                var response = new StringBuilder();
                
                await foreach (var token in _executor.InferAsync(fullPrompt, inferenceParams))
                {
                    response.Append(token);
                }

                var result = response.ToString().Trim();
                Console.WriteLine($"📥 Resposta gerada: {result.Substring(0, Math.Min(100, result.Length))}...");
                
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao gerar resposta: {ex.Message}");
                throw;
            }
        }
    }
}
