namespace Shared.RequestFeatures
{
    public record UserClaimsDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public IList<string>? Roles { get; set; }
    }
}
