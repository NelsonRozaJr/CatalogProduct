using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CatalogProduct.Api.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CatalogProduct.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly IConfiguration _configuration;

        public LoginController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserDto model)
        {
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true // evita que o e-mail necessite ser confirmado antes do login
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                var token = CreateToken(model);
                return Ok(token);
            }
            else
            {
                return BadRequest(result.Errors);
            }            
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                var token = CreateToken(model);
                return Ok(token);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login inválido!");
                return BadRequest(ModelState);
            }
        }

        private UserToken CreateToken(UserDto model)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var configKey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var configIssuer = _configuration["TokenConfigurations:Issuer"];
            var configAudience = _configuration["TokenConfigurations:Audience"];
            var configExpiration = double.Parse(_configuration["TokenConfigurations:ExpirationHours"]);

            var key = new SymmetricSecurityKey(configKey);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha384);
            var expiration = DateTime.UtcNow.AddHours(configExpiration);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configIssuer,
                audience: configAudience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new UserToken
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "Jwt created successfully"
            };
        }
    }
}