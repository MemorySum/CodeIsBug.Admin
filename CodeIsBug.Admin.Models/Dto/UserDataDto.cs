using System;
using System.Collections.Generic;
using System.Text;

namespace CodeIsBug.Admin.Models.Dto
{
	public class UserDataDto
	{
		public int UserId { get; set; }

		public string UserName { get; set; }

		public string UserRoleIds { get; set; }

		public string UserRoleName { get; set; }
	}
}
