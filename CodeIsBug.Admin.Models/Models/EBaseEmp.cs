using System;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;
using SqlSugar;

namespace CodeIsBug.Admin.Models.Models
{
    [SugarTable("e_Base_Emp")]
    public class EBaseEmp 
    {
        [SugarColumn(IsNullable=false, IsPrimaryKey = true, ColumnDescription = "用户Id")]
        public Guid UserId { get; set; }
        [SugarColumn(IsNullable = false, ColumnDescription = "用户姓名")]
        public string Name { get; set; }
        [SugarColumn(IsNullable = false, ColumnDescription = "账号")]
        public string UserName { get; set; }
        [SugarColumn(IsNullable = true, ColumnDescription = "手机号")]
        public string Phone { get; set; }
        [SugarColumn(IsNullable = true, ColumnDescription ="邮箱")]
        public string Email { get; set; }
        [SugarColumn(ColumnDescription = "密码",IsNullable = false)]
        public string Pwd { get; set; }
        [SugarColumn(ColumnDescription = "添加时间",IsNullable = false)]
        public DateTime AddTime { get; set; }
        [SugarColumn(IsNullable = true, ColumnDescription = "修改时间")]
        public DateTime? ModifyTime { get; set; }
    }
}
