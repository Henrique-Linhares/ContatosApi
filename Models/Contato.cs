using System.ComponentModel.DataAnnotations;

namespace ContatosApi.Models
{
    public class Contato
    {
        [Key]
        [Required]
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Telefone { get; set; }
    }
}
