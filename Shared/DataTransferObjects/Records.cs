namespace Shared.DataTransferObjects
{
    public record PhotoUploadResultDto(string PublicId, string Url);
    public record TokenDto(string AccessToken, string RefreshToken);
    public record PhotoToReturnDto(Guid Id, string UserId, string PublicId, string Picture, DateTime CreatedAt, DateTime UpdatedAt);
    public record UserInformationToReturnDto(Guid Id, string UserId, string Street, string Town, string State, string Country, string PostalCode);
    public record WorkExperienceToReturnDto(Guid Id, string Company, string Location, string Descriptions, string Designation, DateTime StartDate, DateTime EndDate, DateTime CreatedAt, DateTime UpdatedAt);
    public record SkillToReturnDto(Guid Id, string Description, DateTime CreatedAt, DateTime UpdatedAt);
    public record UserSkillDto(Guid UserInformationId, Guid SkillId, string Skill, string Level);
    public record CertificationDto(Guid Id, string Name, string IssuingBody, DateTime IssuingDate, DateTime CreatedAt, DateTime UpdatedAt);
    public record CompanyToReturnDto(Guid Id, string Name, string Email, string LogoUrl, string PublicId, DateTime CreatedAt, DateTime UpdatedAt);
}