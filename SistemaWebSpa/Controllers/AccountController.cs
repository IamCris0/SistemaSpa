using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaWebMVC.Models.ViewModels;
using SpaWebMVC.Services;
using System.Security.Claims;

namespace SpaWebMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;

        public AccountController(AuthService authService)
        {
            _authService = authService;
        }

        // GET: Account/Login
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return View(model);

            var usuario = await _authService.AutenticarUsuario(model.Usuario, model.Password);

            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos");
                return View(model);
            }

            // Crear claims del usuario
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioID.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim("NombreCompleto", usuario.NombreCompleto),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.Recordarme,
                ExpiresUtc = model.Recordarme ? DateTimeOffset.UtcNow.AddDays(30) : DateTimeOffset.UtcNow.AddHours(8)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            TempData["SuccessMessage"] = $"¡Bienvenido {usuario.NombreCompleto}!";

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var resultado = await _authService.RegistrarUsuario(
                model.NombreUsuario,
                model.Email,
                model.Password,
                model.NombreCompleto
            );

            if (!resultado.exito)
            {
                ModelState.AddModelError(string.Empty, resultado.mensaje);
                return View(model);
            }

            TempData["SuccessMessage"] = "Registro exitoso. Ahora puedes iniciar sesión.";
            return RedirectToAction("Login");
        }

        // POST: Account/Logout
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["SuccessMessage"] = "Sesión cerrada exitosamente";
            return RedirectToAction("Login");
        }

        // GET: Account/CambiarPassword
        [HttpGet]
        [Authorize]
        public IActionResult CambiarPassword()
        {
            return View();
        }

        // POST: Account/CambiarPassword
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarPassword(CambiarPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (usuarioIdClaim == null)
                return RedirectToAction("Login");

            int usuarioId = int.Parse(usuarioIdClaim.Value);

            var resultado = await _authService.CambiarPassword(
                usuarioId,
                model.PasswordActual,
                model.NuevaPassword
            );

            if (!resultado.exito)
            {
                ModelState.AddModelError(string.Empty, resultado.mensaje);
                return View(model);
            }

            TempData["SuccessMessage"] = resultado.mensaje;
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/AccessDenied
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
