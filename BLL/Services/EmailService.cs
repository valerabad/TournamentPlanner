using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmailService
    {
        private string Address { get; set; }
        private string Smtp { get; set; }
        private string Password { get; set; }
        private string SenderName { get; set; }
        private int Port { get; set; }
        public EmailService(string address, string senderName, string smtp, int port, string password)
        {
            Address = address;
            SenderName = senderName;
            Smtp = smtp;
            Password = password;
            Port = port;

        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(SenderName, Address));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(Smtp, Port, true);
                await client.AuthenticateAsync(Address, Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
