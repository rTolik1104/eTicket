using MimeKit;
using MailKit.Net.Smtp; 
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using eTicket_Demo.Models;

namespace eTicket_Demo.Data.Services
{
    public class EmailServices
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("eTicket.uz", "eticket917@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("",email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync("eticket917@gmail.com", "rt11042002");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
