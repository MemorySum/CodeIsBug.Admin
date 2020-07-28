using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models.DbContext;
using CodeIsBug.Admin.Models.DTO;
using CodeIsBug.Admin.Models.Models;

namespace CodeIsBug.Admin.Services.Service
{
    public class AccountService : DataContext<EBaseEmp>
    {
        public EBaseEmp Login(LoginInputDto dto)
        {
            Db.CodeFirst.SetStringDefaultLength(100).InitTables(typeof(EBaseEmp), typeof(ESysMenu));
            return Db.Queryable<EBaseEmp>().First(a => a.UserName.Equals(dto.username)
                                                       && a.Pwd.ToLower().Equals(StringHelper.Md5Hash(dto.password)));
        }
    }
}
