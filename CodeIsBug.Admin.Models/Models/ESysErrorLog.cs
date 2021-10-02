using CodeIsBug.Admin.Models.Enums;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeIsBug.Admin.Models.Models
{
    /// <summary>
    /// 系统日志表
    /// </summary>
    [SugarTable("E_Sys_ErrorLog")]
    public class ESysErrorLog
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, ColumnDescription = "主键")]
        public Guid Id { get; set; }
        /// <summary>
        /// 日志类型
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "日志类型")]
        public LogTypeEnum LogType { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "记录时间")]
        public DateTime LogTime { get; set; }
        /// <summary>
        /// 错误位置
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "错误位置")]
        public string DisplayName { get; set; }
        /// <summary>
        /// 错误行号
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "错误行号")]
        public int LineNumber { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "错误信息", Length = 200)]
        public string Message { get; set; }
        /// <summary>
        /// 错误详情
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "错误详情",Length =5000)]
        public string MessageDetails {  get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "创建时间")]
        public DateTime CreateTime {  get; set; }
    }
}
