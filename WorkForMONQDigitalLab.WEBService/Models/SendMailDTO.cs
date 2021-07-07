using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkForMONQDigitalLab.WEBService.Models
{
    public class SendMailDTO
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public IEnumerable<string> Recipients { get; set; }
    }
}
