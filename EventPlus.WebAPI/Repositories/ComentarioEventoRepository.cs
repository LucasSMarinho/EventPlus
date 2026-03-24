using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories
{
    public class ComentarioEventoRepository : IComentarioEventoRepository
    {
        private readonly EventContext _context;

        public ComentarioEventoRepository(EventContext context)
        {
            _context = context;
        }

        public ComentarioEvento BuscarPorIdUsuario(Guid IdUsuario, Guid IdEvento)
        {

            return _context.ComentarioEventos.Include(c => c.IdUsuarioNavigation).Include(c => c.IdEventoNavigation).FirstOrDefault(p => p.IdUsuario == IdUsuario && p.IdEvento == IdEvento )!;
        }

        public void Cadastrar(ComentarioEvento comentarioEvento)
        {
            _context.ComentarioEventos.Add(comentarioEvento);
            _context.SaveChanges();
        }

        public void Deletar(Guid id)
        {
            var Comentario = _context.ComentarioEventos.Find(id);
            if(Comentario != null)
            {
            _context.ComentarioEventos.Remove(Comentario);
            _context.SaveChanges();
            }
        }

        public List<ComentarioEvento> Listar(Guid IdEvento)
        {
           return _context.ComentarioEventos.OrderBy(p => p.IdEvento == IdEvento).ToList();
        }

        public List<ComentarioEvento> ListarSomenteExibe(Guid IdEvento)
        {
            return _context.ComentarioEventos
                .Include(c => c.IdUsuarioNavigation)
                .Include(c => c.IdEventoNavigation)
                .Where(c => c.Exibe == true && c.IdEvento == IdEvento)
                .ToList();
        }
    }
}
