using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models.Dto;
using CodeIsBug.Admin.Models.Models;
using CodeIsBug.Admin.Services.Base;
using SqlSugar;
using StackExchange.Redis;

namespace CodeIsBug.Admin.Services.Service
{
    public class RoleMenuMapService : BaseService<ESysRoleMenuMap>
    {


        public async Task<List<Guid>> GetMenuListByRoleId(Guid roleGuid)
        {

            //return await Context.Queryable<ESysRoleMenuMap, ESysMenu, ESysRoles>((map, menu, role) =>
            //        new JoinQueryInfos(JoinType.Left, map.MenuId.Equals(menu.MenuId),
            //            JoinType.Left, map.RoleId.Equals(role.RoleId)))
            //    .Where(map => map.RoleId.Equals(roleGuid))
            //    .Select((map, menu, role) => menu.MenuId).ToListAsync();
            var data = await Context.Queryable<ESysRoles, ESysRoleMenuMap, ESysMenu>((role, rolemap, menu) => new JoinQueryInfos(JoinType.Left, role.RoleId == rolemap.RoleId,
                JoinType.Inner, rolemap.MenuId == menu.MenuId))
                .Where((role, rolemap, menu) => role.RoleId == roleGuid)
                .Distinct().Select((role, rolemap, menu) => menu.MenuId).ToListAsync();
            return data;
        }

        public async Task<bool> SaveRoleMenuInfo(RoleMenuMapSaveDto saveDto)
        {
            var mapList = new List<ESysRoleMenuMap>();
            if (saveDto.SelectMenuIds.Any())
                foreach (var item in saveDto.SelectMenuIds)
                {
                    var map = new ESysRoleMenuMap
                    {
                        MapId = GuidHelper.GenerateGuid(),
                        MenuId = item,
                        RoleId = saveDto.RoleId
                    };
                    mapList.Add(map);
                }

            try
            {
                Context.Ado.BeginTran();
                await Context.Deleteable<ESysRoleMenuMap>().Where(x => x.RoleId.Equals(saveDto.RoleId)).ExecuteCommandHasChangeAsync();
                await Context.Insertable(mapList).UseSqlServer().ExecuteBlueCopyAsync();
                Context.Ado.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                Context.Ado.RollbackTran();
                return false;
            }
        }
    }
}
