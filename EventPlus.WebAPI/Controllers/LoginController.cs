using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public LoginController (IUsuarioRepository usuarioRepository)
    {
          _usuarioRepository = usuarioRepository;
    }


    [HttpPost]

    public IActionResult Login(LoginDTO loginDTO)
    {
        try
        {
            Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmailESenha(loginDTO.Email!, loginDTO.Senha!);

            if (usuarioBuscado == null)
            {
                return NotFound("Email ou senha invalidos!");
            }

            var claims = new[]
            {
               //Formato da claim
               new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
               new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Senha),
               new Claim("Tipo de Usuario", $"{usuarioBuscado.IdTipoUsuarioNavigation!.Titulo}")
            };

            //2° - Definir chave de acesso ao token
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("event-chave-autenticacao-webapi-dev"));

            //3° - Definir as credenciais do token (HEADER)

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //4° - Gerar o token

            var token = new JwtSecurityToken
            (
                //emissor do token
                issuer: "api_event",

                //destinatário do token
                audience: "api_event",

                //dados definidos nas claims
                claims: claims,

                //tempo de expiração do token
                expires: DateTime.Now.AddMinutes(5),

                //credenciais do token
                signingCredentials: creds

            );

            //5° retornar o token criado
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
