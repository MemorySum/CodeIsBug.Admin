﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CodeIsBug.Admin.Models.Dto.Emp;
using Microsoft.IdentityModel.Tokens;

namespace CodeIsBug.Admin.Common.Helper
{
    /// <summary>
    /// jwt帮助类
    /// </summary>
    public class JwtHelper
    {
        /// <summary>
        /// 生产token
        /// </summary>
        /// <param name="jwtSettings"></param>
        /// <param name="dataDto"></param>
        /// <returns></returns>
        public static string CreatToken(JwtSettings jwtSettings, UserDataDto dataDto)
        {
            var claim = new[]
            {
                new(ClaimTypes.Name, dataDto.UserName),
                new Claim(ClaimTypes.Role, dataDto.UserRoleName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            //签名证书(秘钥，加密算法)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                jwtSettings.Issuer,
                jwtSettings.Audience,
                claim,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                creds);

            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodeToken;
        }
    }
}