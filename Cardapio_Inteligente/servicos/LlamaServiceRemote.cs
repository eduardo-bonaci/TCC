using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Cardapio_Inteligente.Servicos
{
    /// <summary>
    /// Implementação REMOTA da IA que usa a API via HTTP (Windows)
    /// Conecta em http://localhost:5068 onde o serviço Windows está rodando
    /// </summary>
    public class LlamaServiceRemote : ILlamaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        private bool _isReady = false;

        public LlamaServiceRemote(string apiUrl = "http://localhost:5068")
        {
            _apiUrl = apiUrl;
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(60)
            };
        }

        public async Task<bool> IsReadyAsync()
        {
            if (_isReady)
                return true;

            try
            {
                // Tenta fazer uma requisição de teste
                var response = await _httpClient.GetAsync($"{_apiUrl}/api/Pratos", new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token);
                _isReady = response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.Unauthorized;
                
                if (_isReady)
                {
                    Console.WriteLine("✅ API remota está acessível");
                }
                else
                {
                    Console.WriteLine($"⚠️ API retornou status: {response.StatusCode}");
                }
                
                return _isReady;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ API não está acessível: {ex.Message}");
                _isReady = false;
                return false;
            }
        }

        public async Task<string> GerarRespostaAsync(string prompt)
        {
            if (!_isReady)
            {
                var ready = await IsReadyAsync();
                if (!ready)
                {
                    throw new Exception("API não está disponível. Certifique-se de que o serviço Windows está rodando.");
                }
            }

            try
            {
                Console.WriteLine($"📤 Enviando pergunta para API: {prompt}");

                var requestData = new { Prompt = prompt };
                var content = new StringContent(
                    JsonSerializer.Serialize(requestData),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _httpClient.PostAsync($"{_apiUrl}/api/Pratos/assistente-chat", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API retornou erro: {response.StatusCode} - {errorContent}");
                }

                var responseText = await response.Content.ReadAsStringAsync();

                // Tenta parsear JSON
                try
                {
                    using var doc = JsonDocument.Parse(responseText);
                    if (doc.RootElement.TryGetProperty("mensagem", out var mensagem))
                    {
                        var resultado = mensagem.GetString() ?? responseText;
                        Console.WriteLine($"📥 Resposta da API recebida");
                        return resultado;
                    }
                }
                catch
                {
                    // Se não for JSON, retorna o texto direto
                }

                var result = responseText.Trim().Trim('"');
                Console.WriteLine($"📥 Resposta da API recebida");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao comunicar com API: {ex.Message}");
                throw;
            }
        }
    }
}
