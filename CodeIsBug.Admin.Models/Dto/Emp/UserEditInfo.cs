﻿using System;

namespace CodeIsBug.Admin.Models.Dto.Emp
{
    public class UserEditInfo
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}