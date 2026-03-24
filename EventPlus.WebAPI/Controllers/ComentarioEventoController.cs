using Azure;
using Azure.AI.ContentSafety;
using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioEventoController : ControllerBase
    {
        private readonly ContentSafetyClient _contentSafetyClient;

        private readonly IComentarioEventoRepository _comentarioEventoRepository;

        public ComentarioEventoController(ContentSafetyClient contentSafetyClient, IComentarioEventoRepository comentarioEventoRepository)
        {
            _contentSafetyClient = contentSafetyClient;
            _comentarioEventoRepository = comentarioEventoRepository;
        }
       

        /// <summary>
        /// Endpoint da API que faz chamado ao metodo de buscar um comentario
        /// </summary>
        /// <param name="IdEvento">Id do evento do comentario</param>
        /// <returns>Retorna uma lista de comentarios de um evento especifico</returns>
        [HttpGet("{IdEvento}")]
        public IActionResult Get(Guid IdEvento)
        {
            try
            {
                return Ok(_comentarioEventoRepository.Listar(IdEvento));
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpGet("ExibeSomente{IdEvento}")]
        public IActionResult SomenteExibe(Guid IdEvento)
        {
            try
            {
                return Ok(_comentarioEventoRepository.ListarSomenteExibe(IdEvento));
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint da API que faz chamado ao metodo de Buscar comentarios por Id do usuário
        /// </summary>
        /// <param name="IdUsuario">Id do usuário que fez o comentario</param>
        /// <param name="IdEvento">Id do evento que os comentários estão</param>
        /// <returns></returns>
        [HttpGet("BuscarPorUsuario{IdEvento}")]
        public IActionResult BuscarPorUsuario(Guid IdUsuario, Guid IdEvento)
        {
            try
            {
                return Ok(_comentarioEventoRepository.BuscarPorIdUsuario(IdUsuario, IdEvento));
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint da API que faz chamado ao metodo de cadastrar um comentario
        /// </summary>
        /// <param name="comentarioEvento">Dados do comentario</param>
        /// <returns>sStatus Code 201, novoComentario</returns>
        [HttpPost]
        public async Task<IActionResult> Cadastrar(ComentarioEventoDTO comentarioEvento)
        {
            try
            {
                if(string.IsNullOrEmpty(comentarioEvento.Descricao))
                {
                    return BadRequest("O texto a ser moderado não pode ser vazio");
                }

                //Criar objeto de analise
                var request = new AnalyzeTextOptions(comentarioEvento.Descricao);
                //Chamar a API da Azure
                Response<AnalyzeTextResult> response = await _contentSafetyClient.AnalyzeTextAsync(request);
                //Verificar se o texto tem severidade maior que 0
                bool temConteudoImproprio = response.Value.CategoriesAnalysis.Any(comentario => comentario.Severity > 0);

                var novoComentario = new ComentarioEvento
                {
                    Descricao = comentarioEvento.Descricao,
                    IdUsuario = comentarioEvento.IdUsuario,
                    IdEvento = comentarioEvento.IdEvento,
                    DataComentarioEvento = DateTime.Now,
                    //Verificar se o texto tem linguagem impropria
                    Exibe = !temConteudoImproprio
                };

                //Cadastrar comentario
                _comentarioEventoRepository.Cadastrar(novoComentario);

                return StatusCode(201, novoComentario);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _comentarioEventoRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}
