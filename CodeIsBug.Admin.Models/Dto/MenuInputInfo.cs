using System;

namespace CodeIsBug.Admin.Models.DTO
{
	public class MenuInputInfo
	{
		public int MenuId { get; set; }
		public string Name { get; set; }
		public int IsDeleted { get; set; }
		public string Icon { get; set; }
		public int Sort { get; set; }
		public int Level { get; set; }
		public string Url { get; set; }
		public int? ParentId { get; set; }
	}
}