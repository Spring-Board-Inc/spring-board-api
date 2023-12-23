using Shared.RequestFeatures;
using System.Security.Claims;

namespace Shared.DataTransferObjects
{
    public record PhotoUploadResultDto(string PublicId, string Url);
    public record TokenDto(string AccessToken, string RefreshToken, ClaimsDto? UserClaims);
    public record ClaimsDto(Guid UserId, Guid? UserInfomationId, string Email, IList<string>? Roles);
    public record PhotoToReturnDto(Guid Id, string UserId, string PublicId, string Picture, DateTime CreatedAt, DateTime UpdatedAt);
    public record UserInformationToReturnDto(Guid Id, string UserId);
    public record WorkExperienceToReturnDto(Guid Id, string Company, string Location, string Descriptions, string Designation, DateTime StartDate, DateTime EndDate, DateTime CreatedAt, DateTime UpdatedAt);
    public record SkillToReturnDto(Guid Id, string Description, DateTime CreatedAt, DateTime UpdatedAt);
    public record UserSkillDto(Guid UserInformationId, Guid SkillId, string Skill, string Level);
    public record CertificationDto(Guid Id, string Name, string IssuingBody, DateTime IssuingDate, DateTime CreatedAt, DateTime UpdatedAt);
    public record CompanyToReturnDto(Guid Id, string Name, string Email, string LogoUrl, string PublicId, DateTime CreatedAt, DateTime UpdatedAt);
    public record JobStatsDto(int JobSeekers, int ActiveJobs, int JobsFilled, int Companies);
}