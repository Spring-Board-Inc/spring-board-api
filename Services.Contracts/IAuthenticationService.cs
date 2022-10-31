using Entities.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Primitives;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<ApiBaseResponse> RegisterUser(UserForRegistrationDto userForRegistration, string role, StringValues origin);
        Task<ApiBaseResponse> ConfirmEmail(EmailConfirmationRequestParameters request);
        Task<TokenDto> CreateToken(bool populateExp);
        Task<ApiBaseResponse> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<ApiBaseResponse> RefreshToken(TokenDto tokenDto);
        Task<ApiBaseResponse> ChangeForgottenPassword(ChangeForgottenPasswordDto changePasswordDto);
        Task<ApiBaseResponse> ResetPassword(ResetPasswordDto resetPasswordDto, StringValues origin);
        Task<ApiBaseResponse> ChangePassword(ChangePasswordDto passwordDto);
    }
}
