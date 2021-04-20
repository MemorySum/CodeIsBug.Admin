﻿using System;
using System.Threading.Tasks;
using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CodeIsBug.Admin.Api.Controllers
{
    /// <summary>
    ///     省市县信息
    /// </summary>
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CityInfoController : ControllerBase
    {
        /// <summary>
        ///     获取全国省市信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> GetCityInfo()
        {
            Result res = new();
            try
            {
                var result = await _cityInfoService.GetCityInfoTree();

                res.Object = result;
                res.Code = 1;
                res.Message = "城市信息获取成功";
            }
            catch (Exception e)
            {
                res.Code = -1;
                res.Message = e.Message;
            }

            return res;
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
