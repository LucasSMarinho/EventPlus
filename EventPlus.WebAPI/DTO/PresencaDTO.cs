using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO
{
    public class PresencaDTO
    {
        [Required(ErrorMessage = "A situação da presença é obrigatoria")]
        public bool Situacao { get; set; }

        [Required(ErrorMessage = "O Id do usuário obrigatorio")]
        public Guid IdUsuario { get; set; }

        [Required(ErrorMessage = "O Id do evento é obrigatorio")]
        public Guid IdEvento { get; set; }
    }
}
