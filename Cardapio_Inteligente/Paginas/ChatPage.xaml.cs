using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using Cardapio_Inteligente.Servicos;

namespace Cardapio_Inteligente.Paginas
{
    public partial class ChatPage : ContentPage
    {
        private readonly ApiService _apiService;

        // ✅ CORRIGIDO: Agora recebe ApiService por parâmetro ou cria novo
        public ChatPage(ApiService? apiService = null)
        {
            InitializeComponent();
            _apiService = apiService ?? new ApiService();
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
                // ✅ CORRIGIDO: Usa ApiService ao invés de HttpClient direto
                var resposta = await _apiService.GerarRespostaIAAsync(pergunta);
                RemoveLastMessage(); // remove o "Digitando..."
                AddMessage("IA", resposta);
            }
            catch (Exception ex)
            {
                RemoveLastMessage();
                AddMessage("Erro", $"Falha ao conectar à IA: {ex.Message}");
            }
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
