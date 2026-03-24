using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresencaController : ControllerBase
    {
        private IPresencaRepository _presencaRepository;

        public PresencaController(IPresencaRepository presencaRepository)
        {
            _presencaRepository = presencaRepository;
        }

        /// <summary>
        /// Endpoint da API que faz chamado ao metodo que busca presença por Id
        /// </summary>
        /// <param name="Id">Id da presença buscada</param>
        /// <returns>Retorna a presença buscada</returns>
        [HttpGet("{id}")]
        public IActionResult BuscaPorId(Guid Id)
        {
            try
            {
                return Ok(_presencaRepository.BuscarPorId(Id));
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint da API que retorna uma lista de presença filtrada pelo id do usuário
        /// </summary>
        /// <param name="idUsuario">Id do usuário que terá as presenças buscadas</param>
        /// <returns>Lista de presenças filtrada por id </returns>
        [HttpGet("ListarMinhas/{idUsuario}")]

        public IActionResult BuscadPorUsuario(Guid idUsuario)
        {
            try
            {
                return Ok(_presencaRepository.ListarMinhas(idUsuario));
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint da API que faz chamado ao metodo de listar
        /// </summary>
        /// <returns>Retorna uma lista de presença</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_presencaRepository.Listar());
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPost]
        public IActionResult Inscrever(PresencaDTO presenca)
        {
            try
            {
                var novaPresenca = new Presenca
                {
                    Situacao = presenca.Situacao,
                    IdUsuario = presenca.IdUsuario,
                    IdEvento = presenca.IdEvento,
                };
                _presencaRepository.Inscrever(novaPresenca);
                return StatusCode(201, novaPresenca);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }

        }

        /// <summary>
        /// Endpoint da API que faz chamado ao metodo de atualizar uma presença
        /// </summary>
        /// <param name="Id">Id da presença atualizada</param>
        /// <param name="presenca">Novos dados da presença</param>
        /// <returns>retorna a presença atualizada</returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid Id, PresencaDTOAtualizar presenca)
        {
            try
            {
                var novaPresenca = new Presenca
                {
                    Situacao = presenca.Situacao,
                    IdUsuario = presenca.IdUsuario,
                    IdEvento = presenca.IdEvento,
                };
                _presencaRepository.Atualizar(Id, novaPresenca);
                return Ok(_presencaRepository.Listar());
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint da API que faz chamado ao metodo de deletar uma presença
        /// </summary>
        /// <param name="Id">Id da presença que será deletada</param>
        /// <returns>Retorna status code 204</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(Guid Id)
        {
            try
            {
                _presencaRepository.Deletar(Id);
                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}
