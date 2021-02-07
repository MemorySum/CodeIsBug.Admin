using System;
using System.Collections.Generic;
using System.Text;
using CodeIsBug.Admin.Common.Config;
using CodeIsBug.Admin.Models.Models;
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
                    DbType = SqlSugar.DbType.SqlServer,
                    InitKeyType = InitKeyType.Attribute,
                    IsAutoCloseConnection = true,
                    ConnectionString = DBConfig.ConnectionString
                });
                Db.DbMaintenance.CreateDatabase("CodeIsBug.Admin");
                Db.CodeFirst.SetStringDefaultLength(100).InitTables(typeof(EBaseEmp), typeof(ESysEmpRoleMap), typeof(ESysMenu), typeof(ESysRoles));
            }

            
        }
        public SqlSugarClient Db;
    }
}
