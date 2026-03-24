using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EventPlus.WebAPI.Repositories;

public class EventoRepository : IEventoRepository
{

    private readonly EventContext _context;

    public EventoRepository(EventContext context)
    {
        _context = context; 
    }

    /// <summary>
    /// Metodo que atualiza as informações do evento
    /// </summary>
    /// <param name="Id">Id evento que será atualizado</param>
    /// <param name="evento">Novos dados do evento</param>
    public void Atualizar(Guid Id, Evento evento)
    {
        var eventoBuscado = _context.Eventos.Find(Id);

        if (eventoBuscado != null)
        {
            eventoBuscado.Nome = String.IsNullOrWhiteSpace(evento.Nome) ? eventoBuscado.Nome : evento.Nome;
            eventoBuscado.DataEvento = evento.DataEvento == DateTime.MinValue ? eventoBuscado.DataEvento : evento.DataEvento;
            eventoBuscado.Descricao = String.IsNullOrWhiteSpace(evento.Descricao) ? eventoBuscado.Descricao : evento.Descricao;
            eventoBuscado.IdTipoEvento = (evento.IdTipoEvento == null) ? eventoBuscado.IdTipoEvento : evento.IdTipoEvento;
            eventoBuscado.IdInstituicao = (evento.IdInstituicao == null) ? eventoBuscado.IdInstituicao : evento.IdInstituicao;


            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Metodo que busca um evento por id
    /// </summary>
    /// <param name="Id">Id do evento que será buscado</param>
    /// <returns>retorna o evento buscado</returns>
    public Evento BuscarPorId(Guid Id)
    {
        return _context.Eventos.Find(Id)!;
    }

    /// <summary>
    /// Metodo que adiciona um evento
    /// </summary>
    /// <param name="evento">Dados do novo evento</param>
    public void Cadastrar(Evento evento)
    {
        _context.Eventos.Add(evento);
        _context.SaveChanges();
    }

    /// <summary>
    /// Metodo que deleta um evento 
    /// </summary>
    /// <param name="Id">Id do evento que será deletado</param>
    public void Deletar(Guid Id)
    {
        var eventoBuscado = _context.Eventos.Find(Id);

        if (eventoBuscado != null)
        {
            _context.Eventos.Remove(eventoBuscado);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Metodo que busca todos os eventos pelo mais recente
    /// </summary>
    /// <returns>retorna lista de eventos ordernada por data mais recente</returns>
    public List<Evento> Listar()
    {
        return _context.Eventos.OrderBy(Evento => Evento.DataEvento).ToList();
    }

    /// <summary>
    /// Metodo que busca eventos que um certo usuário participou
    /// </summary>
    /// <param name="IdUsuario">Id do usuário a ser buscado</param>
    /// <returns>Retorna lista de eventos que o usuário participou</returns>
    public List<Evento> ListarPorId(Guid IdUsuario)
    {
        return _context.Eventos.Include(e => e.IdTipoEventoNavigation).Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.Presencas.Any(p => p.IdUsuario == IdUsuario && p.Situacao == true)).ToList();
    }


    /// <summary>
    /// Metodo que traz a lista de proximos eventos
    /// </summary>
    /// <returns>Retorna lista de eventos</returns>
    public List<Evento> ProximosEventos()
    {
        return _context.Eventos.Include(e => e.IdTipoEventoNavigation).Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.DataEvento >= DateTime.Now)
            .OrderBy(e => e.DataEvento).ToList();
    }
}
