using System;
using System.Threading.Tasks;
using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
namespace CodeIsBug.Admin.Api.Controllers
{
    /// <summary>
    ///     验证码
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VerifyCodeController : ControllerBase
    {
        private readonly RedisHelper _redisHelper;

        public VerifyCodeController(IConfiguration configuration)
        {
            Configuration = configuration;
            var redisConnectionString = Configuration.GetValue<string>("RedisConn");
            _redisHelper = new RedisHelper(redisConnectionString);
        }
        private IConfiguration Configuration { get; }

        /// <summary>
        ///     生成登录验证码
        /// </summary>
        /// <param name="guid">页面生成的GUID</param>
        /// <returns>返回验证码图片</returns>
        [HttpGet]
        public FileContentResult GetCode(string guid)
        {
            var code = VerifyCodeHelper.GetSingleObj()
                .CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.NumberVerifyCode);
            var ts1 = new TimeSpan(0, 0, 1, 0);
            _redisHelper.SetValue(guid, code, ts1);
            var codeImage = VerifyCodeHelper.GetSingleObj().CreateByteByImgVerifyCode(code, 100, 40);
            return File(codeImage, @"image/jpeg");
        }

        /// <summary>
        ///     验证验证码
        /// </summary>
        /// <param name="checkCodeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> VerifyCode([FromBody] CkeckCodeDto checkCodeDto)
        {
            if (checkCodeDto == null) throw new ArgumentNullException(nameof(checkCodeDto));

            if (Equals(Guid.Empty, checkCodeDto.CheckGuid)) return new Result { Code = -1, Message = "参数错误" };
            var code = await _redisHelper.GetValueAsync(checkCodeDto.CheckGuid.ToString());
            if (!code.HasValue) return new Result { Code = 0, Message = "验证码已过期，请刷新页面重新尝试登录" };
            if (!code.ToString().Equals(checkCodeDto.CheckCode)) return new Result { Code = 0, Message = "验证不通过" };
            return new Result { Code = 1, Message = "验证通过" };
        }
    }
}
