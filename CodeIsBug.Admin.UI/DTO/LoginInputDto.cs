using System;

namespace CodeIsBug.Admin.UI.DTO
{
    public class LoginInputDto
    {
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string Captcha { get; set; }
    }
}