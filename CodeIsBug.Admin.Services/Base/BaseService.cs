using System;
using System.Linq;
using CodeIsBug.Admin.Common.Config;
using SqlSugar;

namespace CodeIsBug.Admin.Services.Base
{
    public class BaseService<T> :  SimpleClient<T> where T : class, new()
    {
        public BaseService(ISqlSugarClient context = null) : base(context)//注意这里要有默认值等于null
        {
            if (context == null)
            {
                Db = new SqlSugarClient(new ConnectionConfig()
                {
                    DbType = DbType.MySql,
                    InitKeyType = InitKeyType.Attribute,
                    IsAutoCloseConnection = true,
                    ConnectionString = DBConfig.ConnectionString
                });
               
            }
            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" + Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                // LogHelper.LogWrite(sql + "\r\n" +Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
            };

        }
        public SqlSugarClient Db;
    }
}
