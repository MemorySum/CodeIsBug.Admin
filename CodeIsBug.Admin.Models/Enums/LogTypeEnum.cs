using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeIsBug.Admin.Models.Enums
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogTypeEnum
    {
        /// <summary>
        /// 错误日志
        /// </summary>
        [Description("错误日志")]
        Error = 1,
        /// <summary>
        /// 操作日志
        /// </summary>
        [Description("操作日志")]
        Operator = 2
    }
}
