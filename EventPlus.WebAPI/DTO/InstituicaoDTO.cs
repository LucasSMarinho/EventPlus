using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO
{
    public class InstituicaoDTO
    {
        [Required(ErrorMessage = "As informações da instuição são obrigatorias")]
        public string? NomeFantasia { get; set; }
        public string? Cnpj { get; set; }
        public string? Endereco { get; set; }
    }
}
