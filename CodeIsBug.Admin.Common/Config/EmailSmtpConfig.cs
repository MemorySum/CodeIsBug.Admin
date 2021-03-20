using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeIsBug.Admin.Common.Config
{
    public class EmailSmtpConfig
    {
        public  string SendServer { get; set; }

        public  int SendPort { get; set; }

        public  string SendEmail { get; set; }

        public  string SendNickname { get; set; }

        public  string SendPassword { get; set; }
    }
}
