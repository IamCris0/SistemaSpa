using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaSpaWeb.Models;
using System.Security.Claims;

namespace SistemaSpaWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Account/Login
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.NombreUsuario == model.NombreUsuario && u.Password == model.Password);

                if (usuario != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioID.ToString()),
                        new Claim(ClaimTypes.Name, usuario.NombreUsuario)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RecordarMe,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(model.RecordarMe ? 24 : 8)
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos");
            }

            return View(model);
        }

        // GET: Account/Register
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el usuario ya existe
                var existeUsuario = await _context.Usuarios
                    .AnyAsync(u => u.NombreUsuario == model.NombreUsuario);

                if (existeUsuario)
                {
                    ModelState.AddModelError("NombreUsuario", "Este nombre de usuario ya está en uso");
                    return View(model);
                }

                var usuario = new Usuario
                {
                    NombreUsuario = model.NombreUsuario,
                    Password = model.Password // En producción, usar hash
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Registro exitoso. Por favor, inicie sesión";
                return RedirectToAction(nameof(Login));
            }

            return View(model);
        }

        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        // GET: Account/AccessDenied
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}