using MailKit.Net.Smtp;
using MimeKit;

namespace VPPShop.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendVerificationEmail(string toEmail, string token)
        {
            string fromEmail = _configuration["Email:Address"];
            string fromPassword = _configuration["Email:Password"];

            string subject = "Xác nhận đăng ký tài khoản";
            string body = $"Vui lòng xác nhận email của bạn bằng cách nhấn vào đường dẫn sau:\n" +
                          $"https://yourdomain.com/Account/VerifyEmail?token={token}";

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("VPPShop", fromEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate(fromEmail, fromPassword);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
