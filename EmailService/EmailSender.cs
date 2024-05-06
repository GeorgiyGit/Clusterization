using MimeKit;
using Microsoft.Extensions.Configuration;
using Domain.Interfaces.Other;

namespace EmailService
{
    public class EmailSender: IMyEmailSender
    {
        private readonly IConfigurationSection _emailSenderOptions;
        public EmailSender(IConfiguration configuration)
        {
            _emailSenderOptions = configuration.GetSection("EmailSenderOptions");
        }

        public async Task SendEmail(string subject, string body, string toUsername, string toEmail)
        {
            using (MimeMessage mm = new MimeMessage())
            {
                mm.From.Add(new MailboxAddress(_emailSenderOptions["Username"], _emailSenderOptions["Email"]));
                mm.To.Add(new MailboxAddress(toUsername, toEmail));
                mm.Subject = subject;
                BodyBuilder builder = new BodyBuilder();
                builder.HtmlBody = body;
                mm.Body = builder.ToMessageBody();
                using (MailKit.Net.Smtp.SmtpClient smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    await smtp.ConnectAsync(_emailSenderOptions["Host"], int.Parse(_emailSenderOptions["Port"]));
                    await smtp.AuthenticateAsync(_emailSenderOptions["Username"], _emailSenderOptions["Password"]);
                    await smtp.SendAsync(mm);
                    await smtp.DisconnectAsync(true);
                }
            }
        }
    }
}
