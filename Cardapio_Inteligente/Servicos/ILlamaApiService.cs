namespace Cardapio_Inteligente.Servicos
{
    /// <summary>
    /// Interface para o serviço de IA da API interna
    /// </summary>
    public interface ILlamaApiService
    {
        Task<bool> IsReadyAsync();
        Task<string> ProcessarPerguntaAsync(string pergunta);
    }
}
