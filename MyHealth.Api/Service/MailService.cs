using MyHealth.Data;
using MyHealth.Data.Dto;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using MyHealth.Data.Entities;
using MyHealth.Api.Utils;

namespace MyHealth.Api.Service
{
    public class MailService
    {
        public MailService()
        {
        }

        public async Task SendAsync(string pSubject, string pMessage, params string[] pRecipients)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("MyHealth", Env.GetString("SMTP_SENDER_EMAIL")));
            email.To.AddRange(pRecipients.Select(s => new MailboxAddress("MyHealthClient", s)));

            email.Subject = pSubject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = pMessage
            };

            await SendAsync(email);
        }

        private async Task SendAsync(MimeMessage pMessage)
        {
            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(Env.GetString("SMTP_HOST"), int.Parse(Env.GetString("SMTP_PORT")), true);

                await smtp.AuthenticateAsync(Env.GetString("SMTP_USERNAME"), Env.GetString("SMTP_PASSWORD"));

                await smtp.SendAsync(pMessage);

                await smtp.DisconnectAsync(true);
            }
        }
    }
}
