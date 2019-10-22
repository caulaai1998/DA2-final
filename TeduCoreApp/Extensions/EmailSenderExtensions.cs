using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TeduCoreApp.Services;

namespace TeduCoreApp.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            string sHTML = File.ReadAllText(@"..\TeduCoreApp\wwwroot\templates\emailSendTemp.txt");
            sHTML = sHTML.Replace("id=\"veryImportant\" href=\"#\""
                , $"id=\"veryImportant\" href='{HtmlEncoder.Default.Encode(link)}' ");
            return emailSender.SendEmailAsync(email, "Welcome to Web Core APp!!",
                sHTML);
        }
    }
}
