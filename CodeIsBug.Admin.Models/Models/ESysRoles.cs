using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeIsBug.Admin.Models
{
    public partial class ESysRoles
    {
        [Key]
        [Display(Name ="角色主键")]
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime? ModifyTime { get; set; }
    }
}
