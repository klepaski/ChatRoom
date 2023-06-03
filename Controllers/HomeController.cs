using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using task6x.Services;
using task6x.Models;

namespace task6x.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMessageService _messageService;

        public HomeController(ILogger<HomeController> logger, IMessageService messageService)
        {
            _logger = logger;
            _messageService = messageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login()
        {
            var userName = HttpContext.Request.Form["userName"];
            HttpContext.Session.SetString("userName", userName);
            await _messageService.CreateUserAsync(userName);
            return RedirectPermanent("~/Home/ChatRoom");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userName");
            return RedirectPermanent("~/Home/Index");
        }

        public async Task<IActionResult> ChatRoom()
        {
            var currentUser = HttpContext.Session.GetString("userName");
            if (currentUser == null) return RedirectPermanent("~/Home/Index");
            ViewBag.Users = await _messageService.GetUsersAsync();
            ViewBag.CurrentUser = currentUser;
            return View();
        }

        public async Task<IActionResult> MessagesOfUser()
        {
            var currentUser = HttpContext.Session.GetString("userName");
            ViewBag.UserMessages = await _messageService.GetMessagesForUserAsync(currentUser);
            return PartialView("_MessagesOfUser");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}