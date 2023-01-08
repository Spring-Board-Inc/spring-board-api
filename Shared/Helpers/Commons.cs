using Entities.ErrorModel;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;
using System.Text.RegularExpressions;

namespace Shared.Helpers
{
    public class Commons
    {
        const int PHOTO_MAX_ALLOWABLE_SIZE = 1000000;
        const int DOC_MAX_ALLOWABLE_SIZE = 2000000;

        public static string Capitalize(string text)
        {
            return Regex.Replace(Normalize(text), "^[a-z]", m => m.Value.ToUpper());
        }

        public static UserForRegistrationDto CapitalizeUserDetails(UserForRegistrationDto user)
        {
            user.FirstName = Capitalize(user.FirstName);
            user.LastName = Capitalize(user.LastName);
            user.Gender = Capitalize(user.Gender);
            user.City = Capitalize(user.City);

            return user;
        }

        private static string Normalize(string text)
        {
            return text.ToLower().Trim();
        }

        public static ResponseDetails ValidateImageFile(IFormFile photo)
        {
            var fileFormats = new string[] { ".png", ".jpg", ".jpeg" };
            var isCorrectFormat = false;
            foreach (var f in fileFormats)
            {
                if (photo.FileName.EndsWith(f))
                {
                    isCorrectFormat = true;
                    break;
                }
            }
            if (!isCorrectFormat)
                return new ResponseDetails { Successful = false, Message = ResponseMessages.InvalidImageFormat };

            if (photo.Length > PHOTO_MAX_ALLOWABLE_SIZE)
                return new ResponseDetails { Successful = false, Message = ResponseMessages.FileTooLarge };

            return new ResponseDetails { Successful = true };
        }

        public static ResponseDetails ValidateDocumentFile(IFormFile doc)
        {
            var docFormats = new string[] { ".pdf", ".doc", ".docx" };
            var isCorrectFormat = false;
            foreach (var f in docFormats)
            {
                if (doc.FileName.EndsWith(f))
                {
                    isCorrectFormat = true;
                    break;
                }
            }
            if (!isCorrectFormat)
                return new ResponseDetails { Successful = false, Message = ResponseMessages.InvalidDocumentFormat };

            if (doc.Length > PHOTO_MAX_ALLOWABLE_SIZE)
                return new ResponseDetails { Successful = false, Message = ResponseMessages.FileTooLarge };

            return new ResponseDetails { Successful = true };
        }
    }
}
