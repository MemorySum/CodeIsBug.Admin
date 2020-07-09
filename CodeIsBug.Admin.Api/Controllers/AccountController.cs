using System;
using System.Threading.Tasks;
using CodeIsBug.Admin.Api.DTO;
using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models.DbContext;
using CodeIsBug.Admin.Models.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CodeIsBug.Admin.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	[EnableCors("CodeIsBug.Admin.Policy")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly CodeIsBugContext _codeIsBugContext;
		private readonly JwtSettings _jwtSettings;
		public AccountController(CodeIsBugContext context, IOptions<JwtSettings> jwtSettings)
		{
			this._codeIsBugContext = context;
			_jwtSettings = jwtSettings.Value;
		}

		/// <summary>
		/// 登录接口
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<Result> Login([FromBody] LoginInputDto dto)
		{
			if (dto == null)
			{
				throw new ArgumentNullException(nameof(dto));
			}
			if (string.IsNullOrEmpty(dto.username))
			{
				return new Result { Code = 0, Message = "账号必填" };
			}
			if (string.IsNullOrWhiteSpace(dto.password))
			{
				return new Result { Code = 0, Message = "密码必填" };
			}
			var empInfo = await _codeIsBugContext.EBaseEmps.FirstOrDefaultAsync(a => a.UserName.Equals(dto.username) && a.Pwd.ToLower().Equals(StringHelper.Md5Hash(dto.password)));
			if (ReferenceEquals(empInfo, null))
			{
				return new Result { Code = 0, Message = "账号或密码错误" };
			}
			UserDataDto userData = new UserDataDto();
			UserDataDto userInfo = new UserDataDto
			{
				UserId = empInfo.UserGuid,
				UserName = empInfo.UserName,
				UserRoleIds = string.Empty,
				UserRoleName = string.Empty
			};
			var token = JwtHelper.CreatToken(_jwtSettings, userInfo);
			return new Result
			{
				Code = 1,
				Message = "登录成功",
				Object = userInfo,
				ExtendObject = new { Access_Token = token }
			};
		}
	}
}