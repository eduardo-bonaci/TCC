using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cardapio_Inteligente.Servicos;

namespace Cardapio_Inteligente.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IAController : ControllerBase
    {
        private readonly ILlamaApiService _llamaService;

        public IAController(ILlamaApiService llamaService)
        {
            _llamaService = llamaService;
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            var isReady = await _llamaService.IsReadyAsync();
            return Ok(new { ready = isReady, message = isReady ? "IA pronta" : "IA inicializando..." });
        }

        [HttpPost("chat")]
        [AllowAnonymous]
        public async Task<IActionResult> Chat([FromBody] ChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Mensagem))
            {
                return BadRequest(new { message = "Mensagem não pode estar vazia" });
            }

            var resposta = await _llamaService.ProcessarPerguntaAsync(request.Mensagem);
            return Ok(new { resposta });
        }
    }

    public class ChatRequest
    {
        public string Mensagem { get; set; } = "";
    }
}
