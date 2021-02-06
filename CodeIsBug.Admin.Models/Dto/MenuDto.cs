﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CodeIsBug.Admin.Models.Dto
{
	public class MenuDto
	{
		public int id { get; set; }

		public string menuName { get; set; }

		public string path { get; set; }

		public string icon { get; set; }

		public List<MenuDto> Children { get; set; }
	}
}
