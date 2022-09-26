using Entities.Response;

namespace SpringBoard.Presentation.Controllers.V1.Extensions
{
    public static class ApiBaseResponseExtensions
    {
        public static TResultType GetResult<TResultType>(this ApiBaseResponse apiBaseResponse) => 
            ((ApiOkResponse<TResultType>)apiBaseResponse).Result;
    }
}
