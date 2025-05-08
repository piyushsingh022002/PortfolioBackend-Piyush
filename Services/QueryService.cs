using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;

namespace Backend.Services
{
    public class QueryService : IQueryService
    {

        public async Task SendQueryEmailAsync(string name, string email, string designation, string PhoneNumber, string query)
        {
            using var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("piyushsingh6521@gmail.com", "othy mypw hxza bpsd"),
                EnableSsl = true,
            };

            var mail = new MailMessage
            {
                From = new MailAddress("piyushsingh6521@gmail.com"),
                Subject = $"New Feedback from {name}",
                Body = $"Email: {email}\nQuery: {query}\nPhoneNumber:{PhoneNumber}\nDesignation:{designation}",
                IsBodyHtml = false,
            };

            mail.To.Add("b221037@iiit-bh.ac.in");

            await smtpClient.SendMailAsync(mail);



        }
    }
}