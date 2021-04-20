using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models.Dto;
using CodeIsBug.Admin.Models.Models;
using CodeIsBug.Admin.Services.Base;
using SqlSugar;
namespace CodeIsBug.Admin.Services.Service
{
    public class RoleMenuMapService : BaseService<ESysRoleMenuMap>
    {


        public async Task<List<Guid>> GetMenuListByRoleId(Guid roleGuid)
        {

            return await Db.Queryable<ESysRoleMenuMap, ESysMenu, ESysRoles>((map, menu, role) =>
                    new JoinQueryInfos(JoinType.Left, map.MenuId.Equals(menu.MenuId),
                        JoinType.Left, map.RoleId.Equals(role.RoleId)))
                .Where(map => map.RoleId.Equals(roleGuid))
                .Select((map, menu, role) => menu.MenuId).ToListAsync();
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
                Db.Ado.BeginTran();
                await Db.Deleteable<ESysRoleMenuMap>().Where(x => x.RoleId.Equals(saveDto.RoleId)).ExecuteCommandHasChangeAsync();
                await Db.Insertable(mapList).UseSqlServer().ExecuteBlueCopyAsync();
                Db.Ado.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                Db.Ado.RollbackTran();
                return false;
            }
        }
    }
}
