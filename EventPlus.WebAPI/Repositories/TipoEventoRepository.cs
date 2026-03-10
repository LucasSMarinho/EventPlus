using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories
{
    public class TipoEventoRepository : ITipoEventoRepository
    {
        private readonly EventContext _context;

        //injeção de dependencia

        public TipoEventoRepository(EventContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Atualiza um tipo de evento usando o rastreamento automatico
        /// </summary>
        /// <param name="id">Id do Evento a ser atualizado</param>
        /// <param name="tipoEvento"> Novos dados do tipo evento</param>


        public void Atualizar(Guid id, TipoEvento tipoEvento)
        {
            var tipoEventoBuscado = _context.TipoEventos.Find(id);

            if(tipoEventoBuscado != null)
            {
                tipoEventoBuscado.Titulo = tipoEvento.Titulo;
                
                //SaveChanges() detecta a mudança da propriedade titulo automaticamente
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Busca um tipo de evento por id
        /// </summary>
        /// <param name="id"> id do tipo evento que será buscado</param>
        /// <returns> objeto do tipoEvento com as informações do tipo de evento buscado</returns>
        public TipoEvento BuscarPorId(Guid id)
        {
            return _context.TipoEventos.Find(id)!;
        }

        /// <summary>
        /// Cadastra um novo tipo evento
        /// </summary>
        /// <param name="tipoEvento">tipo evento a ser cadastrado</param>
        public void Cadastrar(TipoEvento tipoEvento)
        {
            _context.TipoEventos.Add(tipoEvento);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deleta um tipo de evento
        /// </summary>
        /// <param name="id">id do Objeto que será deletado</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Deletar(Guid id)
        {
            var tipoEventoBuscado = _context.TipoEventos.Find(id);

            if(tipoEventoBuscado != null)
            {
                _context.TipoEventos.Remove(tipoEventoBuscado);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Lista todos os tipos de evento
        /// </summary>
        /// <returns>Retorna os tipos de eventos criados</returns>
        public List<TipoEvento> Listar()
        {
            return _context.TipoEventos.OrderBy(tipoEvento => tipoEvento.Titulo).ToList();
        }
    }
}
