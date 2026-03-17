using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EventPlus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituicaoController : ControllerBase
    {
        private readonly IInstituicaoRepository _instituicaoRepository;

        public InstituicaoController(IInstituicaoRepository instituicaoRepository)
        {
            _instituicaoRepository = instituicaoRepository;
        }

        /// <summary>
        /// Endpoint da API que faz chamado ao metodo Listar da instituição
        /// </summary>
        /// <returns>Retorna StatusCode 200 e lista das instituições</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_instituicaoRepository.Listar());
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint da API que faz chamado ao metodo ObterPorId da instituição
        /// </summary>
        /// <param name="id">Id da instituição procurada</param>
        /// <returns>Retorna StatusCode 200 e os dados da instituição buscada</returns>
        [HttpGet("{id}")]

        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                return Ok(_instituicaoRepository.BuscarPorId(id));
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint da API que faz chamado ao metodo de cadastrar uma instituição
        /// </summary>
        /// <param name="instituicao">Dados da nova instituição</param>
        /// <returns>retorna Status Code 201 e os dados da nova instituição</returns>
        [HttpPost]

        public IActionResult Cadastrar(InstituicaoDTO instituicao)
        {
            try
            {
                var novaInstituicao = new Instituicao
                {
                    NomeFantasia = instituicao.NomeFantasia!,
                    Cnpj = instituicao.Cnpj!,
                    Endereco = instituicao.Endereco!
                };

                _instituicaoRepository.Cadastrar(novaInstituicao);
                return StatusCode(201, novaInstituicao);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint da API que faz chamado ao metodo de atualizar uma instituição
        /// </summary>
        /// <param name="id">Id da instuição que será atualizada</param>
        /// <param name="instituicao">Novos dados da instituição</param>
        /// <returns>Retorna Status Code 201 e os dados da instituição</returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, InstituicaoDTOAtualizar instituicao)
        {
            try
            {
                var novaInstituicao = new Instituicao
                {
                    NomeFantasia = instituicao.NomeFantasia!,
                    Cnpj = instituicao.Cnpj!,
                    Endereco = instituicao.Endereco!
                };

                _instituicaoRepository.Atualizar(id, novaInstituicao);
                return Ok(_instituicaoRepository.BuscarPorId(id));
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint da API que faz chamado ao metodo de deletar uma instituição
        /// </summary>
        /// <param name="id">Id da instituição que será deletada</param>
        /// <returns>Retorna Status Code 204</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(Guid id)
        {
            try
            {
                _instituicaoRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}
