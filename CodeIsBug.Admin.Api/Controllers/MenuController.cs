using CodeIsBug.Admin.Api.DTO;
using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models;
using CodeIsBug.Admin.Models.DbContext;
using CodeIsBug.Admin.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeIsBug.Admin.Models.Models;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SQLitePCL;

namespace CodeIsBug.Admin.Api.Controllers
{
	[Authorize]
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class MenuController : ControllerBase
	{
		#region 构造参数-DI注入
		private readonly CodeIsBugContext _context;

		public MenuController(CodeIsBugContext context)
		{
			_context = context;
		}
		#endregion

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
			try
			{
				var result = from c in _context.ESysMenus
							 from s in _context.ESysMenus
							 where c.MenuId == s.ParentId
							 select new MenuOutputInfo()
							 {
								 MenuId = c.MenuId,
								 ParentId = c.ParentId ?? Guid.Empty,
								 Name = c.Name,
								 Icon = c.Icon,
								 IsDeleted = c.IsDeleted,
								 Level = c.Level,
								 ModifyTime = c.ModifyTime,
								 Sort = c.Sort,
								 Url = c.Url,
								 AddTime = c.AddTime,
								 children = new List<MenuOutputInfo>
								 {
									 new MenuOutputInfo
									 {
										 MenuId =   s.MenuId,
										 ParentId= s.ParentId?? Guid.Empty,
										 Name= s.Name,
										 Icon= s.Icon,
										 IsDeleted= s.IsDeleted,
										 Level=    s.Level,
										 ModifyTime= s.ModifyTime,
										 Sort= s.Sort,
										 Url= s.Url,
										 AddTime=s.AddTime
									 }
								 }

							 };

				if (!string.IsNullOrEmpty(query))
				{
					result = result.Where(a => a.Name.Contains(query));
				}

				var count = result.Count();
				res.Object = new
				{
					data = result.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
					count = count
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

					ESysMenu menu = new ESysMenu()
					{
						MenuId = Guid.NewGuid(),
						ParentId = inputInfo.ParentId,
						Name = inputInfo.Name,
						Icon = inputInfo.Icon,
						Sort = inputInfo.Sort,
						IsDeleted = inputInfo.IsDeleted,
						Level = inputInfo.Level,
						Url = inputInfo.Url,
						AddTime = DateTime.Now
					};
					await _context.ESysMenus.AddAsync(menu);
					await _context.SaveChangesAsync();
					result.Code = 1;
					result.Message = "菜单添加成功";
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
			try
			{
				if (inputInfo == null)
				{
					result.Code = 0;
					result.Message = "参数错误";
				}
				else
				{
					var menuinfo = await _context.ESysMenus.FirstOrDefaultAsync(x => x.MenuId == inputInfo.MenuId);

					if (menuinfo == null)
					{
						result.Code = 0;
						result.Message = "未获取到菜单信息";
					}
					else
					{
						menuinfo.Level = inputInfo.Level;
						menuinfo.ParentId = inputInfo.ParentId;
						menuinfo.Name = inputInfo.Name;
						menuinfo.Icon = inputInfo.Icon;
						menuinfo.Sort = inputInfo.Sort;
						menuinfo.IsDeleted = inputInfo.IsDeleted;
						menuinfo.Level = inputInfo.Level;
						menuinfo.Url = inputInfo.Url;
						menuinfo.ModifyTime = DateTime.Now;

						_context.ESysMenus.Update(menuinfo);
						await _context.SaveChangesAsync();
						result.Code = 1;
						result.Message = "菜单修改成功";
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
		/// <param name="menuGuid">菜单Id</param>
		/// <returns></returns>
		[HttpGet]

		public async Task<Result> DelMenu([FromQuery] Guid menuGuid)
		{

			try
			{
				if (menuGuid == Guid.Empty) { return new Result { Code = 0, Message = "参数错误" }; }

				var menuInfo = await _context.ESysMenus.FirstOrDefaultAsync(x => x.MenuId == menuGuid);
				_context.ESysMenus.Remove(menuInfo);
				await _context.SaveChangesAsync();
				return new Result { Code = 1, Message = "菜单删除成功" };

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
			try
			{
				var menuList = await _context.ESysMenus.Where(x => x.Level == 0).Select(a => new
				{
					a.MenuId,
					a.Name
				}).ToListAsync();
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
		[HttpGet]
		public async Task<Result> GetMenuInfo([FromQuery] Guid menuGuid)
		{
			Result r = new Result();
			try
			{
				var menuInfo = await _context.ESysMenus.FirstOrDefaultAsync(x => x.MenuId == menuGuid);
				MenuInputInfo menu = new MenuInputInfo()
				{
					MenuId = menuInfo.MenuId,
					ParentId = menuInfo.ParentId ?? Guid.Empty,
					Name = menuInfo.Name,
					Icon = menuInfo.Icon,
					Sort = menuInfo.Sort,
					IsDeleted = menuInfo.IsDeleted,
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
			try
			{
				List<MenuDto> menus = buildMenu();
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

		#region 递归无限层级菜单
		private List<MenuDto> buildMenu()
		{
			var menuinfo = _context.ESysMenus.ToList();
			if (!menuinfo.Any()) { return null; }
			List<MenuDto> menuDtos = new List<MenuDto>();
			List<ESysMenu> firstIndexMenuList = menuinfo.Where(x => x.Level == 0 && x.IsDeleted == 0).OrderBy(x => x.Sort).ToList();
			foreach (var item in firstIndexMenuList)
			{
				MenuDto dto = new MenuDto
				{
					id = item.MenuId,
					menuName = item.Name,
					path = item.Url,
					icon = item.Icon,
					children = GetChildrenMenu(menuinfo, item.MenuId)
				};
				menuDtos.Add(dto);
			}
			return menuDtos;
		}

		private List<MenuDto> GetChildrenMenu(List<ESysMenu> menuInfo, Guid parentGuid)
		{

			List<ESysMenu> childrenMenuList = menuInfo.Where(x => x.ParentId == parentGuid).OrderBy(x => x.Sort).ToList();
			if (!childrenMenuList.Any()) return null;
			List<MenuDto> menuDtos = new List<MenuDto>();
			foreach (var item in childrenMenuList)
			{
				MenuDto dto = new MenuDto
				{
					id = item.MenuId,
					menuName = item.Name,
					path = item.Url,
					icon = item.Icon,
					children = GetChildrenMenu(menuInfo, item.MenuId)
				};

				menuDtos.Add(dto);
			}
			return menuDtos;
		}
		#endregion
	}
}
