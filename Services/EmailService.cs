using Mailjet.Client;
using Mailjet.Client.Resources;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Newtonsoft.Json.Linq;
using Services.Contracts;
using Shared.RequestFeatures;
using System.Net.Mail;

namespace Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly IMailjetClient _client;

        public EmailService(IConfiguration config, IMailjetClient client)
        {
            _config = config;
            _client = client;
        }
        
        public async Task SendMailAsync(EmailRequestParameters requestParameters)
        {
            var emails = new List<string>() { requestParameters.To };
            try
            {
                foreach (var mail in emails)
                {
                    string email = mail;
                    MailjetRequest request = new MailjetRequest { Resource = SendV31.Resource }
                    .Property(Send.Messages, new JArray {
                        new JObject
                        {
                            {
                                "From",new JObject
                                {
                                    {"Email","ojotobar@gmail.com"},
                                    {"Name", "Spring Board Inc."}
                                }
                            },
                            {
                                "To", new JArray
                                {
                                    new JObject
                                    {
                                        {"Email", mail },
                                    }
                                }
                            },
                            { "Subject", requestParameters.Subject },
                            { "TextPart", requestParameters.Message },
                            { "HtmlPart", requestParameters.Message },
                            { "CustomId", "SpringBoardApp" }
                        }
                    });
                    MailjetResponse response = await _client.PostAsync(request);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string? GetConfirmEmailTemplate(string emailLink, string name)
        {
            string body;
            var folderName = Path.Combine("wwwroot", "Templates", "ConfirmEmail.html");
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (File.Exists(filepath))
                body = File.ReadAllText(filepath);
            else
                return null;

            var msgBody = body.Replace("{email_link}", emailLink).
                Replace("{name}", name).
                Replace("{company}", "CarX Inc.").
                Replace("{year}", DateTime.Now.Year.ToString());

            return msgBody;
        }
    }
}