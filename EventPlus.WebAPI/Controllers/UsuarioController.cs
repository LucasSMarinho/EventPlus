using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{

    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

/// <summary>
/// Endpoint da API que faz a chamada para o metodo de Buscar um usuário por id
/// </summary>
/// <param name="id">Id do usuário a ser buscado</param>
/// <returns>Status Code 200 e o usuário buscado</returns>
[HttpGet("{id}")]
public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_usuarioRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

/// <summary>
/// Endpoint da API que faz a chamada para o metodo de cadastrar um usuário
/// </summary>
/// <param name="usuario">Dados do usuário que será criado</param>
/// <returns>Status Code 201 e o usuário cadastrado</returns>
[HttpPost]
public IActionResult Cadastrar(UsuarioDTO usuario)
    {
        try
        {
            var novoUsuario = new Usuario
            {
                Email = usuario.Email!,
                Nome = usuario.Nome!,
                Senha = usuario.Senha!,
                IdTipoUsuario = usuario.IdTipoUsuario
            };

            _usuarioRepository.Cadastrar(novoUsuario);
            return StatusCode(201, novoUsuario);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}