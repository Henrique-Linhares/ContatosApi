using ContatosApi.Data;
using ContatosApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq; 

namespace ContatosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private readonly ContatoContext _context;

        public ContatoController(ContatoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contato>>> GetContatos([FromQuery] string? busca)
        {
            if (_context.Contatos == null) return NotFound();

            var consulta = _context.Contatos.AsQueryable();

            if (!string.IsNullOrEmpty(busca))
            {
                consulta = consulta.Where(c => c.Name.Contains(busca) || c.Telefone.Contains(busca));
            }

            return await consulta.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contato>> GetContatoPorId(int id)
        {
            if (_context.Contatos == null) return NotFound();

            var contato = await _context.Contatos.FirstOrDefaultAsync(c => c.Id == id);

            if (contato == null) return NotFound("Contato não encontrado.");

            return contato;
        }

        [HttpPost]
        public async Task<ActionResult<Contato>> AdicionarContato(Contato contato)
        {
            if (_context.Contatos == null)
            {
                return Problem("Entity set 'ContatoContext.Contatos' is null.");
            }
            _context.Contatos.Add(contato);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContatoPorId),
                new { id = contato.Id },
                contato);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarContato(int id, Contato contato)
        {
            if (id != contato.Id)
            {
                return BadRequest("O ID fornecido não corresponde ao ID do contato.");
            }

            var existe = await _context.Contatos.AnyAsync(c => c.Id == id);
            if (!existe) return NotFound("Contato não encontrado para atualização.");

            _context.Entry(contato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarContato(int id)
        {
            if (_context.Contatos == null) return NotFound();

            var contato = await _context.Contatos.SingleOrDefaultAsync(c => c.Id == id);

            if (contato == null) return NotFound();

            _context.Contatos.Remove(contato);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
