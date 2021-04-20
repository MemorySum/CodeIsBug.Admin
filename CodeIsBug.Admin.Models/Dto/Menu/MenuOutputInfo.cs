using System;
using System.Collections.Generic;
namespace CodeIsBug.Admin.Models.Dto
{
    public class MenuOutputInfo
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public int IsDeleted { get; set; }
        public string Icon { get; set; }
        public int Sort { get; set; }
        public int Level { get; set; }
        public string Url { get; set; }
        public int? ParentId { get; set; }
        public DateTime? ModifyTime { get; set; }
        public DateTime AddTime { get; set; }
        public List<MenuOutputInfo> Children { get; set; }
    }
}
