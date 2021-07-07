using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkForMONQDigitalLab.WEBService.Models.Settings
{

    public class EmailOptions
    {
        public const string EmailSettings = "EmailSettings";

        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string SenderName { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
    }
}
