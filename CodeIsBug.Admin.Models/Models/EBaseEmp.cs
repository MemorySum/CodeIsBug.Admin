using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeIsBug.Admin.Models.Models
{
    [Table("e_Base_Emp")]
    public partial class EBaseEmp: BaseModel
    {
        [Key]
        
        public Guid UserGuid { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public int IsDelete { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime? ModifyTime { get; set; }
    }
}
