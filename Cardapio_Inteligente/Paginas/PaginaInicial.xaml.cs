using Cardapio_Inteligente.Modelos;
using Cardapio_Inteligente.Servicos;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using System;
using Cardapio_Inteligente.Paginas;

namespace Cardapio_Inteligente.Paginas
{
    public partial class PaginaInicial : ContentPage
    {
        public Usuario? UsuarioLogado { get; private set; }
        private readonly ApiService _apiService;
        private readonly AssistenteConversacional _assistente;
        private ObservableCollection<Prato> _pratos = new();

        public PaginaInicial(Usuario? usuario = null, ApiService? apiService = null)
        {
            InitializeComponent();

            UsuarioLogado = usuario;
            _apiService = apiService ?? new ApiService();
            _assistente = new AssistenteConversacional(_apiService);

            listaDePratos.ItemsSource = _pratos;

            if (UsuarioLogado != null && !string.IsNullOrEmpty(UsuarioLogado.Token))
            {
                _apiService.SetToken(UsuarioLogado.Token);
            }

            CarregarPratos();
        }

        private async void CarregarPratos(string? alergias = null, string? categoria = null)
        {
            try
            {
                var pratos = await _apiService.GetPratosAsync(alergias, categoria);

                _pratos.Clear();
                if (pratos != null)
                {
                    foreach (var prato in pratos)
                        _pratos.Add(prato);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Falha ao carregar pratos: {ex.Message}", "OK");
            }
        }

        private void btnSemLactose_Clicked(object sender, EventArgs e)
        {
            CarregarPratos(alergias: "lactose");
        }

        private void btnComLactose_Clicked(object sender, EventArgs e)
        {
            CarregarPratos();
        }

        private async void btnPerguntar_Clicked(object sender, EventArgs e)
        {
            string pergunta = txtPergunta.Text?.Trim();
            if (string.IsNullOrEmpty(pergunta))
            {
                await DisplayAlert("Aviso", "Digite uma pergunta sobre o cardápio.", "OK");
                return;
            }

            try
            {
                string respostaIA = await _assistente.ProcessarMensagemMenuAsync(pergunta);
                await DisplayAlert("Assistente Inteligente", respostaIA, "OK");
                txtPergunta.Text = string.Empty;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro de IA", $"Não foi possível obter a resposta do assistente. Detalhe: {ex.Message}", "OK");
            }
        }

        private async void OnAssistenteClicked(object sender, EventArgs e)
        {
            // ✅ CORRIGIDO: Passa ApiService para ChatPage
            await Navigation.PushAsync(new ChatPage(_apiService));
        }
    }

    }
