namespace EventPlus.WebAPI.DTO
{
    public class EventoDTOAtualizar
    {
        
            public string? Nome { get; set; }
            public string? Descricao { get; set; }
            public Guid? IdIntituicao { get; set; }
            public Guid? IdTipoEvento { get; set; }
            public DateTime DataEvento { get; set; }
    }

}
