﻿using System;
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
    /// <summary>
    ///     用户角色对照service
    /// </summary>
    public class EmpRoleMapService : BaseService<ESysEmpRoleMap>
    {
        /// <summary>
        ///     根据列表选择的用户guid加载对应的角色guid
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public async Task<List<Guid>> GetUserRolesByUserId(Guid userGuid)
        {
            return await Context.Queryable<ESysEmpRoleMap, EBaseEmp>((map, emp) =>
                    new JoinQueryInfos(JoinType.Left, map.EmpId.Equals(emp.UserId)))
                .Where(map => map.EmpId.Equals(userGuid))
                .Select((map, emp) => map.RoleId.Value).ToListAsync();
        }

        /// <summary>
        ///     保存用户角色
        /// </summary>
        /// <param name="saveDto"></param>
        /// <returns></returns>
        public async Task<bool> SaveRoleId(EmpRoleMapSaveDto saveDto)
        {
            var mapList = new List<ESysEmpRoleMap>();
            if (saveDto.SelectRoleIds.Any())
                foreach (var item in saveDto.SelectRoleIds)
                {
                    var map = new ESysEmpRoleMap
                    {
                        Id = GuidHelper.GenerateGuid(),
                        EmpId = saveDto.UserId,
                        RoleId = item 
                    };
                    mapList.Add(map);
                }

            try
            {
                Context.Ado.BeginTran();
                await Context.Deleteable<ESysEmpRoleMap>().Where(x => x.EmpId.Equals(saveDto.UserId))
                    .ExecuteCommandAsync();
                await Context.Insertable(mapList).UseSqlServer().ExecuteBlukCopyAsync();
                Context.Ado.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                Context.Ado.RollbackTran();
                return false;
            }
        }

        /// <summary>
        ///     获取用户对应角色的菜单权限
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public async Task<List<ESysMenu>> GetUserRoleMenu(Guid userGuid)
        {
            var data = await Context.Queryable<EBaseEmp, ESysEmpRoleMap, ESysRoles, ESysRoleMenuMap, ESysMenu>(
                    (emp, emprolemap, role, rolemenumap, menu) => new JoinQueryInfos(
                        JoinType.Left, emp.UserId == emprolemap.EmpId,
                        JoinType.Left, emprolemap.RoleId == role.RoleId,
                        JoinType.Left, role.RoleId == rolemenumap.RoleId,
                        JoinType.Inner, rolemenumap.MenuId == menu.MenuId))
                .Where((emp, emprolemap, role, rolemenumap, menu) => emp.UserId == userGuid)
                .Select((emp, emprolemap, role, rolemenumap, menu) => menu)
                .ToTreeAsync(menu => menu.Children, x => x.ParentId, Guid.Empty);
            return data;
        }

        public async Task<string> GetUserRoleName(Guid userGuid)
        {
            var roleNameList = await Context.Queryable<EBaseEmp, ESysEmpRoleMap, ESysRoles>(
                    (emp, emprolemap, role) =>
                        new JoinQueryInfos(JoinType.Left, emp.UserId.Equals(emprolemap.EmpId),
                            JoinType.Left, emprolemap.RoleId.Equals(role.RoleId)))
                .Where((emp, emprolemap, role) => emp.UserId.Equals(userGuid))
                .Select((emp, emprolemap, role) => role.Name).ToListAsync();
            return string.Join(',', roleNameList);
        }
    }
}