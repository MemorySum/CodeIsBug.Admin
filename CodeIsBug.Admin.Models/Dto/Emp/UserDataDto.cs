using System;
using System.Collections.Generic;
using CodeIsBug.Admin.Models.Models;

namespace CodeIsBug.Admin.Models.Dto
{
    public class UserDataDto
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public List<ESysMenu> UserMenus { get; set; }

        public string UserRoleName { get; set; }
    }
}