﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models.DbContext;
using CodeIsBug.Admin.Models.Dto;
using CodeIsBug.Admin.Models.Models;
using SqlSugar;

namespace CodeIsBug.Admin.Services.Service
{
    /// <summary>
    /// 用户管理Service
    /// </summary>
    public class SysUsersService : DataContext<EBaseEmp>
    {
        public List<EmpOutputInfo> GetUserList(string query, int pageIndex, int pageSize, ref int totalCount)
        {
            var list = Db.Queryable<EBaseEmp>().Select(emp => new EmpOutputInfo
            {
                UserId = emp.UserId,
                UserName = emp.UserName,
                Name = emp.Name,
                Phone = emp.Phone,
                Email = emp.Email,
                AddTime = emp.AddTime,
                ModifyTime = emp.ModifyTime
            }).WhereIF(!string.IsNullOrEmpty(query), emp => (emp.Name.Contains(query) || emp.UserName.Contains(query)))
                .ToPageList(pageIndex, pageSize, ref totalCount);
            return list;
        }

        public async Task<bool> AddUser(UserAddInfo inputInfo)
        {
            EBaseEmp emp = new EBaseEmp
            {
                AddTime = DateTime.Now,
                Email = inputInfo.Email,
                UserName = inputInfo.UserName,
                Name = inputInfo.Name,
                Phone = inputInfo.Phone,
                Pwd = StringHelper.Md5Hash("1")
            };
            return await Db.Insertable<EBaseEmp>(emp).ExecuteCommandIdentityIntoEntityAsync();
        }

        public async Task<bool> DelUser(int userId)
        {
            return await Db.Deleteable<EBaseEmp>().Where(a => a.UserId.Equals(userId)).ExecuteCommandHasChangeAsync();
        }

        public async Task<UserEditInfo> GetUserInfo(int userId)
        {
            var list = await Db.Queryable<EBaseEmp>().Where(a => a.UserId.Equals(userId))
                 .Select<UserEditInfo>(a => new UserEditInfo()
                 {
                     UserId = a.UserId,
                     Name = a.Name,
                     UserName = a.UserName,
                     Email = a.Email,
                     Phone = a.Phone
                 }).ToListAsync();
            return list.FirstOrDefault();
        }

        public async Task<bool> UpdateUserInfo(UserEditInfo info)
        {
            EBaseEmp emp = new EBaseEmp()
            {
                UserId = info.UserId,
                Name = info.Name,
                UserName = info.UserName,
                Email = info.Email,
                Phone = info.Phone,
                ModifyTime = DateTime.Now
            };
            return await Db.Updateable<EBaseEmp>(emp)
                .UpdateColumns(it => new
                {
                    it.Name,
                    it.Email,
                    it.ModifyTime,
                    it.UserName,
                    it.Phone
                }).With(SqlWith.UpdLock).ExecuteCommandHasChangeAsync();
        }
    }
}