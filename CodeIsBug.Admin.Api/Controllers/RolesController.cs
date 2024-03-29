﻿using System;
using System.Threading.Tasks;
using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models.Dto.Role;
using CodeIsBug.Admin.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeIsBug.Admin.Api.Controllers
{
    /// <summary>
    ///     角色管理
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        #region 加载角色树

        /// <summary>
        ///     加载角色树
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRolesTree")]
        public async Task<Result> GetRoles()
        {
            var res = new Result();
            try
            {
                res.Object =await _rolesService.GetRoles();
                res.Code = 1;
                res.Message = "菜单获取成功";
            }
            catch (Exception e)
            {
                res.Code = -1;
                res.Message = e.Message;
            }

            return res;
        }

        #endregion

        #region 添加角色

        /// <summary>
        ///     添加角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("AddRole")]
        public async Task<Result> AddRole([FromBody] RoleAddDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            var result = new Result();
            try
            {
                var isSuccess = await _rolesService.AddRole(dto);
                result.Code = isSuccess ? 1 : 0;
                result.Message = isSuccess ? "角色添加成功" : "角色添加失败";
            }
            catch (Exception e)
            {
                result.Code = -1;
                result.Message = e.Message;
            }

            return result;
        }

        #endregion

        #region 删除角色

        /// <summary>
        ///     删除角色
        /// </summary>
        /// <param name="roleGuid">角色guid</param>
        /// <returns></returns>
        [HttpDelete("DelRole")]
        public async Task<Result> DelRole([FromQuery] Guid roleGuid)
        {
            if (roleGuid.Equals(Guid.Empty)) throw new ArgumentNullException(nameof(roleGuid));
            var result = new Result();
            try
            {
                var isHasChildren = await _rolesService.IsHasChildren(roleGuid);
                if (isHasChildren)
                {
                    result.Code = 0;
                    result.Message = "当前角色含有子级角色，请删除所有子级角色后才能删除";
                }
                else
                {
                    var isSuccess = await _rolesService.DelRole(roleGuid);
                    result.Code = isSuccess ? 1 : 0;
                    result.Message = isSuccess ? "角色删除成功" : "角色删除失败";
                }
            }
            catch (Exception e)
            {
                result.Code = -1;
                result.Message = e.Message;
            }

            return result;
        }

        #endregion

        #region 获取单个角色信息

        /// <summary>
        ///     获取单个角色信息
        /// </summary>
        /// <param name="roleGuid">角色guid</param>
        /// <returns></returns>
        [HttpGet("GetRoleInfo")]
        public async Task<Result> GetRoleInfo([FromQuery] Guid roleGuid)
        {
            if (roleGuid.Equals(Guid.Empty)) throw new ArgumentNullException(nameof(roleGuid));
            var r = new Result();
            try
            {
                var info = await _rolesService.GetRoleInfo(roleGuid);
                if (!ReferenceEquals(info, null))
                {
                    r.Code = 1;
                    r.Message = "角色信息获取成功";
                    r.Object = info;
                }
                else
                {
                    r.Code = 0;
                    r.Message = "角色信息获取失败";
                }
            }
            catch (Exception e)
            {
                r.Code = -1;
                r.Message = e.Message;
            }

            return r;
        }

        #endregion

        #region 更新角色信息

        /// <summary>
        ///     更新角色信息
        /// </summary>
        /// <param name="info">角色信息dto</param>
        /// <returns></returns>
        [HttpPut("EditRoleInfo")]
        public async Task<Result> EditRoleInfo([FromBody] RoleEditInfo info)
        {
            if (ReferenceEquals(info, null)) throw new ArgumentNullException(nameof(info));
            var result = new Result();
            try
            {
                var isSuccess = await _rolesService.EditRoleInfo(info);
                result.Code = isSuccess ? 1 : 0;
                result.Message = isSuccess ? "角色修改成功" : "角色修改失败";
            }
            catch (Exception e)
            {
                result.Code = -1;
                result.Message = e.Message;
            }

            return result;
        }

        #endregion

        #region 构造函数注入

        private readonly RolesService _rolesService;

        public RolesController(RolesService rolesService)
        {
            _rolesService = rolesService;
        }

        #endregion
    }
}