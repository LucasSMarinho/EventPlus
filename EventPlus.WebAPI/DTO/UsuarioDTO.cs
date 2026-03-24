using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "O nome do usuário é obrigatorio")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O Email do usuário é obrigatorio")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O Senha do usuário é obrigatorio")]
        public string? Senha { get; set; }

        [Required(ErrorMessage = "O IdTipoUsuario do usuário é obrigatorio")]
        public Guid? IdTipoUsuario { get; set; }
    }
}
