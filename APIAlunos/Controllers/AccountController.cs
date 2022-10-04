using AlunosAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace APIAlunos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticate _authentication;

        public AccountController(IConfiguration configuration, IAuthenticate autentication)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _authentication = autentication ?? throw new ArgumentNullException(nameof(autentication));
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] RegisterModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "As senhas não conferem");
                return BadRequest(ModelState);
            }

            var result = await _authentication.RegisterUser(model.Email, model.Password);

            if (result)
            {
                return Ok($"Usuario {model.Email} criado com sucesso");
            }
            else
            {
                ModelState.AddModelError("CreateUser", "Registro inválido.");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] LoginModel userInfo)
        {
            var result = await _authentication.Authenticate(userInfo.Email, userInfo.Password);

            if (result)
            {
                return GenerateToken(userInfo);
            }
            else
            {
                ModelState.AddModelError("LoginUser", "Login inválido!");
                return BadRequest(ModelState);
            }
        }

        private ActionResult<UserToken> GenerateToken(LoginModel userInfo)
        {
            var claims = new[]
            {
                new Claim("email", userInfo.Email),
                new Claim("meuToken", "token da Monica"),
                new Claim(JwtRegistededClaimNames.Jti, Guid.NewGuid().ToString());
            }

        var key = new SymmetricSecutiryKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]))

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);  //assinatura digital

        var expiration = DateTime.UtcNow.AddMinutes(20); //define que o token expirará em 20 min

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],  //vem do arquivo app.settings.json
            audience: _configuration["Jwt:Audience"], //vem do arquivo app.settings.json
            claims: claims,
            expires: expiration,
            signingCredentials: creds);

        return new UserToken()
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration: expiration,
        }
            }
    }
}
