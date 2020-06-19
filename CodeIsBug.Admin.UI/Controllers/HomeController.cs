using CodeIsBug.Admin.Models.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeIsBug.Admin.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly CodeIsBugContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(CodeIsBugContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}