namespace Shared.Helpers
{
    public class GetEmailTemplates
    {
        public static string? GetConfirmEmailTemplate(string emailLink, string name)
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
                Replace("{year}", DateTime.Now.Year.ToString());

            return msgBody;
        }

        public static string? GetResetPasswordEmailTemplate(string emailLink)
        {
            string body;
            var folderName = Path.Combine("wwwroot", "Templates", "ResetPassword.html");
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (File.Exists(filepath))
                body = File.ReadAllText(filepath);
            else
                return null;

            var msgBody = body.Replace("{email_link}", emailLink).
                Replace("{year}", DateTime.Now.Year.ToString());

            return msgBody;
        }
    }
}
