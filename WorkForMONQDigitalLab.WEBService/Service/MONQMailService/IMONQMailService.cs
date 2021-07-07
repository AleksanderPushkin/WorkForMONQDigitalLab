using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkForMONQDigitalLab.WEBService.Models;

namespace WorkForMONQDigitalLab.WEBService.Service.MONQMailService
{
    public interface IMONQMailService
    {
        IEnumerable<MailDTO> Get();
        Task<Tuple<ActionResult, string>> SendMailAsync(SendMailDTO sendMailDTO);
    }
}
