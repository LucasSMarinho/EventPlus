using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Tar;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoUsuarioController : ControllerBase
{
    private readonly ITipoUsuarioRepository _tipoUsuarioRepository;

    public TipoUsuarioController (ITipoUsuarioRepository tipoUsuarioRepository)
    {
        _tipoUsuarioRepository = tipoUsuarioRepository;
    }

    /// <summary>
    /// Endpoint da API que faz chamado ao metodo de Listar os tipos de usuário
    /// </summary>
    /// <returns>Retorna StatusCode 200 e a lista de tipos de usuários</returns>
    [Authorize]
    [HttpGet]

    public IActionResult Listar()
    {
        try
        {
            return Ok(_tipoUsuarioRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz chamado ao metodo de BuscarPorId do tipo de usuário
    /// </summary>
    /// <param name="id">Id do tipo de usuário buscado</param>
    /// <returns>Retorna StatusCode 200 e o tipo de usuário buscado</returns>
    [HttpGet("{id}")]

    public IActionResult BuscarPorId(Guid id) 
    {
        try
        {
            return Ok(_tipoUsuarioRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz chamado ao metodo de cadastrar um tipo de usuário
    /// </summary>
    /// <param name="tipoUsuario">Dados do novo usuario</param>
    /// <returns>Retorna StatusCode 201 e os o novo usuário</returns>
    [HttpPost]

    public IActionResult Cadastrar(TipoUsuarioDTO tipoUsuario)
    {
        try
        {
            var novoTipoUsuario = new TipoUsuario
            {
                Titulo = tipoUsuario.Titulo!
            };

            _tipoUsuarioRepository.Cadastrar(novoTipoUsuario);

            return StatusCode(201, novoTipoUsuario);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz chamado ao metodo de atualizar um tipo de usuário
    /// </summary>
    /// <param name="id">Id do usuário que será atualizado</param>
    /// <param name="tipoUsuario">Novos dados do usuário</param>
    /// <returns>Retorna StatusCode 200 e os dados atualizados do usuário </returns>
    [HttpPut("{id}")]

    public IActionResult Atualizar(Guid id, TipoUsuarioDTO tipoUsuario)
    {
        try
        {
            var novoTipoUsuario = new TipoUsuario
            {
                Titulo = tipoUsuario.Titulo!
            };

            _tipoUsuarioRepository.Atualizar(id, novoTipoUsuario);

            return Ok(_tipoUsuarioRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz chamado ao metodo de deletar um tipo usuário
    /// </summary>
    /// <param name="id">Id do tipo de usuário que será deletado</param>
    /// <returns>Retorna StatusCode 204</returns>
    [HttpDelete("{id}")]

    public IActionResult Deletar(Guid id)
    {
        try
        {
            _tipoUsuarioRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}