﻿using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models;
using CodeIsBug.Admin.Models.DbContext;
using CodeIsBug.Admin.Models.Dto;
using CodeIsBug.Admin.Models.DTO;
using CodeIsBug.Admin.Models.Models;
using CodeIsBug.Admin.Services.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using Microsoft.AspNetCore.Authorization;

namespace CodeIsBug.Admin.Api.Controllers
{
    /// <summary>
    /// 登录授权
    /// </summary>
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        public AccountController(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public Result Login([FromBody] LoginInputDto dto)
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
            AccountService  accountService =   new AccountService();
            var empInfo = accountService.Login(dto);
            if (ReferenceEquals(empInfo, null))
            {
                return new Result { Code = 0, Message = "账号或密码错误" };
            }

            UserDataDto userInfo = new UserDataDto
            {
                UserId = empInfo.UserId,
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