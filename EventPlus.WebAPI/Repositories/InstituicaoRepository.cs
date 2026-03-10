using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class InstituicaoRepository : IInstituicaoRepository
{

    private readonly EventContext _context;

    //injeção de dependencia

    public InstituicaoRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Atualiza uma instituição atráves do id
    /// </summary>
    /// <param name="id">id da instuição que será atualizada</param>
    /// <param name="instituicao">Novos dados da instituição</param>
    public void Atualizar(Guid id, Instituicao instituicao)
    {
        var instituicaoBuscada = _context.Instituicaos.Find(id);

        if (instituicaoBuscada != null)
        {
            instituicaoBuscada.NomeFantasia = instituicao.NomeFantasia;
            instituicaoBuscada.Cnpj = instituicao.Cnpj;
            instituicaoBuscada.Endereco = instituicao.Endereco;

            _context.SaveChanges(); 
        }
    }

    /// <summary>
    /// Busca uma instituição por id
    /// </summary>
    /// <param name="id">Id da instituição que queremos atualizar</param>
    /// <returns>retorna a instituição buscada</returns>
    public Instituicao BuscarPorId(Guid id)
    {
        return _context.Instituicaos.Find(id)!;
    }

    /// <summary>
    /// Cadastra uma instituição nova
    /// </summary>
    /// <param name="instituicao">Dados da nova instituição</param>
    public void Cadastrar(Instituicao instituicao)
    {
        _context.Instituicaos.Add(instituicao);
        _context.SaveChanges();
    }

    /// <summary>
    /// DEleta uma instituição
    /// </summary>
    /// <param name="id">Id da instituição que será deletada</param>
    public void Deletar(Guid id)
    {
        var instituiçaoBuscada = _context.Instituicaos.Find(id);

        if (instituiçaoBuscada != null)
        {
            _context.Instituicaos.Remove(instituiçaoBuscada);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Lista todas as instituições
    /// </summary>
    /// <returns>Retorna todas s instituições do banco</returns>
    public List<Instituicao> Listar()
    {
        return _context.Instituicaos.OrderBy(instituicao => instituicao.NomeFantasia).ToList();
    }
}
