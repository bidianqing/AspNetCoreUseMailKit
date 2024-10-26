using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace AspNetCoreUseMailKit
{
    public class EmailSender : IEmailSender
    {
        private readonly MailOptions _mailOptions;
        public EmailSender(IOptionsMonitor<MailOptions> mailOptions)
        {
            _mailOptions = mailOptions.CurrentValue;
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            await this.SendAsync([to], subject, body);
        }

        public async Task SendAsync(List<string> to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_mailOptions.DisplayName, _mailOptions.UserName));
            message.To.AddRange(to.Select(address => new MailboxAddress("", address)));
            message.Subject = subject;
            message.Body = new TextPart(TextFormat.Plain) { Text = body };

            await this.SendAsync(message);
        }

        public async Task SendAsync(MimeMessage message)
        {
            using var client = new SmtpClient();

            await client.ConnectAsync(_mailOptions.Host, _mailOptions.Port, _mailOptions.UseSsl);

            // Note: only needed if the SMTP server requires authentication
            await client.AuthenticateAsync(_mailOptions.UserName, _mailOptions.Password);

            var result = await client.SendAsync(FormatOptions.Default, message);
            await client.DisconnectAsync(true);
        }

    }
}
