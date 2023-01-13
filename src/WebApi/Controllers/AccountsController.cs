using Core;
using Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webapi.Controllers;
using WebApi.ViewModels.Identity;

namespace WebApi.Controllers {
    public class AccountsController : ApiController {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public AccountsController(IConfiguration configuration, 
                                  UserManager<User> userManager) {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignupViewModel model) {
            if (!ModelState.IsValid) {
                return BadRequest("Invalid model");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user.IsNotNull()) {
                return BadRequest("Existing user");
            }

            user = new User() {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded) {
                await _userManager.AddToRoleAsync(user, "Viewer");
                return Created("", user.Email);
            }

            // TODO: not sure what to return here (is e.Description exposing implementation details?)
            return BadRequest(result.Errors.Select(e => e.Description).Aggregate((d1, d2) => $"{d1}\n{d2}"));
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(LoginViewModel model) {
            if (!ModelState.IsValid) {
                return BadRequest("Invalid model");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user.IsNull()) {
                return BadRequest("Invalid credentials");
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.Password);
            if (isPasswordCorrect) {
                return Ok(await GetJwtTokenAsync(user));
            }

            return BadRequest("Invalid credentials");
        }

        [Authorize]
        [HttpPost("sign-out")]
        public new IActionResult SignOut() {
            // Here we will add the provided token to a blacklist in database
            return Ok();
        }

        [Authorize]
        [HttpGet("refresh-token")]
        public async Task<IActionResult> RefreshToken() {
            var userId = User.Claims.Single(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
            var user = await _userManager.FindByIdAsync(userId);

            return Ok(await GetJwtTokenAsync(user));
        }

        private async Task<string> GetJwtTokenAsync(User user) {
            var issuer = AppSettings.JwtToken.Issuer;
            var audience = AppSettings.JwtToken.Audience;
            var securityKey = AppSettings.JwtToken.SecurityKey;

            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.Jti, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, string.Join(", ", await _userManager.GetRolesAsync(user)))
            };

            var keyBytes = Encoding.UTF8.GetBytes(securityKey);
            var symmetricKey = new SymmetricSecurityKey(keyBytes);
            var creds = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.Now.AddMinutes(20), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
