using System;
using System.ComponentModel.DataAnnotations;

namespace CodeIsBug.Admin.Models.Models
{
    public  class ESysMenu: BaseModel
    {
        [Key]
        public Guid MenuId { get; set; }
        public Guid? ParentId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int Sort { get; set; }
        public int Level { get; set; }
        public int IsDeleted { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime? ModifyTime { get; set; }
    }
}
