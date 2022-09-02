using Microsoft.AspNetCore.Mvc;
using SGI_JMC.ViewModels;

namespace SGI_JMC.Extensions
{
    public static class ControllerExtension
    {
        public static void MostrarMensagem(this Controller @this, string texto, bool erro = false)
        {
            @this.TempData["mensagem"] = MensagemViewModel.Serializar(
                texto, erro ? TipoMensagem.Erro : TipoMensagem.Informacao);
        }
    }
}
