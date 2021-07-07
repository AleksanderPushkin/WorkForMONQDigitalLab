using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkForMONQDigitalLab.WEBService.Models;
using WorkForMONQDigitalLab.WEBService.Models.Settings;

namespace WorkForMONQDigitalLab.WEBService.Service.MailService
{
    public class MailService : IMailService
    {
        private readonly EmailOptions _options;
        private readonly ILogger _logger;

        public MailService(IOptions<EmailOptions> options,ILogger<MailService> logger)
        {
            _options = options.Value;
            _logger = logger;
        }
        /// <summary>
        /// This method is responsible for sending messages
        /// </summary>
        /// <param name="emails">incoming array of  email addresses</param>
        /// <param name="subject">Message subject</param>
        /// <param name="message">Message body</param>
        /// <returns>
        ///     Return send results
        /// </returns>
        public async Task<Tuple<ActionResult, string>> SendMailAsync(IEnumerable<string> emails, string subject, string message)
        {
            try
            {
                MimeMessage mimeMessage = GenerateMimeMessage(emails, subject, message);

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(_options.MailServer, _options.MailPort, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_options.Sender, _options.Password);
                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);
                }
                return Tuple.Create<ActionResult, string>(ActionResult.OK, string.Empty);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Tuple.Create<ActionResult, string>(ActionResult.Failed, ex.Message);
            }
        }
        /// <summary>
        /// Formation email
        /// </summary>
        /// <param name="emails">Recipient list </param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private MimeMessage GenerateMimeMessage(IEnumerable<string> emails, string subject, string message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_options.SenderName, _options.Sender));
            foreach (var email in emails)
            {
                mimeMessage.To.Add(MailboxAddress.Parse(email));
            }
            mimeMessage.Subject = subject;
            var builder = new BodyBuilder();
            builder.TextBody = message;
            mimeMessage.Body = builder.ToMessageBody();
            return mimeMessage;
        }
    }
}
