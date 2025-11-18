using Microsoft.Maui.Controls;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Cardapio_Inteligente.Paginas
{
    public partial class ChatPage : ContentPage
    {
        private readonly HttpClient _httpClient = new();

        public ChatPage()
        {
            InitializeComponent();
        }

        private async void OnSendClicked(object sender, EventArgs e)
        {
            var pergunta = PromptEntry.Text?.Trim();
            if (string.IsNullOrWhiteSpace(pergunta))
                return;

            AddMessage("Você", pergunta);
            PromptEntry.Text = string.Empty;

            AddMessage("IA", "Digitando...");

            try
            {
                var resposta = await ObterRespostaIA(pergunta);
                RemoveLastMessage(); // remove o "Digitando..."
                AddMessage("IA", resposta);
            }
            catch (Exception ex)
            {
                RemoveLastMessage();
                AddMessage("Erro", $"Falha ao conectar à IA: {ex.Message}");
            }
        }

        private async Task<string> ObterRespostaIA(string prompt)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7284/api/pratos/assistente-chat", new { prompt });

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        private void AddMessage(string remetente, string texto)
        {
            MessagesStack.Children.Add(new Label
            {
                Text = $"{remetente}: {texto}",
                Style = (Style)Resources["MessageLabel"]
            });

            ChatScroll.ScrollToAsync(MessagesStack, ScrollToPosition.End, true);
        }

        private void RemoveLastMessage()
        {
            if (MessagesStack.Children.Count > 0)
                MessagesStack.Children.RemoveAt(MessagesStack.Children.Count - 1);
        }
    }
}
