using AutoMapper;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using ResourceManagementSystem.Core.Models;
using ResourceManagementSystem.Application.DTOs;
using ResourceManagementSystem.Application.Interfaces.Base;

namespace ResourcesManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly UserManager<Staff> _userManager;

        private readonly SignInManager<Staff> _signInManager;

        private readonly ILogger<StaffController> _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IConfiguration _configuration;

        public StaffController(UserManager<Staff> userManager, SignInManager<Staff> signInManager, ILogger<StaffController> logger, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        #region API Calls
        [HttpGet("GetAllStaffs"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Staff>>> GetAllStaffs()
        {
            var staffs = await _unitOfWork.Staff.GetAll();

            if (staffs.Count() == 0)
            {
                return BadRequest("No any staffs have been added yet.");
            }

            return Ok(staffs.Select(staff => _mapper.Map<StaffDto>(staff)));
        }

        [HttpPost("RegisterStaff")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RegisterStaff(RegisterDto user)
        {
            var userExists = await _userManager.FindByEmailAsync(user.Email);

            if (userExists != null)
            {
                return BadRequest("Staff has already been registered to the system");
            }

            Staff staff = new Staff()
            {
                Name = user.Name,
                UserName = user.Name,
                EmailConfirmed = true,
                Email = user.Email,
                NormalizedEmail = user.Email.ToUpper(),
                NormalizedUserName = user.Name.ToUpper(),
                PhoneNumber = user.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(staff, user.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.ToString());
            }

            _logger.LogInformation("User created a new account with password.");

            return Ok("Staff registered successfully");
        }

        [HttpPost("LoginStaff")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LoginStaff(LoginDto user)
        {
            var userExists = await _userManager.FindByEmailAsync(user.Email);

            if (userExists != null && await _userManager.CheckPasswordAsync(userExists, user.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(userExists);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userExists.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(5),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    User = userExists.Name
                });
            }

            return Unauthorized("Login failed");

            //var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, lockoutOnFailure: true);

            //if (result.Succeeded)
            //{
            //    _logger.LogInformation("User logged in.");
            //    return Ok("User successfully logged in.");
            //}

            //return Unauthorized("Login failed");
        }
        #endregion
    }
}
