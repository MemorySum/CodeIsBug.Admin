using System;
using System.Threading.Tasks;
using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models.Dto;
using CodeIsBug.Admin.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeIsBug.Admin.Api.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SysUserController : ControllerBase
    {
        #region 获取用户列表
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public Result GetUserList(string query, int pageIndex, int pageSize)
        {
            Result res = new Result();
            SysUsersService menuService = new SysUsersService();
            int totalCount = 0;
            try
            {
                var result = menuService.GetUserList(query, pageIndex, pageSize, ref totalCount);

                res.Object = new
                {
                    data = result,
                    count = totalCount
                };
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

        #region 添加用户
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="inputInfo">用户实体</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> AddUser([FromBody] UserAddInfo inputInfo)
        {
            Result result = new Result();
            try
            {
                if (ReferenceEquals(null, inputInfo))
                {
                    result.Code = 0;
                    result.Message = "参数错误";
                }
                else
                {
                    SysUsersService usersService = new SysUsersService();
                    bool flag = await usersService.AddUser(inputInfo);
                    if (flag)
                    {
                        result.Code = 1;
                        result.Message = "用户添加成功";
                    }
                    else
                    {
                        result.Code = 0;
                        result.Message = "用户添加失败";
                    }
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

        #region 删除用户
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> DelUser([FromQuery] int UserId)
        {
            Result result = new Result();
            try
            {
                SysUsersService usersService = new SysUsersService();
                bool flag = await usersService.DelUser(UserId);
                if (flag)
                {
                    result.Code = 1;
                    result.Message = "用户删除成功";
                }
                else
                {
                    result.Code = 0;
                    result.Message = "用户删除失败";
                }
            }
            catch (Exception e)
            {
                result.Code = -1;
                result.Message = $"删除用户发生异常,错误信息为{e.Message}";
            }

            return result;

        }
        #endregion

        #region 获取用户信息
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> GetUserInfo([FromQuery] int UserId)
        {
            Result result = new Result();
            try
            {
                SysUsersService usersService = new SysUsersService();
                UserEditInfo info = await usersService.GetUserInfo(UserId);
                if (ReferenceEquals(null, info))
                {
                    result.Code = 0;
                    result.Message = "无数据";
                }
                else
                {
                    result.Code = 1;
                    result.Message = "用户数据获取成功";
                    result.Object = info;
                }
            }
            catch (Exception exception)
            {
                result.Code = -1;
                result.Message = exception.Message;
            }
            return result;
        }
        #endregion

        #region 修改用户信息
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> UpdateUserInfo([FromBody] UserEditInfo info)
        {
            Result result = new Result();
            try
            {
                if (ReferenceEquals(null, info))
                {
                    result.Code = 0;
                    result.Message = "参数错误";
                }
                else
                {
                    SysUsersService usersService = new SysUsersService();
                    bool isSuccess = await usersService.UpdateUserInfo(info);
                    result.Code = isSuccess ? 1 : 0;
                    result.Message = isSuccess ? "用户更新成功" : "用户更新失败";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return result;
        }
        #endregion
    }
}
