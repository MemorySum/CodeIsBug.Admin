using System;
using SqlSugar;

namespace CodeIsBug.Admin.Models.Models
{
    [SugarTable("e_Sys_EmpRoleMap")]
    public partial class ESysEmpRoleMap
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, ColumnDescription = "主键Id")]
        public Guid Id { get; set; }
        [SugarColumn(IsNullable = true,ColumnDescription = "用户Id")]
        public Guid? EmpId { get; set; }
        [SugarColumn(IsNullable = true, ColumnDescription = "角色Id")]
        public Guid? RoleId { get; set; }
    }
}
