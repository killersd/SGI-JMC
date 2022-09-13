using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGI_JMC.Extensions;
using SGI_JMC.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace SGI_JMC.Controllers
{

    public class UsuarioController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IActionResult Index()
        {
            return View();
        }

        public UsuarioController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }



        [Authorize(Roles ="administrador")]
        [HttpGet]
        public async Task<IActionResult> Cadastrar(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var usuarioBD = await _userManager.FindByIdAsync(id);
                if (usuarioBD == null)
                {
                    this.MostrarMensagem("Usuário não encontrado.", true);
                    return RedirectToAction("Index", "Home");
                }
                var usuarioVM = new CadastrarUsuarioViewModel
                {
                    Id = usuarioBD.Id,
                    NomeUsuario = usuarioBD.UserName,
                    Email = usuarioBD.Email,
                    Telefone = usuarioBD.PhoneNumber
                };
                return View(usuarioVM);
            }
            return View(new CadastrarUsuarioViewModel());
        }

        private bool EntidadeExiste(string id)
        {
            return (_userManager.Users.AsNoTracking().Any(u => u.Id == id));
        }

        private static void MapearCadastrarUsuarioViewModel(CadastrarUsuarioViewModel entidadeOrigem, IdentityUser entidadeDestino)
        {
            entidadeDestino.UserName = entidadeOrigem.NomeUsuario;
            entidadeDestino.NormalizedUserName =
                entidadeOrigem.NomeUsuario.ToUpper().Trim();
            entidadeDestino.Email = entidadeOrigem.Email;
            entidadeDestino.NormalizedEmail =
                entidadeOrigem.Email.ToUpper().Trim();
            entidadeDestino.PhoneNumber = entidadeOrigem.Telefone;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(
        [FromForm] CadastrarUsuarioViewModel usuarioVM)
        {
            //se for alteração, não tem senha e confirmação de senha
            if (!string.IsNullOrEmpty(usuarioVM.Id))
            {
                ModelState.Remove("Senha");
                ModelState.Remove("ConfSenha");
            }

            if (ModelState.IsValid)
            {
                if (EntidadeExiste(usuarioVM.Id))
                {
                    var usuarioBD = await _userManager.FindByIdAsync(usuarioVM.Id);
                    if ((usuarioVM.Email != usuarioBD.Email) &&
                        (_userManager.Users.Any(u => u.NormalizedEmail == usuarioVM.Email.ToUpper().Trim())))
                    {
                        ModelState.AddModelError("Email",
                            "Já existe um usuário cadastrado com este e-mail.");
                        return View(usuarioVM);
                    }
                    MapearCadastrarUsuarioViewModel(usuarioVM, usuarioBD);

                    var resultado = await _userManager.UpdateAsync(usuarioBD);
                    if (resultado.Succeeded)
                    {
                        this.MostrarMensagem("Usuário alterado com sucesso.");
                        return RedirectToAction("IndexUsuarios");
                    }
                    else
                    {
                        this.MostrarMensagem("Não foi possível alterar o usuário.", true);
                        foreach (var error in resultado.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(usuarioVM);
                    }
                }
                else
                {
                    var usuarioBD = await _userManager.FindByEmailAsync(usuarioVM.Email);
                    if (usuarioBD != null)
                    {
                        ModelState.AddModelError("Email",
                            "Já existe um usuário cadastrado com este e-mail.");
                        return View(usuarioBD);
                    }

                    usuarioBD = new IdentityUser();
                    MapearCadastrarUsuarioViewModel(usuarioVM, usuarioBD);

                    var resultado = await _userManager.CreateAsync(
                        usuarioBD, usuarioVM.Senha);
                    if (resultado.Succeeded)
                    {
                        this.MostrarMensagem("Usuário cadastrado com sucesso. Use suas credenciais para entrar no sistema.");
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        this.MostrarMensagem("Erro ao cadastrar usuário.", true);
                        foreach (var error in resultado.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(usuarioVM);
                    }
                }
            }
            else
            {
                return View(usuarioVM);
            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(login.Usuario, login.Senha, login.Lembrar, false);
                if (resultado.Succeeded)
                {
                    login.ReturnUrl = login.ReturnUrl ?? "~/";
                    return LocalRedirect(login.ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty,
                        "Tentativa de login inválida. Reveja seus dados de acesso e tente novamente.");
                    return View(login);
                }
            }
            else
            {
                return View(login);
            }
        }

        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> IndexUsuarios()
        {
            var usuarios = await _userManager.Users.AsNoTracking().ToListAsync();
            //captura os administradore e coloca na viewbag "Administradores"
            var admins = (await _userManager.GetUsersInRoleAsync("administrador"))
                .Select(u => u.UserName);
            ViewBag.Administradores = admins;
            return View(usuarios);
        }


        [HttpGet]
        public async Task<IActionResult> Excluir(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                this.MostrarMensagem("Usuário não informado.", true);
                return RedirectToAction(nameof(Index));
            }

            if (!EntidadeExiste(id))
            {
                this.MostrarMensagem("Usuário não encontrado.", true);
                return RedirectToAction(nameof(Index));
            }

            var usuario = await _userManager.FindByIdAsync(id);

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> ExcluirPost(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario != null)
            {
                var resultado = await _userManager.DeleteAsync(usuario);
                if (resultado.Succeeded)
                {
                    this.MostrarMensagem("Usuário excluído com sucesso.");
                }
                else
                {
                    this.MostrarMensagem("Não foi possível excluir o usuário.", true);
                }
                return RedirectToAction(nameof(IndexUsuarios));
            }
            else
            {
                this.MostrarMensagem("Usuário não encontrado.", true);
                return RedirectToAction(nameof(IndexUsuarios));
            }
        }


        public IActionResult AcessoRestrito([FromQuery] string returnUrl)
        {
            return View(model: returnUrl);
        }

        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> AddAdministrador(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario != null)
            {
                var resultado = await _userManager.AddToRoleAsync(usuario, "administrador");
                if (resultado.Succeeded)
                {
                    this.MostrarMensagem(
                        $"Perfil administrador adicionado com sucesso para <b>{usuario.UserName}</b>.");
                }
                else
                {
                    this.MostrarMensagem(
                        $"Não foi possível adicionar perfil administrador para <b>{usuario.UserName}</b>.", true);
                }
                return RedirectToAction(nameof(IndexUsuarios));
            }
            else
            {
                this.MostrarMensagem("Usuário não encontrado.", true);
                return RedirectToAction(nameof(IndexUsuarios));
            }
        }

        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> RemAdministrador(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario != null)
            {
                var resultado = await _userManager.RemoveFromRoleAsync(usuario, "administrador");
                if (resultado.Succeeded)
                {
                    this.MostrarMensagem(
                        $"Perfil administrador removido com sucesso de <b>{usuario.UserName}</b>.");
                }
                else
                {
                    this.MostrarMensagem(
                        $"Não foi possível remover perfil administrador de <b>{usuario.UserName}</b>.", true);
                }
                return RedirectToAction(nameof(IndexUsuarios));
            }
            else
            {
                this.MostrarMensagem("Usuário não encontrado.", true);
                return RedirectToAction(nameof(IndexUsuarios));
            }
        }

    }
}