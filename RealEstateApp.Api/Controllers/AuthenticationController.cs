using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RealEstateApp.Api.DatabaseContext;
using RealEstateApp.Api.DTO.AuthDTO;
using RealEstateApp.Api.Entity;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEstateApp.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RealEstateContext _context;


        public AuthenticationController(UserManager<IdentityUser> userManager, IConfiguration configuration, RealEstateContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var realEstateUser = await _context.RealEstateUsers.SingleAsync(x => x.Email == user.Email);

                var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.UserName),
                    new("Id", realEstateUser.Id.ToString()),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    user = user.UserName,
                    roles = userRoles
                });
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "User already exists!" });
            }

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto
                    {
                        Status = "Error",
                        Message = "User creation failed! Please check user details and try again."
                    });
            }

            await _userManager.AddToRoleAsync(user, "User");

            RealEstateUser realEstateUser = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
            };
            _context.RealEstateUsers.Add(realEstateUser);
            await _context.SaveChangesAsync();

            return Ok(new ResponseDto { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequestDto model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                       new ResponseDto { Status = "Error", Message = "User already exists!" });
            }

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                       new ResponseDto
                                       {
                                           Status = "Error",
                                           Message = "User creation failed! Please check user details and try again."
                                       });
            }
            await _userManager.AddToRoleAsync(user, "Admin");
            await _userManager.AddToRoleAsync(user, "User");

            RealEstateUser realEstateUser = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
            };
            _context.RealEstateUsers.Add(realEstateUser);
            await _context.SaveChangesAsync();

            return Ok(new ResponseDto { Status = "Success", Message = "User created successfully!" });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            return new JwtSecurityToken(
                  issuer: _configuration["JWT:ValidIssuer"],
                  audience: _configuration["JWT:ValidAudience"],
                  expires: DateTime.Now.AddHours(3),
                  claims: authClaims,
                  signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
        }

    }
}
