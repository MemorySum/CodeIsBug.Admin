#nullable enable
using System;
using System.Collections.Generic;

namespace CodeIsBug.Admin.Api.DTO
{
	public class MenuOutputInfo
	{
		public Guid MenuId { get; set; }
		public string Name { get; set; }
		public int IsDeleted { get; set; }
		public string Icon { get; set; }
		public int Sort { get; set; }
		public int Level { get; set; }
		public string Url { get; set; }
		public Guid ParentId { get; set; }
		public DateTime? ModifyTime { get; internal set; }
		public DateTime AddTime { get; internal set; }
		public List<MenuOutputInfo>? children { get; set; }
	}
}