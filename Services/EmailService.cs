using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }


        public async Task SendContactEmailAsync(string name, string email, string message)
        {
            using (var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("piyushsingh6521@gmail.com", "othy mypw hxza bpsd"),
                EnableSsl = true,
            })
            {
                var mail = new MailMessage
                {
                    From = new MailAddress("piyushsingh6521@gmail.com"),
                    Subject = $"New Contact from {name}",
                    Body = $"Email: {email}\nMessage: {message}",
                    IsBodyHtml = false,
                };

                mail.To.Add("piyushsingh6521@gmail.com");

                await smtpClient.SendMailAsync(mail);
            }
        }



    }
}