using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoTimelyAPI;
using Microsoft.AspNetCore.Cors;


namespace GoTimelyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserController> _logger;

        public UserController(AppDbContext context, ILogger<UserController> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation("UserController initialized.");
        }

       

        // OPTIONS endpoint specifically for /login
        [HttpOptions("login")]
        public IActionResult PreflightLoginRoute()
        {
            return NoContent();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Email is already registered.");

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                UserID = 0,
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                PasswordHash = passwordHash
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("User registered: {UserName}", user.Name);
            return Ok(new 
            {
                message = "User registered successfully",
                userId = user.UserID
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
            {
                _logger.LogWarning("Login failed: User not found for email {Email}", dto.Email);
                return NotFound("User not found.");
            }

            bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!isValid)
            {
                _logger.LogWarning("Login failed: Invalid password for email {Email}", dto.Email);
                return Unauthorized("Invalid password.");
            }

            _logger.LogInformation("Login successful for user {UserName}", user.Name);
            return Ok(new
            {
                message = "Login successful",
                userId = user.UserID,
                name = user.Name,
                email = user.Email
            });
        }
    }

    public class UserRegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}