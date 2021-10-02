using System;
using System.Linq;
using CodeIsBug.Admin.Models.Models;
using SqlSugar;
using SqlSugar.IOC;

namespace CodeIsBug.Admin.Services.Base
{
    public class BaseService<T> : SimpleClient<T> where T : class, new()
    {
        public BaseService(ISqlSugarClient context = null) : base(context) //注意这里要有默认值等于null
        {
            base.Context = DbScoped.SugarScope;

            //调式代码 用来打印SQL 
            Context.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                                  Context.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName,
                                      it => it.Value)));
                // LogHelper.LogWrite(sql + "\r\n" +Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
            };
            base.Context.CodeFirst.SetStringDefaultLength(200).InitTables<ESysErrorLog>();
        }
    }
}