using SqlSugar;

namespace CodeIsBug.Admin.Models.Models
{
    [SugarTable("e_Sys_EmpRoleMap")]
    public partial class ESysEmpRoleMap
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true,ColumnDescription = "主键Id")]
        public int Id { get; set; }
        [SugarColumn(IsNullable = true,ColumnDescription = "用户Id")]
        public int? EmpId { get; set; }
        [SugarColumn(IsNullable = true, ColumnDescription = "角色Id")]
        public int? RoleId { get; set; }
    }
}
