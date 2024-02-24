using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventsTN.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        // GET: api/Users
        [HttpGet("get-all-users")]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            return Ok(users);
        }
        // GET: api/Users/5
        [HttpGet("get-user/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        [HttpPost("set-role")]
        public async Task<IActionResult> SetRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // Remove existing roles
                var existingRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, existingRoles);

                // Add the selected role
                await _userManager.AddToRoleAsync(user, role);
            }

            return Ok("Role is set successfully");
        }
    }
}
