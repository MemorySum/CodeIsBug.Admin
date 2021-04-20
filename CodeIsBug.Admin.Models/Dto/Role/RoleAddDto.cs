using System;
namespace CodeIsBug.Admin.Models.Dto
{
    public class RoleAddDto
    {
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
    }
}
