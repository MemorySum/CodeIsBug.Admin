using System;
using System.Threading.Tasks;
using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models.Dto;
using CodeIsBug.Admin.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CodeIsBug.Admin.Api.Controllers
{
    /// <summary>
    ///     登录授权
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly EmpRoleMapService _empRoleMapService;
        private readonly JwtSettings _jwtSettings;

        public AccountController(IOptions<JwtSettings> jwtSettings, AccountService accountService,
            EmpRoleMapService empRoleMapService)
        {
            _jwtSettings = jwtSettings.Value;
            _accountService = accountService;
            _empRoleMapService = empRoleMapService;
        }

        /// <summary>
        ///     登录接口
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<Result> Login([FromBody] LoginInputDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrEmpty(dto.username)) return ApiResultHelper.Failed("账号必填");
            if (string.IsNullOrWhiteSpace(dto.password)) return ApiResultHelper.Failed("密码必填");
            var loginResult = await _accountService.Login(dto);

            if (loginResult is null) return ApiResultHelper.Failed("账号或密码错误");

            var userRoleMenu = await _empRoleMapService.GetUserRoleMenu(loginResult.UserId);
            var userRoleNameStr = await _empRoleMapService.GetUserRoleName(loginResult.UserId);
            UserDataDto userInfo = new()
            {
                UserId = loginResult.UserId,
                UserName = loginResult.Name,
                UserMenus = userRoleMenu,
                UserRoleName = userRoleNameStr
            };
            var token = JwtHelper.CreatToken(_jwtSettings, userInfo);

            return ApiResultHelper.Success("登录成功", userInfo, new {Access_Token = token});
        }
    }
}