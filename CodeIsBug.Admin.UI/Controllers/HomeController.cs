using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeIsBug.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeIsBug.Admin.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly CodeIsBugContext codeIsBugContext;
        public HomeController(CodeIsBugContext context)
        {
            this.codeIsBugContext = context;
        }
        public IActionResult Index()
        {
            

            
            return View();
        }
    }
}