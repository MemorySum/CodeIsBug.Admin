using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeIsBug.Admin.Models.DbContext;
using CodeIsBug.Admin.Models.Dto;
using CodeIsBug.Admin.Models.DTO;
using CodeIsBug.Admin.Models.Models;

namespace CodeIsBug.Admin.Services.Service
{
    public class MenuService : DataContext<ESysMenu>
    {

        public List<MenuDto> BuildMenu()
        {
            var menuInfo = Db.Queryable<ESysMenu>().ToList();
            if (!menuInfo.Any() && menuInfo.Count <= 0) { return null; }
            List<MenuDto> menuDots = new List<MenuDto>();
            List<ESysMenu> firstIndexMenuList = menuInfo.Where(x => x.Level == 0 ).OrderBy(x => x.Sort).ToList();
            foreach (var item in firstIndexMenuList)
            {
                MenuDto dto = new MenuDto
                {
                    id = item.MenuId,
                    menuName = item.Name,
                    path = item.Url,
                    icon = item.Icon,
                    Children = GetChildrenMenu(menuInfo, item.MenuId)
                };
                menuDots.Add(dto);
            }
            return menuDots;
        }

        private List<MenuDto> GetChildrenMenu(List<ESysMenu> menuInfo, int parentGuid)
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
                    Children = GetChildrenMenu(menuInfo, item.MenuId)
                };

                menuDtos.Add(dto);
            }
            return menuDtos;
        }

        public  List<MenuOutputInfo> GetMenus(string query, int pageIndex, int pageSize, ref int totalCount)
        {

            var list = Db.Queryable<ESysMenu>()
                .Select(pm => new MenuOutputInfo
                {
                    MenuId = pm.MenuId,
                    Name = pm.Name,
                    Icon = pm.Icon,
                    Level = pm.Level,
                    ModifyTime = pm.ModifyTime,
                    Sort = pm.Sort,
                    Url = pm.Url,
                    AddTime = pm.AddTime
                }).WhereIF(!string.IsNullOrEmpty(query), pm => pm.Name.Contains(query)).ToPageList(pageIndex, pageSize, ref totalCount);
            return list;
        }

        public async Task<List<MenuFirstLevelInfo>> GetAllFirstLevelMenu()
        {
            var list = await Db.Queryable<ESysMenu>().Where(a => a.Level == 0).Select(a => new MenuFirstLevelInfo { MenuId = a.MenuId, Name = a.Name }).ToListAsync();
            return list;
        }

        public async Task<bool> AddMenu(MenuInputInfo inputInfo)
        {
            ESysMenu menu = new ESysMenu()
            {
                
                ParentId = inputInfo.ParentId,
                Name = inputInfo.Name,
                Icon = inputInfo.Icon,
                Sort = inputInfo.Sort,
                Level = inputInfo.Level,
                Url = inputInfo.Url,
                AddTime = DateTime.Now
            };
            bool b = await Db.Insertable(menu).ExecuteCommandAsync() > 0;
            return b;
        }

        public async Task<ESysMenu> GetMenuInfo(int inputInfoMenuId)
        {
            return await Db.Queryable<ESysMenu>().FirstAsync(a => a.MenuId.Equals(inputInfoMenuId));
        } 

        public async Task<bool> UpdateMenu(MenuInputInfo inputInfo)
        {
            ESysMenu menuinfo = new ESysMenu();
            menuinfo.Level = inputInfo.Level;
            menuinfo.ParentId = inputInfo.ParentId;
            menuinfo.Name = inputInfo.Name;
            menuinfo.Icon = inputInfo.Icon;
            menuinfo.Sort = inputInfo.Sort;
            menuinfo.Level = inputInfo.Level;
            menuinfo.Url = inputInfo.Url;
            menuinfo.ModifyTime = DateTime.Now;
            menuinfo.MenuId = inputInfo.MenuId;
            return await Db.Updateable<ESysMenu>(menuinfo).ExecuteCommandHasChangeAsync();

        }

        public async Task<bool> DelMenu(int menuId)
        {
            return await Db.Deleteable<ESysMenu>().Where(a => a.MenuId.Equals(menuId)).ExecuteCommandHasChangeAsync();
        }
    }
}
