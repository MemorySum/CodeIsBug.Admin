using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeIsBug.Admin.Models.Dto;
using CodeIsBug.Admin.Models.Models;
using CodeIsBug.Admin.Services.Base;
using SqlSugar;

namespace CodeIsBug.Admin.Services.Service
{
    public class MenuService : BaseService<ESysMenu>
    {

        public async Task<List<MenuDto>> BuildMenuForIndex()
        {
            var menuInfo = await Db.Queryable<ESysMenu>().ToListAsync();
            if (!menuInfo.Any() && menuInfo.Count <= 0) { return null; }
            List<MenuDto> menuDots = new List<MenuDto>();
            List<ESysMenu> firstIndexMenuList = menuInfo.Where(x => x.Level == 0).OrderBy(x => x.Sort).ToList();
            foreach (var item in firstIndexMenuList)
            {
                MenuDto dto = new MenuDto
                {
                    id = item.MenuId,
                    menuName = item.Name,
                    path = item.Url,
                    icon = item.Icon,
                    Children = GetChildrenMenuForIndex(menuInfo, item.MenuId)
                };
                menuDots.Add(dto);
            }
            return menuDots;
        }

        private List<MenuDto> GetChildrenMenuForIndex(List<ESysMenu> menuInfo, Guid parentGuid)
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
                    Children = GetChildrenMenuForIndex(menuInfo, item.MenuId)
                };

                menuDtos.Add(dto);
            }
            return menuDtos;
        }

        public List<ESysMenu> GetMenus()
        {
            return Db.Queryable<ESysMenu>().OrderBy(sys => sys.Sort, OrderByType.Asc)
                .ToTree(sys => sys.Children, sys => sys.ParentId, Guid.Empty);
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
                ParentId = inputInfo.ParentId ?? Guid.Empty,
                Name = inputInfo.Name,
                Icon = inputInfo.Icon,
                Sort = inputInfo.Sort,
                Level = inputInfo.Level,
                Url = inputInfo.Url,
                AddTime = Db.GetDate()
            };
            bool b = await Db.Insertable(menu).ExecuteCommandIdentityIntoEntityAsync();
            return b;

        }

        public async Task<ESysMenu> GetMenuInfo(Guid inputInfoMenuId)
        {
            return await Db.Queryable<ESysMenu>().FirstAsync(a => a.MenuId.Equals(inputInfoMenuId));
        }

        public async Task<bool> UpdateMenu(MenuInputInfo inputInfo)
        {
            return await Db.Updateable<ESysMenu>()
                .SetColumns(it => new ESysMenu
                {
                    Name = inputInfo.Name,
                    Icon = inputInfo.Icon,
                    Sort = inputInfo.Sort,
                    Level = inputInfo.Level,
                    Url = inputInfo.Url,
                    ModifyTime = DateTime.Now,
                    ParentId = inputInfo.ParentId.Value
                }).IgnoreColumns(x => x.AddTime)
                .Where(x => x.MenuId.Equals(inputInfo.MenuId))
                .ExecuteCommandHasChangeAsync();

        }

        public async Task<bool> DelMenu(Guid menuId)
        {
            return await Db.Deleteable<ESysMenu>().Where(a => a.MenuId.Equals(menuId)).ExecuteCommandHasChangeAsync();
        }
    }
}
