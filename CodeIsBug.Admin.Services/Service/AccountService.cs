﻿using System.Threading.Tasks;
using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models.Dto;
using CodeIsBug.Admin.Models.Models;
using CodeIsBug.Admin.Services.Base;

namespace CodeIsBug.Admin.Services.Service
{
    public class AccountService : BaseService<EBaseEmp>
    {
        public async Task<EBaseEmp> Login(LoginInputDto dto)
        {
            return await Context.Queryable<EBaseEmp>()
                .FirstAsync(a => a.UserName.Equals(dto.username) && a.Pwd.Equals(dto.password.Md5Hash()));
        }
    }
}