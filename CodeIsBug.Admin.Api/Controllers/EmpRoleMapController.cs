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
        ///   根据列表选择的用户guid加载对应的角色guid
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        [HttpGet("GetUserRoles")]
        public async Task<Result> GetUserRolesByUserId([FromQuery] Guid userGuid)
        {
            var result = new Result();
            try
            {
                var list = await _empRoleMapService.GetUserRolesByUserId(userGuid);
                if (list.Any())
                {
                    result.Code = 1;
                    result.Object = list;
                }
                else
                {
                    result.Code = 0;
                    result.Message = "无角色信息";
                }
            }
            catch (Exception e)
            {
                result.Code = -1;
                result.Message = $"获取用户对应角色信息失败,错误信息为{e.Message}";
            }

            return result;
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
            var result = new Result();
            try
            {
                var isSuccess = await _empRoleMapService.SaveRoleId(saveDto);
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
        #region 构造函数注入service
        private readonly EmpRoleMapService _empRoleMapService;

        public EmpRoleMapController(EmpRoleMapService empRoleMapService)
        {
            _empRoleMapService = empRoleMapService;
        }
        #endregion
    }
}
