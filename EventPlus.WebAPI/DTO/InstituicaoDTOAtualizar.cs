using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO
{
    public class InstituicaoDTOAtualizar
    {
        public string? NomeFantasia { get; set; }
        public string? Cnpj { get; set; }
        public string? Endereco { get; set; }
    }
}
