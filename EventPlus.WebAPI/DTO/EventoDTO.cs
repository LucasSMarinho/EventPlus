using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO
{
    public class EventoDTO
    {
        [Required(ErrorMessage = "O nome do evento é obrigatorio")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "A data do evento é obrigatoria")]
        public DateTime DataEvento { get; set; }

        [Required(ErrorMessage = "A Descricao do evento é obrigatoria")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "A Intituicao do evento é obrigatoria")]
        public Guid? IdIntituicao { get; set; }

        [Required(ErrorMessage = "O Tipo do evento é obrigatorio")]
        public Guid? IdTipoEvento { get; set; }
    }
}
