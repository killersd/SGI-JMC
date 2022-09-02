using Microsoft.AspNetCore.Mvc;
using SGI_JMC.Extensions;
using SGI_JMC.Services;
using System.Text;
using System.Threading.Tasks;

namespace SGI_JMC.Controllers
{
    public class Email : Controller
    {
        private readonly IEmailService _emailService;

        public Email(IEmailService emailservice)
        {
            this._emailService = emailservice;   
        }

        public IActionResult Index()
        { 
        return View();  
        }

        public async Task<IActionResult> EnviarEmailTeste()
        {
            var html = new StringBuilder();
            html.Append("<h1>Redefinição de senha</h1>");
            html.Append("<p>Este é um teste do serviço de envio de e-mails usando ASP.NET Core.</p>");
            await _emailService.SendEmailAsync("killersdalexsd@gmail.com", "Redefinição de senha", string.Empty, html.ToString());
            this.MostrarMensagem("Uma mensagem foi enviada para o e-mail killersdalexsd@gmail.com.");

            return RedirectToAction(nameof(Index));
        }
    }
}
