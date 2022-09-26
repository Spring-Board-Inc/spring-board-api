using Entities.Models;

namespace Shared.Helpers
{
    public static class ResponseMessages
    {
        public static readonly string InvalidDateRange = "Date range is invalid.";
        public static readonly string NoLocationFound = "No location found.";
        public static readonly string RegistrationFailed = "Registration failed.";
        public static readonly string UserNotFound = "User not found.";
        public static readonly string InvalidToken = "The token is invalid.";
        public static readonly string Unauthorized = "You are not authorized to perform this operation.";
        public static readonly string EmailTaken = "This email is taken.";
        public static readonly string UpdateSuccessful = "Update successful.";
        public static readonly string DeleteSuccessful = "Record successfully deleted.";
        public static readonly string PasswordResetSuccessful = "Password successfully reset.";
        public static readonly string PasswordChangeSuccessful = "Password successfully changed.";
        public static readonly string EmailNotConfirmed = "Password reset failed. Email not confirmed.";
        public static readonly string PasswordResetFailed = "Password reset failed.";
        public static readonly string PasswordChangeFailed = "Password change failed.";
        public static readonly string UserUpdateFailed = "User profile update failed.";
        public static readonly string UserDeactivationSuccessful = "User profile successfully deactivated.";
        public static readonly string UserActivationSuccessful = "User profile successfully activated.";
        public static readonly string PhotoUploadFailed = "Photo upload failed.";
        public static readonly string PhotoNotFound = "Photo not found.";
        public static readonly string PhotoDeletionSuccessful = "Photo deleted successfully.";
        public static readonly string InvalidImageFormat = "Invalid file format.";
        public static readonly string FileTooLarge = "The file size is too large.";
        public static readonly string PhotoUpdateSuccessful = "Photo updated successfully.";
        public static readonly string PhotoDeletionFailed = "Photo deleted successfully.";
        public static readonly string PhotoUploadSuccessful = "Photo uploaded successfully.";
        public static readonly string InactiveAccount = "Account not active.";
        public static readonly string WrongPasswordOrUserName = "Wrong username or password.";
        public static readonly string LoginSuccessful = "Login successful.";
        public static readonly string UserInformationNotFound = "User information not found.";
        public static readonly string UserInfoUpdated = "User information updated successfully.";
        public static readonly string UserInfoDeleted = "User information deleted successfully.";
        public static readonly string EducationNotFound = "Education profile not found.";
        public static readonly string EducationDeleted = "Education profile successfully deleted";
        public static readonly string EducationUpdated = "Education profile successfully updated.";
        public static readonly string InvalidRequest = "Invalid request.";
        public static readonly string WorkExperienceNotFound = "Work experience not found";
        public static readonly string WorkExperienceUpdated = "Work experience details updated successfully.";
        public static readonly string WorkExperienceDeleted = "Work experience record deleted successfully.";
        public static readonly string IncorrectSkillLevel = "Invalid value for skill level.";
        public static readonly string SkillNotFound = "Skill not found.";
        public static readonly string SkillUpdated = "Skill details sucessfully updated.";
        public static readonly string SkillDeleted = "Skill details deleted successfully.";
        public static readonly string UserSkillNotFound = "User skill not found.";
        public static readonly string NoContent;
        public static readonly string CertificationNotFound = "Certification not found.";
        public static readonly string CertificationUpdated = "Certification successfully updated.";
        public static readonly string CertificationDeleted = "Certification successfully deleted.";
        public static readonly string JobTypeNotFound = "Job type not found.";
        public static readonly string JobTypeDeleted = "Job type successfully deleted.";
        public static readonly string IndustryNotFound = "Industry not found.";
        public static readonly string IndustryDeleted = "Industry successfully deleted.";
        public static readonly string CompanyNotFound = "Company not found.";
        public static readonly string CompanyDeleted = "Company successfully deleted.";
        public static readonly string CompanyUpdateFailed = "Company details update failed.";
        public static readonly string NoFileChosen;
        public static readonly string InvalidSalaryRange;
        public static readonly string InvalidClosingDate;
    }
}
