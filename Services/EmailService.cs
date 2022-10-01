using Mailjet.Client;
using Mailjet.Client.Resources;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Newtonsoft.Json.Linq;
using Services.Contracts;
using Shared.DataTransferObjects;
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

        public async Task SendMailAsync(ApplicationRequestParameters requestParameters)
        {
            var base64String = string.Empty;
            if(requestParameters.File.Length > 0)
            {
                using(var stream = new MemoryStream())
                {
                    requestParameters.File.CopyTo(stream);
                    var fileBytes = stream.ToArray();
                    base64String = Convert.ToBase64String(fileBytes);
                }
            }

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
                            { "Attachments", new JArray
                                {
                                    new JObject
                                    {
                                        {"ContentType", "text/plain" },
                                        {"Filename", requestParameters.File.FileName },
                                        {"Base64Content", base64String }
                                    }
                                 }
                            },
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