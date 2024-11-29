using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Grpc.Core;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using UniquenessCheckELMA.Application.DTOs;

namespace UniquenessCheckELMA.Application.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Метод принимает учетные данные пользователя и возвращает токен доступа. Реализовано неполностю.
        /// </summary>
        /// <param name="credentials">Учетные данные пользователя для аутентификации в системе.</param> 
        /// <returns></returns>
        [HttpPost]
        public IActionResult Authenticate(Credentials credentials)
        {
            if (credentials.Login == "admin" && credentials.Password == "admin")
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, credentials.Login)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new ArgumentNullException("Ключ для создания токена не найден.")));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                    _configuration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }

            return StatusCode(StatusCodes.Status401Unauthorized);
        }
    }
}
