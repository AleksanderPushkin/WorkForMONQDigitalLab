
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkForMONQDigitalLab.WEBService.Context;
using WorkForMONQDigitalLab.WEBService.Context.Entity;
using WorkForMONQDigitalLab.WEBService.Models;
using WorkForMONQDigitalLab.WEBService.Service.MailService;
using WorkForMONQDigitalLab.WEBService.Service.MONQMailService;

namespace WorkForMONQDigitalLab.WEBService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailsController : ControllerBase
    {
        private readonly IMONQMailService _monqMailService;
        public MailsController(IMONQMailService monqMailService)
        {
            _monqMailService = monqMailService;
        }
        /// <summary>
        /// Give a list of all sent messages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<MailDTO> Get()
        {
            return _monqMailService.Get();
        }
        /// <summary>
        /// Method is responsible for sending emails to receipients.
        /// </summary>
        /// <param name="sendMailDTO">Model with body, subject and receipients</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SendMailAsync(SendMailDTO sendMailDTO)
        {
            var result = await _monqMailService.SendMailAsync(sendMailDTO);
            if (result.Item1 == Models.ActionResult.OK) return base.Ok();
            return BadRequest(result.Item2);
        }

    }
}
