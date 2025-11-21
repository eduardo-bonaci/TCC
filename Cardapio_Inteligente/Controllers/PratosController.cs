using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cardapio_Inteligente.Modelos;
using Cardapio_Inteligente.Servicos;

namespace Cardapio_Inteligente.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PratosController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public PratosController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Prato>>> GetPratos()
        {
            return await _context.Pratos.ToListAsync();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Prato>> GetPrato(int id)
        {
            var prato = await _context.Pratos.FindAsync(id);

            if (prato == null)
            {
                return NotFound();
            }

            return prato;
        }

        [HttpPost]
        public async Task<ActionResult<Prato>> CreatePrato(Prato prato)
        {
            _context.Pratos.Add(prato);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPrato), new { id = prato.Id }, prato);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrato(int id, Prato prato)
        {
            if (id != prato.Id)
            {
                return BadRequest();
            }

            _context.Entry(prato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Pratos.AnyAsync(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrato(int id)
        {
            var prato = await _context.Pratos.FindAsync(id);
            if (prato == null)
            {
                return NotFound();
            }

            _context.Pratos.Remove(prato);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
