using System;
using System.Threading.Tasks;
using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeIsBug.Admin.Api.Controllers
{
    /// <summary>
    ///  省市县信息
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CityInfoController : ControllerBase
    {
        /// <summary>
        /// 获取全国省市信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllCity")]
        public async Task<Result> GetCityInfo()
        {
            try
            {
                var result = await _cityInfoService.GetCityInfoTree();
                return ApiResultHelper.Success("城市信息获取成功", result);
            }
            catch (Exception e)
            {
                return ApiResultHelper.Error(-1, e.Message);
            }
        }

        #region 构造函数注入
        private readonly CityInfoService _cityInfoService;

        public CityInfoController(CityInfoService cityInfoService)
        {
            _cityInfoService = cityInfoService;
        }
        #endregion
    }
}