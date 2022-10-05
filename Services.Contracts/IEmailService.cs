using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendMailAsync(EmailRequestParameters requestParameters);
        Task<bool> SendMailAsync(ApplicationRequestParameters requestParameters);
    }
}
