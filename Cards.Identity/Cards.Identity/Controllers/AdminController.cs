using Cards.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cards.Identity.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager) =>
            (_userManager, _roleManager) = (userManager, roleManager);

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userManager.Users;
            return View(new UsersModel
            {
                Users = users.ToList()
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(string username)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = _roleManager.Roles;
            return View(new AdminRolesModel
            {
                User = user,
                Roles = roles.ToList(),
                UserRoles = userRoles.ToList()
            });
        }
    }
}
