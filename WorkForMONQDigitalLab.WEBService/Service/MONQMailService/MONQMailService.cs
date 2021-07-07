using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkForMONQDigitalLab.WEBService.Context;
using WorkForMONQDigitalLab.WEBService.Models;
using WorkForMONQDigitalLab.WEBService.Service.MailService;

namespace WorkForMONQDigitalLab.WEBService.Service.MONQMailService
{
    public class MONQMailService : IMONQMailService
    {
        private readonly IMailService _sendMailService;
        private readonly MailsDbContext _db;
        private readonly ILogger<MONQMailService> _logger;
        public MONQMailService(IMailService sendMailService, MailsDbContext db, ILogger<MONQMailService> logger)
        {
            _sendMailService = sendMailService;
            _db = db;
            _logger = logger;
        }
        /// <summary>
        /// Give a list of all sent messages
        /// </summary>
        /// <returns>List of send emails protocols</returns>
        public IEnumerable<MailDTO> Get()
        {
            return _db.Mails.AsNoTracking().ToList().Select(t => new MailDTO { Body = t.Body, CreatedAt = t.CreatedAt, Recipients = t.Recipients.Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries), Result = t.Result, FailedMessage= t.FailedMessage, Subject = t.Subject });
        }

        /// <summary>
        /// Send Email and save to repository
        /// </summary>
        /// <param name="sendMailDTO"></param>
        /// <exception cref="ArgumentNullException">when SendMailDTO is null</exception>
        /// <returns>result of message sending</returns>
        public async Task<Tuple<ActionResult, string>> SendMailAsync(SendMailDTO sendMailDTO)
        {
            try
            {
                if (sendMailDTO == null) throw new ArgumentNullException(nameof(sendMailDTO));
                var results = await _sendMailService.SendMailAsync(sendMailDTO.Recipients, sendMailDTO.Subject, sendMailDTO.Body);
                _logger.LogInformation("Results: {0}, Message: {1}", results.Item1, results.Item2);
                _db.Mails.Add(new Context.Entity.Mail { CreatedAt = DateTime.Now, Recipients = string.Join(';', sendMailDTO.Recipients), Subject = sendMailDTO.Subject, Body = sendMailDTO.Body, FailedMessage = results.Item2, Result = results.Item1.ToString("g") });
                _db.SaveChanges();

                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Tuple.Create(ActionResult.Failed, ex.Message);
            }
        }
    }
}
