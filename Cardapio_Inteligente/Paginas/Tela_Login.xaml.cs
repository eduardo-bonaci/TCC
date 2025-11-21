using Cardapio_Inteligente.Modelos;
using Cardapio_Inteligente.Servicos;
using Microsoft.Maui.Controls;
using System;
using System.Net.Mail;

namespace Cardapio_Inteligente.Paginas;

public partial class Tela_Login : ContentPage
{
    private readonly RepositorioUsuario repositorio = new RepositorioUsuario();

    public Tela_Login()
    {
        InitializeComponent();
    }

    private void SetLoading(bool isLoading)
    {
        activityIndicator.IsVisible = isLoading;
        activityIndicator.IsRunning = isLoading;
        btnEntrar.IsEnabled = !isLoading;
        btnNaoTemCadastro.IsEnabled = !isLoading;
        txtEmail.IsEnabled = !isLoading;
        txtSenha.IsEnabled = !isLoading;
        lblMensagemErro.IsVisible = !isLoading ? lblMensagemErro.IsVisible : false;
    }

    private async void btnEntrar_Clicked(object sender, EventArgs e)
    {
        string email = txtEmail?.Text?.Trim() ?? string.Empty;
        string senha = txtSenha?.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
        {
            await DisplayAlert("Erro", "Por favor, preencha todos os campos.", "OK");
            return;
        }

        try
        {
            var endereco = new MailAddress(email);
            var dominio = endereco.Host.ToLowerInvariant();

            if (!(dominio == "gmail.com" || dominio == "hotmail.com" || dominio == "outlook.com"))
            {
                await DisplayAlert("Erro", "O domínio do e-mail deve ser gmail.com, hotmail.com ou outlook.com.", "OK");
                return;
            }
        }
        catch
        {
            await DisplayAlert("Erro", "Por favor, insira um e-mail válido.", "OK");
            return;
        }

        SetLoading(true);

        try
        {
            var usuarioLogado = await repositorio.RealizarLoginAsync(email, senha);

            if (usuarioLogado != null)
            {
                await DisplayAlert("Bem-vindo", $"Login efetuado com sucesso! Olá, {usuarioLogado.Nome}.", "OK");
                await Navigation.PushAsync(new PaginaInicial(usuarioLogado, repositorio.ApiService));
            }
            else
            {
                lblMensagemErro.Text = "E-mail ou senha inválidos.";
                lblMensagemErro.IsVisible = true;
                await DisplayAlert("Erro de Login", "E-mail ou senha inválidos.", "OK");
            }
        }
        catch (Exception ex)
        {
            lblMensagemErro.Text = $"Não foi possível conectar ao servidor. Detalhe: {ex.Message}";
            lblMensagemErro.IsVisible = true;
            await DisplayAlert("Erro de Comunicação", $"Não foi possível conectar ao servidor. Detalhe: {ex.Message}", "OK");
        }
        finally
        {
            SetLoading(false);
        }
    }

    private async void btnNaoTemCadastro_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Tela_Cadastro());
    }
}
