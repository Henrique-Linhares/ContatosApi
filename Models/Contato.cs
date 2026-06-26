using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ContatosApi.Models
{
    public class Contato
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Telefone { get; set; }
    }
}
