using ContatosApi.Data;
using ContatosApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<IEnumerable<Contato>>> GetContato()
        {
            if(_context.Contatos == null)
            {
                return NotFound();
            }
            return await _context.Contatos.ToListAsync(); 
        }

        [HttpPost]
        public async Task<ActionResult<Contato>> AdicionarContato(Contato contato)
        {
            if(_context.Contatos == null)
            {
                return Problem("Entity set 'ContatoContext.Contatos' is null.");
            }
            _context.Contatos.Add(contato);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContato), 
                new { id = contato.Id }
                , contato);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarContato(int id, Contato contato)
        {
            if(id != contato.Id)
            {
                return BadRequest(" O ID fornecido não corresponde ao ID do contato.");
            }

            _context.Entry(contato).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarContato(int id)
        {
            if(_context.Contatos == null)
            {
                return NotFound();
            }

            var contato = await _context.Contatos.FindAsync(id);

            if(contato == null)
            {
                return NotFound();
            }

            _context.Contatos.Remove(contato);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
