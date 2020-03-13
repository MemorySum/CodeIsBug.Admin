using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CodeIsBug.Admin.UI.Controllers
{
    public class UserInfoController : Controller
    {
        public IActionResult UserList()
        {
            return View();
        }
    }
}