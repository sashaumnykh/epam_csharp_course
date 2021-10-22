using System.Collections.Generic;
using module_10.BLL.DTO;
using module_10.BLL.Interfaces;
using AutoMapper;
using module_10.DAL.Entities;
using module_10.DAL.Interfaces;
using System.Linq;
using System.Net.Mail;
using System.Net;

namespace module_10.BLL.Services
{
    public class EmailSender : IEmailSender
    {

        public void SendEmail(string toEmail, string text)
        {
            MailAddress from = new MailAddress("alexandraumnykh@gmail.com", "Tom");
            MailAddress to = new MailAddress(toEmail);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Уведомление о пропуске лекций";
            m.Body = "<h2>" + text + "</h2>";
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("alexandraumnykh@gmail.com", "mypassword");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
