using System;
using System.Collections.Generic;
using SqlSugar;

namespace CodeIsBug.Admin.Models.Models
{
    [SugarTable("e_Sys_Menu")]
    public class ESysMenu
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, ColumnDescription = "菜单Id")]
        public Guid MenuId { get; set; }

        [SugarColumn(IsNullable = true, ColumnDescription = "父级Id")]
        public Guid? ParentId { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "菜单名称")]
        public string Name { get; set; }

        [SugarColumn(IsNullable = true, ColumnDescription = "菜单地址")]
        public string Url { get; set; }

        [SugarColumn(IsNullable = true, ColumnDescription = "菜单图标")]
        public string Icon { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "菜单排序")]
        public int Sort { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "菜单层级")]
        public int Level { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "添加时间")]
        public DateTime AddTime { get; set; }

        [SugarColumn(IsNullable = true, ColumnDescription = "修改时间")]
        public DateTime? ModifyTime { get; set; }

        [SugarColumn(IsIgnore = true)] public List<ESysMenu> Children { get; set; }
    }
}