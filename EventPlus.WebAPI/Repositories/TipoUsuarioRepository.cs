using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private readonly EventContext _context;

        public TipoUsuarioRepository(EventContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Atualiza um tipo de usuário atrávez do id
        /// </summary>
        /// <param name="id">Id do tipo usuário que queremos atualizar</param>
        /// <param name="tipoUsuario">Novos dados do tipo usuário</param>
        public void Atualizar(Guid id, TipoUsuario tipoUsuario)
        {
            var tipoUsuarioBuscado = _context.TipoUsuarios.Find(id);

            if (tipoUsuarioBuscado != null)
            {
                tipoUsuarioBuscado.Titulo = tipoUsuario.Titulo;
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Busca um tipo usuário especifico atrávez do id
        /// </summary>
        /// <param name="id">id do tipo de usuário que procuramos</param>
        /// <returns>Os dados do tipo usuário</returns>
        public TipoUsuario BuscarPorId(Guid id)
        {
            return _context.TipoUsuarios.Find(id)!;
        }

        /// <summary>
        /// Cadastra um novo tipo de usuário
        /// </summary>
        /// <param name="tipoUsuario">Informações do tipo de usuário que querermos adicionar</param>
        public void Cadastrar(TipoUsuario tipoUsuario)
        {
            _context.TipoUsuarios.Add(tipoUsuario);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deleta um tipo de usuário
        /// </summary>
        /// <param name="id">Id do tipo de usuário que queremos deletar</param>
        public void Deletar(Guid id)
        {
            var tipoUsuarioBuscado = _context.TipoUsuarios.Find(id);

            if (tipoUsuarioBuscado != null)
            {
                _context.TipoUsuarios.Remove(tipoUsuarioBuscado);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Lista todos os tipos de usuário do banco
        /// </summary>
        /// <returns>retorna os tipos de usuário do banco</returns>
        public List<TipoUsuario> Listar()
        {
             return _context.TipoUsuarios.OrderBy(tipoUsuario => tipoUsuario.Titulo).ToList();
        }
    }

}
