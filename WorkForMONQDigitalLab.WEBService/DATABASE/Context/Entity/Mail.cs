using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkForMONQDigitalLab.WEBService.Context.Entity
{
    public class Mail
    {
        
            [Key]
            public int Id { get; set; }
            public DateTime CreatedAt { get; set; }
            public string Result { get; set; }
            public string FailedMessage { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public string Recipients { get; set; }
        
    }
}
