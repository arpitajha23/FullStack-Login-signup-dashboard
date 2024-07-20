using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext dbContext;

        public UsersController(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] RegistrtionDTO registrtionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await dbContext.Arpitas.FirstOrDefaultAsync(x => x.Email == registrtionDTO.Email);
            if (existingUser != null)
            {
                return BadRequest("User already exists with the same email address.");
            }

            dbContext.Arpitas.Add(new Arpita
            {
                Name = registrtionDTO.Name,
                Email = registrtionDTO.Email,
                PhoneNumber = registrtionDTO.PhoneNumber,
                Password = registrtionDTO.Password,
                Role = registrtionDTO.Role,
            });
            await dbContext.SaveChangesAsync();

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            var user = dbContext.Arpitas.FirstOrDefault(x => x.Email == loginDTO.Email && x.Password == loginDTO.Password);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound("Invalid email or password");
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(dbContext.Arpitas.ToList());
        }

        [HttpGet("GetUser")]
        public IActionResult GetUser(int id)
        {
            var user = dbContext.Arpitas.FirstOrDefault(x => x.UserId == id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound("User not found");
        }

        [HttpGet("AdminCount")]
        public IActionResult GetAdminCount()
        {
            var count = dbContext.Arpitas.Count(x => x.Role == "Admin");
            return Ok(count);
        }

        [HttpGet("Filter")]
        public IActionResult FilterUsers([FromQuery] string name, [FromQuery] string role)
        {
            var users = dbContext.Arpitas
                .Where(u => (string.IsNullOrEmpty(name) || u.Name.Contains(name)) &&
                            (string.IsNullOrEmpty(role) || u.Role == role))
                .ToList();
            return Ok(users);
        }

        [HttpGet("Search")]
        public IActionResult SearchUsers([FromQuery] string searchTerm)
        {
            var users = dbContext.Arpitas
                .Where(u => u.Name.Contains(searchTerm) ||
                            u.Email.Contains(searchTerm) ||
                            u.Role.Contains(searchTerm))
                .ToList();
            return Ok(users);
        }

        [HttpGet("Sort")]
        public IActionResult SortUsers([FromQuery] string sortBy)
        {
            IQueryable<Arpita> users = dbContext.Arpitas;

            switch (sortBy.ToLower())
            {
                case "name":
                    users = users.OrderBy(u => u.Name);
                    break;
                case "email":
                    users = users.OrderBy(u => u.Email);
                    break;
                case "role":
                    users = users.OrderBy(u => u.Role);
                    break;
                default:
                    users = users.OrderBy(u => u.Name);
                    break;
            }

            return Ok(users.ToList());

            /*
                        [HttpPost("forgot-password")]
                        public IActionResult ForgotPassword(ForgotPasswordRequest request)
                        {
                            var user = dbContext.Arpitas.FirstOrDefault(x => x.Email == request.Email);
                            if (user == null)
                            {
                                return NotFound("User not found");
                            }

                            return Ok("Password reset instructions sent to user's email.");
                        }

                        [HttpPost("change-password")]
                        public IActionResult ChangePassword(ChangePasswordRequest request)
                        {
                            var user = dbContext.Arpitas.FirstOrDefault(x => x.UserId == request.UserId);
                            if (user == null)
                            {
                                return NotFound("User not found");
                            }

                            // In a real scenario, validate current password and hash the new password
                            user.Password = request.NewPassword;

                            dbContext.SaveChanges();

                            return Ok("Password changed successfully.");
                        }
                    }*/
            /* public class ForgotPasswordRequest
             {
                 public string Email { get; set; }= string.Empty;
             }

             public class ChangePasswordRequest
             {
                 public int UserId { get; set; }
                 public string NewPassword { get; set; }=string.Empty;
             }*/
        }
    }
}
