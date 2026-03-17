using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O Email do usuario é obrigatorio")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A Senha do usuario é obrigatoria")]
        public string? Senha { get; set; }
    }
}

