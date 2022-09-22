using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGI_JMC.Extensions;
using SGI_JMC.Models;
using SGI_JMC.Services;
using SGI_JMC.ViewModels;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGI_JMC.Controllers
{

    public class UsuarioController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;

        public IActionResult Index()
        {
            return View();
        }

        public UsuarioController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEmailService emailService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._emailService = emailService;
        }



        //[Authorize(Roles ="administrador")]
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
                        await EnviarLinkConfirmacaoEmailAsync(usuarioBD);
                        this.MostrarMensagem("Usuário cadastrado com sucesso. Uma mensagem de confirmação foi enviada para" +
                            "o seu e-mail. Clique no link de confirmação  recebido para concluir o processo de cadastro.");
                        if (User.IsInRole("administrador"))
                        {
                            return RedirectToAction("IndexUsuarios");
                        }
                        else
                        {
                            return RedirectToAction("Login");
                        }
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
                try
                {
                    var usuario = await _userManager.FindByNameAsync(login.Usuario);
                    if (!_userManager.IsEmailConfirmedAsync(usuario).Result)
                    {
                        this.MostrarMensagem("Este e-mail ainda não foi confirmado. Confirme e tente fazer o login novamente.", true);
                        return View(login);
                    }

                    var resultado = await _signInManager.PasswordSignInAsync(login.Usuario, login.Senha, login.Lembrar, false);
                    if (resultado.Succeeded)
                    {
                        login.ReturnUrl = login.ReturnUrl ?? "~/";
                        return LocalRedirect(login.ReturnUrl);
                        //return RedirectToAction("VerificarAutenticidade", "Declaracao");
                    }
                    else
                    {
                        this.MostrarMensagem("Senha inválida.", true);
                        return View(login);
                    }
                }
                catch (System.Exception)
                {

                    this.MostrarMensagem("Usuário inválido.", true);
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

        [HttpGet]
        public IActionResult EsqueciSenha()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EsqueciSenha([FromForm] EsqueciSenhaViewModel dados)
        {
            if (ModelState.IsValid)
            {
                if (_userManager.Users.AsNoTracking().Any(u => u.NormalizedEmail == dados.Email.ToUpper().Trim()))
                {
                    var usuario = await _userManager.FindByEmailAsync(dados.Email);
                    var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                    var urlConfirmacao = Url.Action(nameof(RedefinirSenha), "Usuario", new { token }, Request.Scheme);
                    var mensagem = new StringBuilder();
                    mensagem.Append($"<p>Olá, {usuario.UserName}.</p>");
                    mensagem.Append("<p>Houve uma solicitação de redefinição de senha para seu usuário em nosso sistema. Se não foi você que fez a solicitação, ignore essa mensagem. Caso tenha sido você, clique no link abaixo para criar sua nova senha:</p>");
                    mensagem.Append($"<p><a href='{urlConfirmacao}'>Redefinir Senha</a></p>");
                    mensagem.Append("<p>Atenciosamente,<br>Equipe de Suporte do SGI-JMC</p>");
                    await _emailService.SendEmailAsync(usuario.Email,
                        "Redefinição de Senha", "", mensagem.ToString());
                    return View(nameof(EmailRedefinicaoEnviado));
                }
                else
                {
                    this.MostrarMensagem(
                            $"E-mail <b>{dados.Email}</b> não encontrado.", true);
                    return View();
                }
            }
            else
            {
                return View(dados);
            }
        }

        public IActionResult EmailRedefinicaoEnviado()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RedefinirSenha(string token)
        {
            var modelo = new RedefinirSenhaViewModel();
            modelo.Token = token;
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> RedefinirSenha([FromForm] RedefinirSenhaViewModel dados)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _userManager.FindByEmailAsync(dados.Email);
                var resultado = await _userManager.ResetPasswordAsync(
                    usuario, dados.Token, dados.NovaSenha);
                if (resultado.Succeeded)
                {
                    this.MostrarMensagem(
                       $"Senha redefinida com sucesso! Agora você já pode fazer login com a nova senha.");
                    return View(nameof(Login));
                }
                else
                {
                    this.MostrarMensagem(
                        $"Não foi possível redefinir a senha. Verifique se preencheu a senha corretamente. Se o problema persistir, entre em contato com o suporte.");
                    return View(dados);
                }
            }
            else
            {
                return View(dados);
            }
        }


        [HttpGet, Authorize]
        public IActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AlterarSenha([FromForm] AlterarSenhaViewModel dados)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                var resultado = await _userManager.ChangePasswordAsync(usuario, dados.SenhaAtual, dados.NovaSenha);
                if (resultado.Succeeded)
                {
                    this.MostrarMensagem(
                        $"Sua senha foi alterada com sucesso.");
                    await _signInManager.SignOutAsync();
                    return RedirectToAction(nameof(Login), "Usuario");
                }
                else
                {
                    this.MostrarMensagem(
                        $"Não foi possível alterar sua senha. Confira os dados informados e tente novamente.");
                    return View(dados);
                }
            }
            else
            {
                return View(dados);
            }
        }

        private async Task EnviarLinkConfirmacaoEmailAsync(IdentityUser usuario)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(usuario);
            var urlConfirmacao = Url.Action("ConfirmarEmail",
                "Usuario", new { email = usuario.Email, token }, Request.Scheme);
            var mensagem = new StringBuilder();
            mensagem.Append($"<p>Olá, {usuario.UserName}.</p>");
            mensagem.Append("<p>Recebemos seu cadastro em nosso sistema. Para concluir o processo de cadastro, clique no link a seguir:</p>");
            mensagem.Append($"<p><a href='{urlConfirmacao}'>Confirmar Cadastro</a></p>");
            mensagem.Append("<p>Atenciosamente,<br>Equipe de Suporte</p>");
            await _emailService.SendEmailAsync(usuario.Email,
                "Confirmação de Cadastro", "", mensagem.ToString());
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmarEmail(string email, string token)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            if (usuario == null)
            {
                this.MostrarMensagem("Não foi possível confirmar o e-mail. Usuário não encontrado", true);
            }
            var resultado = await _userManager.ConfirmEmailAsync(usuario, token);
            if (resultado.Succeeded)
            {
                this.MostrarMensagem("E-mail confirmado com sucesso! Agora você já está liberado para fazer o login.");
            }
            else
            {
                this.MostrarMensagem("Não foi possível validar seu e-mail. Tente novamente em alguns minutos. Se o problema persistir, entre em contato com o suporte.", true);
            }
            return View(nameof(Login));
        }

    }
}