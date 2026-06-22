using Microsoft.EntityFrameworkCore;

namespace ContatosApi.Data
{
    public class ContatoContext : DbContext
    {
        public ContatoContext(DbContextOptions<ContatoContext> options) : base(options)
        {
        }
        public DbSet<Models.Contato> Contatos { get; set; }
    }
}
