using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class PresencaRepository : IPresencaRepository
{
    private readonly EventContext _eventContext;

    public PresencaRepository(EventContext eventContext)
    {
        _eventContext = eventContext; 
    }

    /// <summary>
    /// Atualiza uma presenca
    /// </summary>
    /// <param name="Id">Id da presença a ser atualizada</param>
    /// <param name="presenca">dados da nova presenca</param>
    public void Atualizar(Guid Id, Presenca presenca)
    {
        var presencaBuscada = _eventContext.Presencas.Find(Id);

        if (presencaBuscada != null)
        {
            presencaBuscada.Situacao = !presenca.Situacao;
            presencaBuscada.IdUsuario = presenca.IdUsuario;
            presencaBuscada.IdEvento = presenca.IdEvento;

            _eventContext.SaveChanges();
        }
    }


    /// <summary>
    /// Busca uma presença por Id
    /// </summary>
    /// <param name="Id">Id da presença a ser buscada</param>
    /// <returns>presença buscada</returns>
    public Presenca BuscarPorId(Guid Id)
    {
        return _eventContext.Presencas.Include(p => p.IdEventoNavigation).ThenInclude(e => e!.IdInstituicaoNavigation).FirstOrDefault(p => p.IdPresenca == Id)!;
    }

    /// <summary>
    /// Deleta uma presenca
    /// </summary>
    /// <param name="Id">Id da presenca que será deletada</param>
    public void Deletar(Guid Id)
    {
        var presencaBuscada = _eventContext.Presencas.Find(Id);

        if (presencaBuscada != null)
        {
            _eventContext.Presencas.Remove(presencaBuscada);
            _eventContext.SaveChanges();
        }
    }

    /// <summary>
    /// Inscreve uma presença
    /// </summary>
    /// <param name="Inscricao">Presença a ser inscrita</param>
    public void Inscrever(Presenca Inscricao)
    {
        _eventContext.Presencas.Add(Inscricao);
        _eventContext.SaveChanges();
    }

    /// <summary>
    /// Lista todas as presenças
    /// </summary>
    /// <returns>Retorna uma lista de presenças</returns>
    public List<Presenca> Listar()
    {
        return _eventContext.Presencas.ToList();
    }

    /// <summary>
    /// Lista as presenças de um usuário especifico
    /// </summary>
    /// <param name="IdUsuario">Id do usuário para filtragem</param>
    /// <returns>Uma lista de presenças</returns>
    public List<Presenca> ListarMinhas(Guid IdUsuario)
    {
        return _eventContext.Presencas.Include(p => p.IdEventoNavigation).ThenInclude(e => e!.IdInstituicaoNavigation).Where(p => p.IdUsuario == IdUsuario).ToList();
    }
}
