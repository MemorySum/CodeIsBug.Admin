using CodeIsBug.Admin.Api.DTO;
using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models.DbContext;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CodeIsBug.Admin.UI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly CodeIsBugContext _codeIsBugContext;
		public AccountController(CodeIsBugContext context)
		{
			this._codeIsBugContext = context;
		}

		/// <summary>
		/// 登录接口
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		[HttpPost]
		public Result Login([FromForm] LoginInputDto dto)
		{
			var empInfo = _codeIsBugContext.EBaseEmps.FirstOrDefault(a => a.UserName.Equals(dto.username) && a.Pwd.ToLower().Equals(StringHelper.Md5Hash(dto.password)));
			return empInfo != null ? new Result { Code = 1, Message = "登陆成功" } : new Result { Code = 0, Message = "账号或密码错误" };
		}
	}
}