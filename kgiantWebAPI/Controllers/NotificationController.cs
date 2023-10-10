using Microsoft.AspNetCore.Mvc;

namespace kgiantWebAPI.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult SendNotification(String data)
        {

            return View();
    }
}
}
