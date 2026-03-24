using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO
{
    public class InstituicaoDTO
    {
        [Required(ErrorMessage = "O nome da instuição são obrigatorias")]
        public string? NomeFantasia { get; set; }

        [Required(ErrorMessage = "O Cnpj da instuição são obrigatorias")]
        public string? Cnpj { get; set; }

        [Required(ErrorMessage = "O endereço da instuição são obrigatorias")]
        public string? Endereco { get; set; }
    }
}
