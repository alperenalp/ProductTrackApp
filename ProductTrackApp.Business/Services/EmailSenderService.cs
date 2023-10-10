using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ProductTrackApp.Business.DTOs.Requests;
using System.Net.Http;

namespace ProductTrackApp.Business.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public async Task SendEmailAsync(string htmlContent, string from, string to)
        {
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Product Track App - Ürün Talebi";
            message.Body = htmlContent;
            message.IsBodyHtml = true;

            string smtpServer = "smtp-mail.outlook.com";
            int smtpPort = 587; 
            string smtpUsername = "blablaa@outlook.com"; 
            string smtpPassword = "*****";

            SmtpClient smtp = new SmtpClient(smtpServer);
            smtp.Port = smtpPort;
            smtp.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            smtp.EnableSsl = true; 

            await smtp.SendMailAsync(message);
        }
    }
}
