using Microsoft.AspNetCore.Mvc;

namespace SGI_JMC.Extensions
{

    public enum NotificationType
    {
        Success,
        Error,
        Info
    }

    public class BaseController : Controller
    {
        public void BasicNotification(string msg, NotificationType type, string title = "")
        {
            TempData["notification"] = $"Swal.fire('{title}', '{msg}', '{type.ToString().ToLower()}'";
        }
    }
}
