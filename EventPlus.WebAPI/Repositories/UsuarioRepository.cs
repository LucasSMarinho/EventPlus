using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly EventContext _context;

        //Metodo Construtor
        public UsuarioRepository(EventContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Busca usuario pelo e-mail e valida o hash da senha
        /// </summary>
        /// <param name="Email">Email do usuário a ser buscado</param>
        /// <param name="Senha">Senha para validar o usuário</param>
        /// <returns>Usuário buscado</returns>
        public Usuario BuscarPorEmailESenha(string Email, string Senha)
        {
            //primeiro buscamos o usuario pelo email
            var usuarioBuscado = _context.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation).FirstOrDefault(usuario => usuario.Email == Email);

            //Verificamos se o usuario foi encontrado
            if(usuarioBuscado != null)
            {
                //Comparamos o Hash da senha digitada com o que está no banco
                bool confere = Criptografia.CompararHash(Senha, usuarioBuscado.Senha);

                if(confere)
                {
                    return usuarioBuscado;
                }
            }

            return null!;
        }

        /// <summary>
        /// Procura um usuário no banco de dados atrávez do id incluindo o seu tipo de usuário
        /// </summary>
        /// <param name="id">id do usuário a ser buscado</param>
        /// <returns>Usuário buscado e seu tipo de usuário</returns>
        public Usuario BuscarPorId(Guid id)
        {
            return _context.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation).FirstOrDefault(usuario => usuario.IdUsuario == id)!;
        }


        /// <summary>
        /// Cadastra um novo usuario no banco de dados com uma senha criptografada
        /// </summary>
        /// <param name="usuario">Dados do novo usuario</param>
        public void Cadastrar(Usuario usuario)
        {
            usuario.Senha = Criptografia.GerarHash(usuario.Senha);
            
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }
    }
}
