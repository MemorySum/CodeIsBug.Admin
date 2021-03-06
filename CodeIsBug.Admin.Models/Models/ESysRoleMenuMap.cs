using System;
using System.Security.AccessControl;
using SqlSugar;

namespace CodeIsBug.Admin.Models.Models
{
    [SugarTable("E_Sys_RoleMenuMap")]
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