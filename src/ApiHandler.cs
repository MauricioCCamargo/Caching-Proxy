using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;


namespace src
{
    public class ApiHandler
    {
        public void CreateHandler(WebApplication app, string baseUrl)
        {
            app.MapGet("{*path}", async ctx =>
            {
                var requestUrl = baseUrl + ctx.Request.Path;

                var cache = new Cache();
                var cacheData = cache.GetCache(requestUrl);
                if (cacheData == null)
                {
                    dto.ResponseDto response = new Request().GetRequest(requestUrl);
                    cacheData = cache.SetCache(response.Data, requestUrl, response.ContentType); ;
                }

                ctx.Response.Headers.Append("X-Cache", cacheData.Cache);
                var result = Results.Content(cacheData.Data, cacheData.ContentType, System.Text.Encoding.UTF8, 200);
                await result.ExecuteAsync(ctx);
            });
        }
    }
}