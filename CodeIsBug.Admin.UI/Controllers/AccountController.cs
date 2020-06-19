using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models.DbContext;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CodeIsBug.Admin.UI.DTO;

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
        public Result Login([FromForm] LoginInputDto dto)
        {
            var empInfo = _codeIsBugContext.EBaseEmps.FirstOrDefault(a => a.UserName.Equals(dto.UserName) && a.Pwd.ToLower().Equals(StringHelper.Md5Hash(dto.UserPwd)));
            return empInfo != null ? new Result { Code = 1, Message = "登陆成功" } : new Result { Code = 0, Message = "账号或密码错误" };
        }
    }
}