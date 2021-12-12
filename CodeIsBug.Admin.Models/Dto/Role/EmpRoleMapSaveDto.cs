using System;
using System.Collections.Generic;

namespace CodeIsBug.Admin.Models.Dto.Role
{
    public class EmpRoleMapSaveDto
    {
        public Guid UserId { get; set; }
        public List<Guid> SelectRoleIds { get; set; }
    }
}