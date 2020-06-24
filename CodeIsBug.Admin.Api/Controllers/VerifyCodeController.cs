using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeIsBug.Admin.Common.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CodeIsBug.Admin.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class VerifyCodeController : ControllerBase
	{
        private IConfiguration Configuration { get; }
        private readonly RedisHelper _redisHelper;
		public VerifyCodeController(IConfiguration configuration)
		{
			this.Configuration = configuration;
			string redisConnectionString = Configuration.GetValue<string>("RedisConn");
			_redisHelper = new RedisHelper(redisConnectionString);
		}
		/// <summary>
		/// 生成登录验证码
		/// </summary>
		/// <param name="guid">页面生成的GUID</param>
		/// <returns>返回验证码图片</returns>
		[HttpGet]
		 public FileContentResult GetCode(string guid)
        {
            string code = VerifyCodeHelper.GetSingleObj().CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.NumberVerifyCode);
            TimeSpan ts1 = new TimeSpan(0, 0, 1, 0);
            _redisHelper.SetValue(guid, code,ts1);
            byte[] codeImage = VerifyCodeHelper.GetSingleObj().CreateByteByImgVerifyCode(code, 100, 40);
            return File(codeImage, @"image/jpeg");
        }
	}
}
