
using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoController(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        /// <summary>
        /// Endpoint da API que faz a chamada de listar por eventos filtrado por Id de Usuário
        /// </summary>
        /// <param name="IdUsuario">Id do Usuário para a filtragem</param>
        /// <returns>Status code 200 e uma lista de eventos</returns>
        [HttpGet("Usuario/{IdUsuario}")]
        public IActionResult ListarPorID(Guid IdUsuario)
        {
            try
            {
                return Ok(_eventoRepository.ListarPorId(IdUsuario));
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }


        /// <summary>
        /// Endpoint da API que faz a chamada para o metodo de listar os proximos eventos
        /// </summary>
        /// <returns>Status Code 200 e a lista dos proximos eventos</returns>
        [HttpGet("Listar Proximos")]

        public IActionResult BuscarProximosEventos()
        {
            {
                try
                {
                    return Ok(_eventoRepository.ProximosEventos());
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
        }

        /// <summary>
        /// Endpoint da API que faz chamado ao metodo de listar os eventos
        /// </summary>
        /// <returns>Retorna status code 200 a lista de eventos</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_eventoRepository.Listar());
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint da API que faz chamado ao metodo de buscar eventos por id
        /// </summary>
        /// <param name="id">Id do evento buscado</param>
        /// <returns>Retorna status code 200 e o evento buscado</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                return Ok(_eventoRepository.BuscarPorId(id));
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint da API que faz chamado ao metodo de cadastrar os eventos
        /// </summary>
        /// <param name="evento">Dados do novo evento</param>
        /// <returns>Retorna Status Code 201 e os dados do novo evento</returns>
        [HttpPost]
        public IActionResult Cadastrar(EventoDTO evento)
        {
            try
            {
                var novoEvento = new Evento
                {
                    Nome = evento.Nome!,
                    Descricao = evento.Descricao!,
                    IdTipoEvento = evento.IdTipoEvento,
                    IdInstituicao = evento.IdIntituicao,
                    DataEvento = evento.DataEvento
                };

                _eventoRepository.Cadastrar(novoEvento);
                return StatusCode(201, novoEvento);

            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint da API que faz chamado ao metodo de atualizar evento
        /// </summary>
        /// <param name="id">Id do evento que será atualizado</param>
        /// <param name="evento">Dados do evento atualizado</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, EventoDTOAtualizar evento)
        {
            try
            {
                var eventoAtualizado = new Evento
                {
                    Nome = evento.Nome!,
                    Descricao = evento.Descricao!,
                    IdTipoEvento = evento.IdTipoEvento,
                    IdInstituicao = evento.IdIntituicao,
                    DataEvento = evento.DataEvento
                };

                _eventoRepository.Atualizar(id, eventoAtualizado);
                return Ok(_eventoRepository.BuscarPorId(id));

            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint da API que faz chamado ao metodo de deletar evento
        /// </summary>
        /// <param name="id">Id do evento que será deletado</param>
        /// <returns>retorna status code 204</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(Guid id)
        {
            try
            {
                _eventoRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}
