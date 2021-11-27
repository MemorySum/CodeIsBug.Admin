using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models.Enums;
using CodeIsBug.Admin.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SqlSugar.IOC;
using System;

namespace CodeIsBug.Admin.Api.Extensions
{
    public class GlobalExceptionFilter : IExceptionFilter
    {

        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
             {
                Exception ex = context.Exception;
                //错误所在的控制器方法名称
                var DisplayName = context.ActionDescriptor.DisplayName;
                #region 错误所在的行号行号
                ////行号前的名称有的是中文，有的是英文，注意甄别
                const string lineSearch = ":line ";
                var index = ex.StackTrace.LastIndexOf(lineSearch);
                if (index != -1)
                {
                    var lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length);
                    var lineNumberStr = lineNumberText.Substring(0, lineNumberText.IndexOf("\r\n"));
                    int.TryParse(lineNumberStr, out int lineNumber);
                    ESysErrorLog md = new ESysErrorLog
                    {
                        LogTime = DateTime.Now,
                        LogType = LogTypeEnum.Error,
                        DisplayName = DisplayName,  //错误位置
                        LineNumber = lineNumber,  //错误行号
                        Message = ex.Message,    //错误信息
                        MessageDetails = ex.ToString(),  //错误详情
                        CreateTime = DateTime.Now
                    };
                    DbScoped.SugarScope.GetSimpleClient<ESysErrorLog>().Insert(md);
                    context.Result = new ContentResult
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Content = Newtonsoft.Json.JsonConvert.SerializeObject(
                            ApiResultHelper.Failed(md.Message,
                            StatusCodes.Status500InternalServerError)),
                        ContentType = "application/json"
                    };
                }
                #endregion
            }
            context.ExceptionHandled = true;


        }
    }
}
