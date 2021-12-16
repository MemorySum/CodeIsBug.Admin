using System;
using SqlSugar;

namespace CodeIsBug.Admin.Models.Models
{
    [SugarTable("e_sys_rolemenumap")]
    public class ESysRoleMenuMap
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, ColumnDescription = "主键Id")]
        public Guid MapId { get; set; }

        [SugarColumn(IsNullable = true, ColumnDescription = "角色id")]
        public Guid? RoleId { get; set; }

        [SugarColumn(IsNullable = true, ColumnDescription = "菜单id")]
        public Guid? MenuId { get; set; }
    }
}