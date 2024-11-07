using BookStoreNetReact.Application.Helpers;
using System.Text.Json;

namespace BookStoreNetReact.Api.Extensions
{
    public static class HttpExtension
    {
        public static void AddPaginationHeader(this HttpResponse response, Pagination pagination)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            response.Headers.Append("Pagination", JsonSerializer.Serialize(pagination, options));
            response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
