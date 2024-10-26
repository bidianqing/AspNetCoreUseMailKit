using MimeKit;

namespace AspNetCoreUseMailKit
{
    public interface IEmailSender
    {
        Task SendAsync(string to, string subject, string body);

        Task SendAsync(List<string> to, string subject, string body);

        Task SendAsync(MimeMessage message);
    }
}
