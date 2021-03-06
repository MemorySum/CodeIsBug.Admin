﻿using System;
using System.Linq;
using System.Threading.Tasks;
using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models.Dto;
using CodeIsBug.Admin.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeIsBug.Admin.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleMenuMapController : ControllerBase
    {
        #region 构造函数
        private readonly RoleMenuMapService _roleMenuMapService;

        public RoleMenuMapController(RoleMenuMapService roleMenuMapService)
        {
            _roleMenuMapService = roleMenuMapService;
        }

        #endregion

        #region 根据选择的角色id加载选中菜单树
        /// <summary>
        /// 根据选择的角色id加载选中菜单树
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> GetMenuListByRoleId([FromQuery] Guid roleGuid)
        {
            Result result = new Result();
            try
            {
                var list = await _roleMenuMapService.GetMenuListByRoleId(roleGuid);
                result.Code = 1;
                result.Object = list;
            }
            catch (Exception e)
            {
                result.Code = -1;
                result.Message = $"获取角色对应菜单信息失败,错误信息为{e.Message}";
            }

            return result;
        }
        #endregion

        #region 保存角色菜单权限
        /// <summary>
        /// 保存角色菜单权限
        /// </summary>
        /// <param name="saveDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> SaveRoleMenuInfo([FromBody] RoleMenuMapSaveDto saveDto)
        {
            if (saveDto == null) throw new ArgumentNullException(nameof(saveDto));
            var result = new Result();
            try
            {
                bool isSuccess = await _roleMenuMapService.SaveRoleMenuInfo(saveDto);
                result.Code = isSuccess ? 1 : 0;
                result.Message = isSuccess ? "菜单权限添加成功" : "菜单权限添加失败";
            }
            catch (Exception e)
            {
                result.Code = -1;
                result.Message = e.Message;
            }

            return result;


        }
        #endregion
    }
}
