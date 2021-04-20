using System;
using System.Collections.Generic;
namespace CodeIsBug.Admin.Models.Dto
{
    public class MenuDto
    {
        public Guid id { get; set; }

        public string menuName { get; set; }

        public string path { get; set; }

        public string icon { get; set; }

        public List<MenuDto> Children { get; set; }
    }
}
