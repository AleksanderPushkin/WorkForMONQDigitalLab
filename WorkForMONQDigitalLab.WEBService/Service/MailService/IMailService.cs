using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkForMONQDigitalLab.WEBService.Models;

namespace WorkForMONQDigitalLab.WEBService.Service.MailService
{
     public interface IMailService
    {
        Task<Tuple<ActionResult,string>>SendMailAsync(IEnumerable<string> emails, string subject, string message);
    }
}
