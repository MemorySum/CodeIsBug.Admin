using System;
using CodeIsBug.Admin.Common.Config;
using CodeIsBug.Admin.Common.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CodeIsBug.Admin.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmailSmtpController : ControllerBase
    {
        private readonly EmailSmtpConfig _emailSmtpConfig;
        private readonly IConfiguration _configuration;

        public EmailSmtpController(IOptions<EmailSmtpConfig> emailSmtpConfig, IConfiguration configuration)
        {
            _emailSmtpConfig = emailSmtpConfig.Value;
            _configuration = configuration;
            
        }

        /// <summary>
        /// 获取emailSMTP配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Result GetConfig()
        {
            try
            {
                return new Result
                {
                    Code = 1,
                    Message = "success",
                    Object = new
                    {
                        _emailSmtpConfig.SendEmail,
                        _emailSmtpConfig.SendNickname,
                        _emailSmtpConfig.SendPassword,
                        _emailSmtpConfig.SendPort,
                        _emailSmtpConfig.SendServer,
                    }
                };
            }
            catch (Exception e)
            {
                return new Result
                {
                    Code = -1,
                    Message = e.Message
                };
            }
        }
    }
}
