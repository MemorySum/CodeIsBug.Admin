using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeIsBug.Admin.Models.DbContext;
using CodeIsBug.Admin.Models.Dto;
using CodeIsBug.Admin.Models.Models;

namespace CodeIsBug.Admin.Services.Service
{
    /// <summary>
    /// 角色Service
    /// </summary>
    public class RolesService : DataContext<ESysRoles>
    {
        public List<ESysRoles> GetRoles()
        {
            return Db.Queryable<ESysRoles>()
                .OrderBy(role => role.Sort)
                .ToTree(role => role.Children,
                    role => role.ParentId,
                    Guid.Empty);
        }

        public async Task<bool> AddRole(RoleAddDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            var roles = new ESysRoles
            {
                ParentId = dto.ParentId,
                Name = dto.Name,
                Sort = dto.Sort,
                Remark = dto.Remark,
                AddTime = Db.GetDate()
            };
            return await Db.Insertable<ESysRoles>(roles).ExecuteCommandIdentityIntoEntityAsync();
        }

        public async Task<bool> IsHasChildren(Guid roleGuid)
        {
            return await Db.Queryable<ESysRoles>().AnyAsync(x => x.ParentId.Equals(roleGuid));
        }

        public async Task<bool> DelRole(Guid roleGuid)
        {
            return await Db.Deleteable<ESysRoles>(x => x.RoleId.Equals(roleGuid)).ExecuteCommandHasChangeAsync();
        }

        public async Task<RoleEditInfo> GetRoleInfo(Guid roleGuid)
        {
            return await Db.Queryable<ESysRoles>()
                .Select(role => new RoleEditInfo
                {
                    RoleId = role.RoleId,
                    Name = role.Name,
                    ParentId = role.ParentId ?? Guid.Empty,
                    Sort = role.Sort.ToString(),
                    Remark = role.Remark
                }).FirstAsync(x => x.RoleId.Equals(roleGuid));
        }

        public async Task<bool> EditRoleInfo(RoleEditInfo info)
        {
            ESysRoles role = new ESysRoles
            {
                RoleId = info.RoleId,
                ParentId = info.ParentId,
                Name = info.Name,
                Sort = int.Parse(info.Sort),
                Remark = info.Remark,
                ModifyTime = Db.GetDate()
            };
            return await Db.Updateable(role).IgnoreColumns(x => x.AddTime).ExecuteCommandHasChangeAsync();
        }

    }
}