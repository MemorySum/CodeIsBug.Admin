using System;
using SqlSugar;

namespace CodeIsBug.Admin.Models.Models
{
    [SugarTable("e_Sys_Role")]
    public  class ESysRoles
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true,ColumnDescription = "角色Id")]
        public int Id { get; set; }

        [SugarColumn(IsNullable = false,ColumnDescription = "父级Id")]
        public int ParentId { get; set; }

        [SugarColumn(IsNullable = false,ColumnDescription = "角色名称")]
        public string Name { get; set; }

        [SugarColumn(IsNullable = false,ColumnDescription = "角色排序")]
        public int Sort { get; set; }
        
        [SugarColumn(IsNullable = true,ColumnDescription = "备注")]
        public string Remark { get; set; }

        [SugarColumn(IsNullable = false,ColumnDescription = "添加时间")]
        public DateTime AddTime { get; set; }

        [SugarColumn(IsNullable = true,ColumnDescription = "修改时间")]
        public DateTime? ModifyTime { get; set; }
    }
}
