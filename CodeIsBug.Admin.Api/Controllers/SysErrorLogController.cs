using System.Threading.Tasks;
using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeIsBug.Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SysErrorLogController : ControllerBase
    {
        private readonly SysErrorLogService _sysErrorLogService;

        public SysErrorLogController(SysErrorLogService sysErrorLogService)
        {
            _sysErrorLogService = sysErrorLogService;
        }
        /// <summary>
        /// 错误日志列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<Result> GetErrorLog()
        {
            var list = await _sysErrorLogService.GetAll();
            return ApiResultHelper.Success(list);
        }
    }
}
