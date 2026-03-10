using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IUsuarioRepository
{
    void Cadastrar(Usuario usuario);

    Usuario BuscarPorEmailESenha(string Email, string Senha);
    Usuario BuscarPorId(Guid id);
}
