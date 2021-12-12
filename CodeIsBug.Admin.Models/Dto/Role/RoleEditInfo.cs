using System;

namespace CodeIsBug.Admin.Models.Dto.Role
{
    public class RoleEditInfo
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public Guid ParentId { get; set; }
        public string Sort { get; set; }
        public string Remark { get; set; }
    }
}