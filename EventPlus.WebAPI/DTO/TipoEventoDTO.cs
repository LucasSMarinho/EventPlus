using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

//Data Transfer Object
public class TipoEventoDTO
{
    [Required(ErrorMessage = "O titulo do tipo de evento é obrigatório")]
    public string? Titulo { get; set; }
}
