using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeIsBug.Admin.Common.Helper;
using Microsoft.AspNetCore.Mvc;

namespace CodeIsBug.Admin.UI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult LoginPage()
        {
            return View();
        }
        [HttpPost]
        public Result Login(string username,string password,string verifyCode)
        {
            if(username =="admin"  && password == "1")
            {
                return new Result { Code = 1,Message ="登陆成功"};
            }
            return new Result { Code = 0, Message = "账号或密码错误" };
        }
    }
}