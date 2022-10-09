using AutoMapper;
using Contracts;
using Entities.Enums;
using Entities.Models;
using Entities.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.Helpers;
using Shared.RequestFeatures;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _mailService;
        private readonly IRepositoryManager _repositoryManager;
        private readonly SignInManager<AppUser> _signInManager;
        private AppUser? _user;

        public AuthenticationService
            (
            ILoggerManager logger, 
            IMapper mapper,
            UserManager<AppUser> userManager, 
            IConfiguration configuration,
            IEmailService mailService,
            IRepositoryManager repositoryManager,
            SignInManager<AppUser> signInManager
            )
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _mailService = mailService;
            _repositoryManager = repositoryManager;
            _signInManager = signInManager;
        }
        public async Task<ApiBaseResponse> RegisterUser(UserForRegistrationDto userForRegistration, string role, StringValues origin)
        {
            if (!userForRegistration.IsPasswordMatched)
                return new BadRequestResponse(ResponseMessages.PasswordConfirmPasswordNotMatched);

            if (!userForRegistration.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var user = await _userManager.FindByEmailAsync(userForRegistration.Email);
            if (user != null)
                return new NotFoundResponse(ResponseMessages.UserNotFound);

            userForRegistration.Gender = Commons.Capitalize(userForRegistration.Gender);
            var isCorrectGender = Enum.IsDefined(typeof(EGender), userForRegistration.Gender);
            if (!isCorrectGender)
                return new BadRequestResponse(ResponseMessages.InvalidGender);

            user = _mapper.Map<AppUser>(userForRegistration);
            user.UserName = userForRegistration.Email;

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            
            if (!result.Succeeded) 
                return new BadRequestResponse(ResponseMessages.RegistrationFailed);

            await AssignUserToRole(user.Email, role);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var isSent = await SendEmailTokenToUser(
                new SendTokenEmailDto 
                {
                    User = user,
                    Token = token, 
                    Origin = origin, 
                    Subject = "Confirm Email", 
                    TokenType = EToken.ConfirmEmail 
                }); 
            
            if(!isSent.Success)
                return new BadRequestResponse(ResponseMessages.UnexpectedError);

            return new ApiOkResponse<IdentityResult>(result);
        }

        public async Task<ApiBaseResponse> ConfirmEmail(EmailConfirmationRequestParameters request)
        {
            if(!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return new UserNotFoundResponse(ResponseMessages.UserNotFound);

            if (!(await IsTokenConfirmed(Uri.UnescapeDataString(request.Token), EToken.ConfirmEmail.ToString())))
                return new BadRequestResponse(ResponseMessages.InvalidToken);

            var result = await _userManager.ConfirmEmailAsync(user, Uri.UnescapeDataString(request.Token));
            user.IsActive = true;
            await _userManager.UpdateAsync(user);

            return new ApiOkResponse<IdentityResult>(result);
            
        }

        public async Task<ApiBaseResponse> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            if (!userForAuth.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            _user = await _userManager.FindByNameAsync(userForAuth.Email);
            if (_user == null)
                return new UserNotFoundResponse(ResponseMessages.NoUserWithEmail);

            var signinResult = await _signInManager.PasswordSignInAsync(_user, userForAuth.Password, userForAuth.RememberMe, false);
            var result = (_user != null && _user.IsActive && signinResult.Succeeded);
            if (!result)
            {
                _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong password or user not activated yet.");
                if (_user == null)
                    return new NotFoundResponse(ResponseMessages.UserNotFound);
                else if (!_user.IsActive)
                    return new BadRequestResponse(ResponseMessages.InactiveAccount);
                else
                    return new BadRequestResponse(ResponseMessages.WrongPasswordOrUserName);
            }
            else
            {
                _user.LastLogin = DateTime.Now;
                await _userManager.UpdateAsync(_user);
            }

            return new ApiOkResponse<string>(ResponseMessages.LoginSuccessful);
        }
       
        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var refreshToken = GenerateRefreshToken();
            _user.RefreshToken = refreshToken;
            if (populateExp)
                _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(_user);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new TokenDto(accessToken, refreshToken);
        }

        public async Task<ApiBaseResponse> RefreshToken(TokenDto tokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            var user = await _userManager.FindByNameAsync(principal.Identity.Name);

            if (user == null || user.RefreshToken != tokenDto.RefreshToken)
                return new BadRequestResponse(ResponseMessages.InvalidToken);

            _user = user;
            var token = await CreateToken(populateExp: true);
            return new ApiOkResponse<TokenDto>(token);
        }

        public async Task<ApiBaseResponse> ResetPassword(ResetPasswordDto resetPasswordDto, StringValues origin)
        {
            if(!resetPasswordDto.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);

            if (user == null || !user.EmailConfirmed)
                return new BadRequestResponse(ResponseMessages.EmailNotConfirmed);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var isSent = await SendEmailTokenToUser(
                new SendTokenEmailDto 
                { 
                    User = user, 
                    Token = token, 
                    Origin = origin, 
                    Subject = "Reset Password", 
                    TokenType = EToken.ResetPassword 
                });

            if(!isSent.Success)
                return new BadRequestResponse(ResponseMessages.PasswordResetFailed);

            return new ApiOkResponse<string>(ResponseMessages.PasswordResetSuccessful);
        }

        public async Task<ApiBaseResponse> ChangeForgottenPassword(ChangeForgottenPasswordDto changePasswordDto)
        {
            if (!changePasswordDto.IsPasswordMatched)
                return new BadRequestResponse(ResponseMessages.PasswordConfirmPasswordNotMatched);

            if (!changePasswordDto.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var user = await _userManager.FindByIdAsync(changePasswordDto.UserId);
            if (user == null)
                return new NotFoundResponse(ResponseMessages.UserNotFound);

            if (!(await IsTokenConfirmed(Uri.UnescapeDataString(changePasswordDto.Token), EToken.ResetPassword.ToString())))
                return new BadRequestResponse(ResponseMessages.InvalidToken);

            var changePassword = await _userManager.ResetPasswordAsync(user, Uri.UnescapeDataString(changePasswordDto.Token), changePasswordDto.NewPassword);
            if (!changePassword.Succeeded) 
                return new BadRequestResponse(ResponseMessages.PasswordResetFailed);

            return new ApiOkResponse<string>(ResponseMessages.PasswordChangeSuccessful);
        }

        public async Task<ApiBaseResponse> ChangePassword(string userId, ChangePasswordDto passwordDto)
        {
            if (!passwordDto.IsPasswordMatched)
                return new BadRequestResponse(ResponseMessages.PasswordConfirmPasswordNotMatched);

            var user = await _userManager.FindByIdAsync(userId);
            var changedPassword = await _userManager.ChangePasswordAsync(user, passwordDto.CurrentPassword, passwordDto.NewPassword);
            
            if (!changedPassword.Succeeded)
                return new BadRequestResponse(ResponseMessages.PasswordChangeFailed);

            return new ApiOkResponse<string>(ResponseMessages.PasswordChangeSuccessful);
        }

        #region Private Methods

        private async Task<bool> IsTokenConfirmed(string token, string tokenType)
        {
            var tokenEntity = await _repositoryManager.Token.GetToken(token, true);
            if (tokenEntity == null)
                return false;

            if (DateTime.Now >= tokenEntity.ExpiresAt)
            {
                if(tokenEntity.Type == EToken.ConfirmEmail.ToString())
                {
                    var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == tokenEntity.UserId);
                    if (user == null)
                        return false;

                    await _userManager.DeleteAsync(user);
                }

                _repositoryManager.Token.DeleteToken(tokenEntity);
                await _repositoryManager.SaveAsync();
                return false;
            }

            _repositoryManager.Token.DeleteToken(tokenEntity);
            await _repositoryManager.SaveAsync();
            return true;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>{ new Claim(ClaimTypes.Name, _user.UserName), new Claim(ClaimTypes.NameIdentifier, _user.Id) };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["ValidIssuer"],
            claims: claims,
            expires: DateTime.Now.AddDays(Convert.ToDouble(jwtSettings["Expires"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"))),
                ValidateLifetime = true,
                ValidIssuer = jwtSettings["ValidIssuer"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
            StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }

        private async Task<ApiBaseResponse> AssignUserToRole(string email, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roleResult = await _userManager.AddToRoleAsync(user, role);
            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return new BadRequestResponse(ResponseMessages.RegistrationFailed);
            }

            return new ApiOkResponse<IdentityResult>(roleResult);
        }

        private async Task<ApiBaseResponse> SendEmailTokenToUser(SendTokenEmailDto tokenEmailDto)
        {
            var url = UrlBuilder.Builder(
                new UrlBuilderParameters 
                { 
                    Token = tokenEmailDto.Token, 
                    UserId = tokenEmailDto.User.Id 
                }, tokenEmailDto.Origin);

            var message = GetEmailTemplate(
                new GetEmailTemplateDto 
                { 
                    Url = url, 
                    FirstName = tokenEmailDto.User.FirstName, 
                    TemplateType = (int)tokenEmailDto.TokenType
                });

            if (message == null)
            {
                var userToDelete = await _userManager.FindByIdAsync(tokenEmailDto.User.Id);
                if (userToDelete != null)
                {
                    await _userManager.DeleteAsync(userToDelete);
                    return new BadRequestResponse(ResponseMessages.RegistrationFailed);
                }
            }

            var isSent = await _mailService.SendMailAsync(
                new EmailRequestParameters 
                { 
                    To = tokenEmailDto.User.Email, 
                    Message = message, 
                    Subject = tokenEmailDto.Subject 
                });

            if (!isSent)
                return new BadRequestResponse(ResponseMessages.UnexpectedError);

            var tokenEntity = new Token 
                { 
                    UserId = tokenEmailDto.User.Id, 
                    Value = tokenEmailDto.Token, 
                    Type = tokenEmailDto.TokenType.ToString(), 
                    ExpiresAt = DateTime.Now.AddHours(1) 
                };
            await _repositoryManager.Token.CreateToken(tokenEntity);
            await _repositoryManager.SaveAsync();

            return new ApiOkResponse<bool>(true);
        }

        private string GetEmailTemplate(GetEmailTemplateDto emailTemplateDto)
        {
            var template = string.Empty;

            switch (emailTemplateDto.TemplateType)
            {
                case 0:
                    template = GetEmailTemplates.GetConfirmEmailTemplate(emailTemplateDto.Url, emailTemplateDto.FirstName);
                    break;
                case 1:
                    template = GetEmailTemplates.GetResetPasswordEmailTemplate(emailTemplateDto.Url);
                    break;
            }
            return template;
        }
        #endregion
    }
}
