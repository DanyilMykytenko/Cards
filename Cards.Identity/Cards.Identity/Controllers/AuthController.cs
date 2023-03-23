using Cards.Identity.Data;
using Cards.Identity.Models;
using IdentityModel;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Cards.Identity.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthDbContext _dbContext;
        private readonly IIdentityServerInteractionService _interactionService;

        public AuthController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IIdentityServerInteractionService interactionService,
            AuthDbContext context) =>
            (_signInManager, _userManager, _interactionService, _roleManager, _dbContext) =
            (signInManager, userManager, interactionService, roleManager, context);

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var viewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await _userManager.FindByNameAsync(viewModel.Username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View(viewModel);
            }

            var userRole = await _dbContext.UserRoles.FirstOrDefaultAsync(u => u.UserId == user.Id);
            if(userRole == null)
            {
                ModelState.AddModelError(string.Empty, "RoleRef not found");
                return View(viewModel);
            }

            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == userRole.RoleId);
            if (role == null)
            {
                ModelState.AddModelError(string.Empty, "Role not found");
                return View(viewModel);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, viewModel.Password, false); 
            if (result.Succeeded)
            {
                await _signInManager.SignInWithClaimsAsync(user, false, new List<Claim> { new Claim(JwtClaimTypes.Role, role.Name) });
                return Redirect(viewModel.ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, "Login error");
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            var viewModel = new RegisterViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = new AppUser
            {
                UserName = viewModel.Username
            };


            var result = await _userManager.CreateAsync(user, viewModel.Password);
            result = await _userManager.AddToRoleAsync(user, "User");
            if (result.Succeeded)
            {
                var userRole = await _dbContext.UserRoles.FirstOrDefaultAsync(u => u.UserId == user.Id);
                if (userRole == null)
                {
                    ModelState.AddModelError(string.Empty, "RoleRef not found");
                    return View(viewModel);
                }

                var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == userRole.RoleId);
                if (role == null)
                {
                    ModelState.AddModelError(string.Empty, "Role not found");
                    return View(viewModel);
                }


                //await _signInManager.SignInAsync(user, false);
                await _signInManager.SignInWithClaimsAsync(user, false, new List<Claim> { new Claim(JwtClaimTypes.Role, role.Name) });
                return Redirect(viewModel.ReturnUrl);
            }
            ModelState.AddModelError(string.Empty, "Error occurred");
            return View(viewModel);
        }
        
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
    }
}
