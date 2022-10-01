using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services.Contracts
{
    public interface IEmailService
    {
        Task SendMailAsync(EmailRequestParameters requestParameters);
        Task SendMailAsync(ApplicationRequestParameters requestParameters);
    }
}
