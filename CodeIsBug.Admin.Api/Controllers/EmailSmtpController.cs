using System;
using CodeIsBug.Admin.Common.Config;
using CodeIsBug.Admin.Common.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CodeIsBug.Admin.Api.Controllers
{
    /// <summary>
    /// emailSMTP配置
    /// </summary>
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmailSmtpController : ControllerBase
    {
        private readonly EmailSmtpConfig _emailSmtpConfig;
        

        public EmailSmtpController(IOptions<EmailSmtpConfig> emailSmtpConfig)
        {
            _emailSmtpConfig = emailSmtpConfig.Value;
            
            
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
