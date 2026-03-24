using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO
{
    public class PresencaDTOAtualizar
    {
        
        public bool Situacao { get; set; }

        public Guid IdUsuario { get; set; }

        public Guid IdEvento { get; set; }
    }
}
