using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IEventoRepository
{
    void Cadastrar(Evento evento);
    List<Evento> Listar();
    void Deletar(Guid IdEvento);
    void Atualizar(Guid IdEvento);
    List<Evento> ListarPorId(Guid IdUsuario);
    List<Evento> ProximosEventos();
    List<Evento> BuscarPorId(Guid Id);
}
