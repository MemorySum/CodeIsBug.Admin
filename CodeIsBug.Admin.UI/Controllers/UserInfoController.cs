using System;
using System.Collections.Generic;
using System.Linq;
using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models.DbContext;
using CodeIsBug.Admin.UI.DTO;
using CodeIsBug.Admin.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CodeIsBug.Admin.UI.Controllers
{

    public class UserInfoController : Controller
    {
        private readonly CodeIsBugContext _codeIsBugContext;

        public UserInfoController(CodeIsBugContext context)
        {
            this._codeIsBugContext = context;
        }

        public IActionResult UserList()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetUserList([FromBody] SearchDto dto, int current, int pageSize)
        {
            var q = _codeIsBugContext.EBaseEmps.AsQueryable();
            if (!string.IsNullOrWhiteSpace(dto.UserName))
            {
                q = q.Where(a => a.UserName.Contains(dto.UserName));
            }
            if (!string.IsNullOrWhiteSpace(dto.Name))
            {
                q = q.Where(a => a.Name.Contains(dto.Name));
            }
            if (!string.IsNullOrWhiteSpace(dto.IsDelete))
            {
                q = q.Where(a => a.IsDelete == int.Parse(dto.IsDelete));
            }
            var list = q.Take(current).Skip(pageSize).Select(a => new UserListViewModel()
            {
                UserGuid = a.UserGuid,
                AddTime = a.AddTime,
                Email = a.Email,
                IsDelete = a.IsDelete,
                ModifyTime = a.ModifyTime,
                Name = a.Name,
                Phone = a.Phone,
                UserName = a.UserName
            }).ToList();
           
            return Json(new { code = 0, msg = string.Empty, count = list.Count, data = list });


        }
    }
}