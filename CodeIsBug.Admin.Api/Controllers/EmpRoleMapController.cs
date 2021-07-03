using System;
using System.Linq;
using System.Threading.Tasks;
using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models.Dto;
using CodeIsBug.Admin.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeIsBug.Admin.Api.Controllers
{
    /// <summary>
    ///     用户角色对照
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpRoleMapController : ControllerBase
    {
        #region 根据列表选择的用户guid加载对应的角色guid

        /// <summary>
        ///     根据列表选择的用户guid加载对应的角色guid
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        [HttpGet("GetUserRoles")]
        public async Task<Result> GetUserRolesByUserId([FromQuery] Guid userGuid)
        {
            try
            {
                var list = await _empRoleMapService.GetUserRolesByUserId(userGuid);
                if (list.Any())
                    return ApiResultHelper.Success(list);
                return ApiResultHelper.Failed("无角色信息");
            }
            catch (Exception e)
            {
                return ApiResultHelper.Error(-1, $"获取用户对应角色信息失败,错误信息为{e.Message}");
            }
        }

        #endregion

        #region 保存用户角色

        /// <summary>
        ///     保存用户角色
        /// </summary>
        /// <param name="saveDto"></param>
        /// <returns></returns>
        [HttpPost("SaveUserRole")]
        public async Task<Result> SaveRoleId([FromBody] EmpRoleMapSaveDto saveDto)
        {
            if (saveDto == null) throw new ArgumentNullException(nameof(saveDto));
            try
            {
                var isSuccess = await _empRoleMapService.SaveRoleId(saveDto);
                return isSuccess ? ApiResultHelper.Success("角色添加成功") : ApiResultHelper.Failed("角色添加失败");
            }
            catch (Exception e)
            {
                return ApiResultHelper.Error(-1, e.Message);
            }
        }

        #endregion

        #region 构造函数注入service

        private readonly EmpRoleMapService _empRoleMapService;

        public EmpRoleMapController(EmpRoleMapService empRoleMapService)
        {
            _empRoleMapService = empRoleMapService;
        }

        #endregion
    }
}