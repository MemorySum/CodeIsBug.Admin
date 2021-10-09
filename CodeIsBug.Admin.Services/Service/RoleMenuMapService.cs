using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models.Dto;
using CodeIsBug.Admin.Models.Models;
using CodeIsBug.Admin.Services.Base;
using SqlSugar;
using SqlSugar.IOC;

namespace CodeIsBug.Admin.Services.Service
{
    public class RoleMenuMapService : BaseService<ESysRoleMenuMap>
    {
        public async Task<List<Guid>> GetMenuListByRoleId(Guid roleGuid)
        {
            return await Context.Queryable<ESysRoles>()
                 .LeftJoin<ESysRoleMenuMap>((role, rolemap) => role.RoleId == rolemap.RoleId)
                 .LeftJoin<ESysMenu>((role, rolemap, menu) => rolemap.MenuId == menu.MenuId)
                 .Where((role, rolemap, menu) => role.RoleId == roleGuid)
                 .Distinct()
                 .Select((role, rolemap, menu) => menu.MenuId).ToListAsync();
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
                DbScoped.SugarScope.BeginTran();
                await Context.Deleteable<ESysRoleMenuMap>().Where(x => x.RoleId.Equals(saveDto.RoleId))
                    .ExecuteCommandAsync();
                await Context.Insertable(mapList).UseSqlServer().ExecuteBulkCopyAsync();
                DbScoped.SugarScope.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                DbScoped.SugarScope.RollbackTran();
                return false;
            }
        }
    }
}