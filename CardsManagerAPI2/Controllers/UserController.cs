using CardsManagerAPI2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CardsManagerAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<ApplicationUser> userManager;
        private readonly ApplicationSettings appSettings;

        public UserController(UserManager<ApplicationUser> userManager, IOptions<ApplicationSettings> appSettings)
        {
            this.userManager = userManager;
            this.appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/User/Register
        public async Task<Object> RegisterUser(ApplicationUserModel model)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };

            try
            {
                var result = await this.userManager.CreateAsync(applicationUser, model.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("Login")]
        //POST : /api/User/Login
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await this.userManager.FindByEmailAsync(model.Email);
  
            if (user != null && await this.userManager.CheckPasswordAsync(user, model.Password))
            {
                /**
              var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.appSettings.JWT_Secret));
              var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

              var claims = new[] {
                  new Claim("UserID", user.Id.ToString())
              };

              var token = new JwtSecurityToken(this.appSettings.Client_URL,
                  this.appSettings.Client_URL,
                  claims,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);
              new JwtSecurityTokenHandler().WriteToken(token);

              return Ok(new { token });
          **/

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });
        }
    }
}
