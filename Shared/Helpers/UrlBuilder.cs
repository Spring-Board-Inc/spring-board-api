using Microsoft.Extensions.Primitives;
using Shared.RequestFeatures;
using System.Web;

namespace Shared.Helpers
{
    public class UrlBuilder
    {
        public static string Builder(UrlBuilderParameters param, StringValues origin)
        {
            var uriBuilder = new UriBuilder(origin);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["Token"] = param.Token;
            query["Type"] = param.TokenType;
            query["UserId"] = param.UserId;
            uriBuilder.Query = query.ToString();

            return uriBuilder.ToString();
        }
    }
}
