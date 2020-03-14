using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CodeIsBug.Admin.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly CodeIsBugContext codeIsBugContext;
        public AccountController(CodeIsBugContext context)
        {
            this.codeIsBugContext = context;
        }

        public IActionResult LoginPage()
        {
            return View();
        }
        [HttpPost]
        public Result Login(string username, string password)
        {
            EBaseEmp empInfo = codeIsBugContext.eBaseEmps.FirstOrDefault(a => a.UserName.Equals(username) &&
                                a.Pwd.ToLower().Equals(StringHelper.Md5Hash(password)));
            if (empInfo != null)
            {
                
                return new Result { Code = 1, Message = "登陆成功" };
            }
            return new Result { Code = 0, Message = "账号或密码错误" };
        }
    }
}