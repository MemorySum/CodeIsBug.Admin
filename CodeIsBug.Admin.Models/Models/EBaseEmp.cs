using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeIsBug.Admin.Models
{
    [Table("e_Base_Emp")]
    public partial class EBaseEmp
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public int IsDelete { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime? ModifyTime { get; set; }
        public Guid UserGuid { get; set; }
    }
}
