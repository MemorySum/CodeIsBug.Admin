﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models.Dto;
using CodeIsBug.Admin.Models.DTO;
using CodeIsBug.Admin.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CodeIsBug.Admin.Api.Controllers
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : ControllerBase
    {

        #region 菜单列表
        /// <summary>
        /// 菜单列表
        /// </summary>
        /// <param name="query">查询字符串</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        [HttpGet]
        public Result GetMenus(string query, int pageIndex, int pageSize)
        {
            Result res = new Result();
            MenuService menuService = new MenuService();
            int totalCount = 0;
            try
            {
                var result = menuService.GetMenus(query, pageIndex, pageSize, ref totalCount);

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

        #region 添加菜单
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="inputInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> AddMenu([FromBody] MenuInputInfo inputInfo)
        {
            Result result = new Result();
            try
            {
                if (inputInfo == null)
                {
                    result.Code = 0;
                    result.Message = "参数错误";
                }
                else
                {
                    bool isSuccess = await new MenuService().AddMenu(inputInfo);
                    if (isSuccess)
                    {
                        result.Code = 1;
                        result.Message = "菜单添加成功";
                    }
                    else
                    {
                        result.Code = 0;
                        result.Message = "菜单添加失败";
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

        #region 修改菜单信息

        /// <summary>
        /// 修改菜单信息
        /// </summary>
        /// <param name="inputInfo">修改参数信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> UpdateMenu([FromBody] MenuInputInfo inputInfo)
        {
            Result result = new Result();
            MenuService menuService = new MenuService();
            try
            {
                if (inputInfo == null)
                {
                    result.Code = 0;
                    result.Message = "参数错误";
                }
                else
                {
                    var menuinfo = await menuService.GetMenuInfo(inputInfo.MenuId);

                    if (menuinfo == null)
                    {
                        result.Code = 0;
                        result.Message = "未获取到菜单信息";
                    }
                    else
                    {
                        bool isSuccess = await menuService.UpdateMenu(inputInfo);
                        if (isSuccess)
                        {
                            result.Code = 1;
                            result.Message = "菜单修改成功";
                        }
                        else
                        {
                            result.Code = 0;
                            result.Message = "菜单修改失败";
                        }
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

        #region 删除菜单
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> DelMenu([FromQuery] int menuId)
        {
            try
            {
                bool flag = await new MenuService().DelMenu(menuId);
                if (flag)
                {
                    return new Result { Code = 1, Message = "菜单删除成功" };
                }

                return new Result { Code = 0, Message = "菜单删除失败" };
            }
            catch (Exception e)
            {
                return new Result { Code = -1, Message = e.Message };
            }

        }
        #endregion

        #region 获取所有一级菜单
        /// <summary>
        /// 获取所有一级菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> GetAllFirstLevelMenu()
        {
            Result r = new Result();
            MenuService menuService = new MenuService();
            try
            {
                var menuList = await menuService.GetAllFirstLevelMenu();
                r.Code = 1;
                r.Message = "菜单获取成功";
                r.Object = menuList;
            }
            catch (Exception e)
            {
                r.Code = -1;
                r.Message = e.Message;
            }

            return r;
        }
        #endregion

        #region 获取单个菜单信息
        /// <summary>
        /// 获取单个菜单信息
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> GetMenuInfo([FromQuery] int menuId)
        {
            Result r = new Result();
            try
            {
                var menuInfo = await new MenuService().GetMenuInfo(menuId);
                MenuInputInfo menu = new MenuInputInfo()
                {
                    MenuId = menuInfo.MenuId,
                    ParentId = menuInfo.ParentId ?? 0,
                    Name = menuInfo.Name,
                    Icon = menuInfo.Icon,
                    Sort = menuInfo.Sort,
                    Level = menuInfo.Level,
                    Url = menuInfo.Url
                };
                r.Code = 1;
                r.Message = "菜单信息获取成功";
                r.Object = menu;
            }
            catch (Exception e)
            {
                r.Code = -1;
                r.Message = e.Message;
            }

            return r;
        }
        #endregion

        #region 左侧菜单列表返回
        /// <summary>
        /// 左侧菜单列表返回
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Result GetAllMenuForIndex()
        {
            Result res = new Result();
            MenuService menuService = new MenuService();
            try
            {
                List<MenuDto> menus = menuService.BuildMenu();
                res.Code = 1;
                res.Message = "菜单获取成功";
                res.Object = JsonConvert.SerializeObject(menus);
            }
            catch (Exception ex)
            {
                res.Code = 0;
                res.Message = $"菜单获取失败，错误为:{ex.Message}";
            }

            return res;
        }
        #endregion
    }
}