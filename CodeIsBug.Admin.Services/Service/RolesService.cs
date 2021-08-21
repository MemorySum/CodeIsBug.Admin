using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeIsBug.Admin.Models.Dto;
using CodeIsBug.Admin.Models.Models;
using CodeIsBug.Admin.Services.Base;

namespace CodeIsBug.Admin.Services.Service
{
    /// <summary>
    ///     角色Service
    /// </summary>
    public class RolesService : BaseService<ESysRoles>
    {
        public async Task<List<ESysRoles>> GetRoles()
        {
            return await Context.Queryable<ESysRoles>()
                .OrderBy(role => role.Sort)
                .ToTreeAsync(role => role.Children,
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
                AddTime = Context.GetDate()
            };
            return await Context.Insertable(roles).ExecuteCommandIdentityIntoEntityAsync();
        }

        public async Task<bool> IsHasChildren(Guid roleGuid)
        {
            return await Context.Queryable<ESysRoles>().AnyAsync(x => x.ParentId.Equals(roleGuid));
        }

        public async Task<bool> DelRole(Guid roleGuid)
        {
            return await Context.Deleteable<ESysRoles>(x => x.RoleId.Equals(roleGuid)).ExecuteCommandHasChangeAsync();
        }

        public async Task<RoleEditInfo> GetRoleInfo(Guid roleGuid)
        {
            return await Context.Queryable<ESysRoles>()
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
            var role = new ESysRoles
            {
                RoleId = info.RoleId,
                ParentId = info.ParentId,
                Name = info.Name,
                Sort = int.Parse(info.Sort),
                Remark = info.Remark,
                ModifyTime = Context.GetDate()
            };
            return await Context.Updateable(role).IgnoreColumns(x => x.AddTime).ExecuteCommandHasChangeAsync();
        }
    }
}