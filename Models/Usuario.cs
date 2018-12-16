using System.ComponentModel.DataAnnotations;

namespace vidibr_api.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        [Required]
        [StringLength(255)]
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
    }
}