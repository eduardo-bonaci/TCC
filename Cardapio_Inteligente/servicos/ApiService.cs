using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Cardapio_Inteligente.Modelos;
using System.Net.Http.Headers;
using Microsoft.Maui.Storage;

namespace Cardapio_Inteligente.Servicos
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private string? _token;

        // ✅ Ordem corrigida: tenta primeiro 10.0.2.2 (Android), depois localhost (Windows)
        private readonly string[] _baseAddresses = new[] { "http://10.0.2.2:5068", "http://localhost:5068" };

        public ApiService()
        {
            _httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(30)
            };

            // Carrega token salvo (se houver)
            try
            {
                var saved = Preferences.Get("jwt_token", string.Empty);
                if (!string.IsNullOrWhiteSpace(saved))
                    SetToken(saved);
            }
            catch
            {
                // Ignora falha em Preferences (alguns ambientes Android)
            }
        }

        // ----------------------------------------------------------------------
        // 🔹 Autenticação e token persistente
        // ----------------------------------------------------------------------
        public void SetToken(string token)
        {
            _token = token;
            if (!string.IsNullOrWhiteSpace(token))
            {
                if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
                    _httpClient.DefaultRequestHeaders.Remove("Authorization");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                try
                {
                    Preferences.Set("jwt_token", token);
                }
                catch { }
            }
            else
            {
                if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
                    _httpClient.DefaultRequestHeaders.Remove("Authorization");

                try
                {
                    Preferences.Remove("jwt_token");
                }
                catch { }
            }
        }

        public void Logout()
        {
            SetToken(string.Empty);
        }

        // ----------------------------------------------------------------------
        // 🔹 Lógica de fallback — tenta 10.0.2.2 primeiro
        // ----------------------------------------------------------------------
        private async Task<HttpResponseMessage> SendWithFallbackAsync(Func<Uri, Task<HttpResponseMessage>> action)
        {
            foreach (var baseAddr in _baseAddresses)
            {
                try
                {
                    var baseUri = new Uri(baseAddr.EndsWith("/") ? baseAddr : baseAddr + "/");

                    // Timeout curto para o primeiro host
                    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));

                    var response = await action(baseUri);
                    return response;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Falha ao conectar em {baseAddr}: {ex.Message}");
                    continue;
                }
            }

            throw new Exception("❌ Não foi possível conectar à API em nenhum endpoint configurado.");
        }

        // ----------------------------------------------------------------------
        // 🔹 LOGIN
        // ----------------------------------------------------------------------
        public async Task<LoginResponse?> LoginAsync(string email, string senha)
        {
            var loginData = new { Email = email, Senha = senha };
            var response = await SendWithFallbackAsync(async baseUri =>
            {
                var url = new Uri(baseUri, "api/Usuarios/Login");
                return await _httpClient.PostAsJsonAsync(url, loginData);
            });

            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadAsStringAsync();
                throw new Exception($"Falha no login: {response.StatusCode} - {err}");
            }

            var respostaJson = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<LoginResponse>(respostaJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
                SetToken(loginResponse.Token);

            return loginResponse;
        }

        // ----------------------------------------------------------------------
        // 🔹 CADASTRO DE USUÁRIO
        // ----------------------------------------------------------------------
        public async Task<bool> CadastrarUsuarioAsync(Usuario novoUsuario)
        {
            var response = await SendWithFallbackAsync(async baseUri =>
            {
                var url = new Uri(baseUri, "api/Usuarios/Cadastrar");
                return await _httpClient.PostAsJsonAsync(url, novoUsuario);
            });

            if (response.IsSuccessStatusCode)
                return true;

            var erro = await response.Content.ReadAsStringAsync();
            throw new Exception($"Falha ao cadastrar: {erro}");
        }

        // ----------------------------------------------------------------------
        // 🔹 LISTAR PRATOS
        // ----------------------------------------------------------------------
        public async Task<List<Prato>> GetPratosAsync(string? alergias = null, string? categoria = null)
        {
            var urlPath = "api/Pratos";
            var queryParams = new List<string>();
            if (!string.IsNullOrEmpty(alergias)) queryParams.Add($"alergias={Uri.EscapeDataString(alergias)}");
            if (!string.IsNullOrEmpty(categoria)) queryParams.Add($"categoria={Uri.EscapeDataString(categoria)}");
            if (queryParams.Count > 0) urlPath += "?" + string.Join("&", queryParams);

            var response = await SendWithFallbackAsync(async baseUri =>
            {
                var url = new Uri(baseUri, urlPath);
                return await _httpClient.GetAsync(url);
            });

            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao buscar pratos: {response.StatusCode} - {err}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var pratos = JsonSerializer.Deserialize<List<Prato>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return pratos ?? new List<Prato>();
        }

        // ----------------------------------------------------------------------
        // 🔹 LISTAR INGREDIENTES
        // ----------------------------------------------------------------------
        public async Task<List<string>> GetIngredientesAsync()
        {
            var response = await SendWithFallbackAsync(async baseUri =>
            {
                var url = new Uri(baseUri, "api/Ingredientes");
                return await _httpClient.GetAsync(url);
            });

            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao buscar ingredientes: {response.StatusCode} - {err}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var ingredientes = JsonSerializer.Deserialize<List<string>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return ingredientes ?? new List<string>();
        }

        // ----------------------------------------------------------------------
        // 🔹 CHAMAR IA
        // ----------------------------------------------------------------------
        public async Task<string> GerarRespostaIAAsync(string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
                throw new ArgumentException("Prompt não pode ser vazio.", nameof(prompt));

            var promptObject = new { Prompt = prompt };
            var content = new StringContent(JsonSerializer.Serialize(promptObject), Encoding.UTF8, "application/json");

            var response = await SendWithFallbackAsync(async baseUri =>
            {
                var url = new Uri(baseUri, "api/Pratos/assistente-chat");
                return await _httpClient.PostAsync(url, content);
            });

            var respostaTexto = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Erro na API (Status {response.StatusCode}): {respostaTexto}");

            try
            {
                using var doc = JsonDocument.Parse(respostaTexto);
                if (doc.RootElement.TryGetProperty("mensagem", out var mens))
                {
                    return mens.GetString() ?? string.Empty;
                }
                var trimmed = respostaTexto.Trim().Trim('"');
                return trimmed;
            }
            catch
            {
                return respostaTexto.Trim().Trim('"');
            }
        }
    }
}
