using System;
using System.Collections.Generic;
namespace CodeIsBug.Admin.Models.Dto
{
    public class RoleMenuMapSaveDto
    {
        public Guid RoleId { get; set; }
        public List<Guid> SelectMenuIds { get; set; }
    }
}
