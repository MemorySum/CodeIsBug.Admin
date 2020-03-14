using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodeIsBug.Admin.Common.Helper;
using Microsoft.Extensions.Configuration;

namespace CodeIsBug.Admin.UI.Controllers
{
    public class VerifyCodeController : Controller
    {
        private IConfiguration _configuration { get; }
        private RedisHelper redisHelper;
        public VerifyCodeController(IConfiguration configuration)
        {
            this._configuration = configuration;
            string redisConnectionString = _configuration.GetValue<string>("RedisConn");
            redisHelper = new RedisHelper(redisConnectionString);
        }
        public FileContentResult GetCode(string guid)
        {
            string code = VerifyCodeHelper.GetSingleObj().CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.NumberVerifyCode);
            TimeSpan ts1 = new TimeSpan(0, 0, 1, 0);
            redisHelper.SetValue(guid, code,ts1);
            byte[] codeImage = VerifyCodeHelper.GetSingleObj().CreateByteByImgVerifyCode(code, 100, 40);
            return File(codeImage, @"image/jpeg");
        }
        [HttpGet]
        public Result CheckCode(string guid, string code)
        {
            string redisCode = string.Empty;
            try
            {
                 redisCode = redisHelper.GetValue(guid).ToLower();
            }
            catch (Exception)
            {
                return new Result { Code = 0, Message = "Redis缓存已过期，请刷新页面后重试!" };
            }
            if (string.IsNullOrEmpty(redisCode))
            {
                return new Result { Code = 0, Message = "Redis缓存已过期，请刷新页面后重试!" };
            }
            if (!redisCode.Equals(code.ToLower()))
            {
                return new Result { Code = 0, Message = "验证码不正确，请重新输入!" };
            }
            return new Result { Code = 1, Message = "验证通过!" };
        }
    }
}