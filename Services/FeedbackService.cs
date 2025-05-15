using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;

namespace Backend.Services
{
    public class FeedbackService : IFeedbackService
    {

        public async Task SendFeedbackEmailAsync(string name, string email, int rating, string feedback)
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
                Body = $" Name:{name}\nEmail: {email}\nFeedback: {feedback}\nRating:{rating}",
                IsBodyHtml = false,
            };

            mail.To.Add("b221037@iiit-bh.ac.in");

            await smtpClient.SendMailAsync(mail);



        }
    }
}