using System.Linq;
using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeIsBug.Admin.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly CodeIsBugContext _codeIsBugContext;
        public AccountController(CodeIsBugContext context)
        {
            this._codeIsBugContext = context;
        }

        public IActionResult LoginPage()
        {
            return View();
        }
        [HttpPost]
        public Result Login(string username, string password)
        {
            var empInfo = _codeIsBugContext.eBaseEmps.FirstOrDefault(a => a.UserName.Equals(username) &&
                                                                          a.Pwd.ToLower().Equals(StringHelper.Md5Hash(password)));
            return empInfo != null ? new Result { Code = 1, Message = "登陆成功" } : new Result { Code = 0, Message = "账号或密码错误" };
        }
    }
}