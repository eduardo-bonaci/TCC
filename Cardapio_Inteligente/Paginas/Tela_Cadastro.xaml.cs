using System.Collections.ObjectModel;
using Cardapio_Inteligente.Modelos;
using Cardapio_Inteligente.Servicos;
using Microsoft.Maui.Controls;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cardapio_Inteligente.Paginas;

public partial class Tela_Cadastro : ContentPage
{
    private RepositorioUsuario repositorio = new RepositorioUsuario();
    private AssistenteConversacional assistente;
    private Dictionary<string, string> respostas = new();
    private ObservableCollection<KeyValuePair<string, string>> resumo = new();
    private string chaveAtual = string.Empty;

    private List<CheckBox> checkPreferencias = new();
    private List<string> ingredientes = new();

    public Tela_Cadastro()
    {
        InitializeComponent();
        assistente = new AssistenteConversacional(repositorio.ApiService);
        colRespostas.ItemsSource = resumo;

        _ = CarregarIngredientesAsync();
        btnIniciar_Clicked(this, EventArgs.Empty);
    }

    private async Task CarregarIngredientesAsync()
    {
        try
        {
            ingredientes = await repositorio.ApiService.GetIngredientesAsync();

            stackPreferencias.Children.Clear();
            checkPreferencias.Clear();

            foreach (var ing in ingredientes)
            {
                var check = new CheckBox();
                checkPreferencias.Add(check);

                var label = new Label { Text = ing, VerticalOptions = LayoutOptions.Center, TextColor = Microsoft.Maui.Graphics.Colors.White };

                var horizontal = new HorizontalStackLayout { Spacing = 10 };
                horizontal.Children.Add(check);
                horizontal.Children.Add(label);

                stackPreferencias.Children.Add(horizontal);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"N�o foi poss�vel carregar os ingredientes: {ex.Message}", "OK");
        }
    }

    private void btnIniciar_Clicked(object sender, EventArgs e)
    {
        respostas.Clear();
        resumo.Clear();
        txtResposta.Text = string.Empty;
        txtResposta.IsEnabled = true;
        btnEnviar.IsEnabled = true;
        btnIniciar.IsVisible = false;

        var primeiraPergunta = assistente.ObterPrimeiraPergunta();
        lblPergunta.Text = primeiraPergunta.texto;
        chaveAtual = primeiraPergunta.chave;
    }

    private async void btnEnviar_Clicked(object sender, EventArgs e)
    {
        string resposta = txtResposta.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrEmpty(resposta) && chaveAtual != "ingredientesNaoGosta")
        {
            await DisplayAlert("Aviso", "Por favor, digite uma resposta.", "OK");
            return;
        }

        respostas.Add(chaveAtual, resposta);
        ObterResumo();

        var proximaPergunta = assistente.ObterProximaPergunta(respostas);

        if (proximaPergunta.HasValue)
        {
            lblPergunta.Text = proximaPergunta.Value.texto;
            chaveAtual = proximaPergunta.Value.chave;
            txtResposta.Text = string.Empty;
            btnSalvar.IsEnabled = false;
        }
        else
        {
            lblPergunta.Text = "Cadastro conclu�do! Revise suas respostas abaixo e clique em Salvar.";
            txtResposta.IsEnabled = false;
            btnEnviar.IsEnabled = false;
            btnSalvar.IsEnabled = true;
        }
    }

    private void ObterResumo()
    {
        resumo.Clear();
        foreach (var r in respostas)
        {
            string nomeFormatado = char.ToUpper(r.Key[0]) + r.Key.Substring(1);
            resumo.Add(new KeyValuePair<string, string>(
                nomeFormatado,
                r.Key.ToLower() == "senha" ? "********" : r.Value
            ));
        }
    }

    private void MudarEstadoUI(bool carregando)
    {
        activityIndicator.IsRunning = carregando;
        activityIndicator.IsVisible = carregando;
        btnSalvar.IsEnabled = !carregando && resumo.Count >= 6;
        btnCancelar.IsEnabled = !carregando;
        btnEnviar.IsEnabled = !carregando && (chaveAtual != string.Empty);
        txtResposta.IsEnabled = !carregando && (chaveAtual != string.Empty);
        btnIniciar.IsEnabled = !carregando;
    }

    private async void btnSalvar_Clicked(object sender, EventArgs e)
    {
        var usuario = new Usuario
        {
            Nome = respostas.GetValueOrDefault("nome") ?? "",
            Email = respostas.GetValueOrDefault("email") ?? "",
            Senha = respostas.GetValueOrDefault("senha") ?? "",
            Telefone = respostas.GetValueOrDefault("telefone") ?? "",
            // ✅ CORRIGIDO: Agora usa IngredientesNaoGosta ao invés de Preferencias
            IngredientesNaoGosta = string.Join(", ",
                checkPreferencias
                    .Where(c => c.IsChecked)
                    .Select(c => ((HorizontalStackLayout)c.Parent).Children[1] as Label)
                    .Where(l => l != null)
                    .Select(l => l.Text)),
            Alergias = rbtLactose.IsChecked ? "Lactose" : "Nenhuma",
            DataCadastro = DateTime.UtcNow
        };

        try
        {
            MudarEstadoUI(true);
            await repositorio.SalvarUsuarioAsync(usuario);
            await DisplayAlert("Sucesso", "Cadastro realizado! Voc� ser� redirecionado para o Login.", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Falha no cadastro: {ex.Message}", "OK");
        }
        finally
        {
            MudarEstadoUI(false);
        }
    }

    private async void btnCancelar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
