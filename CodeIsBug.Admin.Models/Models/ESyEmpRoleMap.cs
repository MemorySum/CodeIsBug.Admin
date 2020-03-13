using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeIsBug.Admin.Models
{
    public partial class ESyEmpRoleMap
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? EmpId { get; set; }
        public Guid? RoleId { get; set; }
    }
}
