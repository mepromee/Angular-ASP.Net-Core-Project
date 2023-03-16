using Microsoft.AspNetCore.Http.Features;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppServer.Web.Middlewares
{
    public class ValidateGetPostsRequestMiddleware
    {
        private const string TargetUrl = "/api/posts";
        private const string TagQueryParamName = "tags";
        private const string SortByQueryParamName = "sortBy";
        private const string DirectionQueryParamName = "direction";

        private readonly RequestDelegate _next;

        public ValidateGetPostsRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Get && context.Request.Path.StartsWithSegments(TargetUrl, StringComparison.OrdinalIgnoreCase))
            {
                // Check if the 'tag' parameter is missing.
                var tag = context.Request.Query[TagQueryParamName];
                if (string.IsNullOrWhiteSpace(tag))
                {
                    await SetHttpErrorResponse(context, "tags parameter is required");
                    return;
                }

                var sortBy = context.Request.Query[SortByQueryParamName];
                // Check if the 'sortBy' parameter is missing or not valid.
                if (!string.IsNullOrEmpty(sortBy) && !IsValidSortByParam(sortBy))
                {
                    await SetHttpErrorResponse(context, "sortBy parameter is invalid");
                    return;
                }

                var direction = context.Request.Query[DirectionQueryParamName];
                // Check if the 'direction' parameter is missing or not valid.
                if (!string.IsNullOrEmpty(direction) && !IsValidDirectionParam(direction))
                {
                    await SetHttpErrorResponse(context, "direction parameter is invalid");
                    return;
                }
            }
            await _next(context);
        }

        // Check if the sortBy query param value is accepted or not.
        private bool IsValidSortByParam(string sortBy)
        {
            var acceptableFields = new[] { "id", "reads", "likes", "popularity" };
            return acceptableFields.Contains(sortBy.ToLower());
        }

        // Check if the direction query param value is accepted or not.
        private bool IsValidDirectionParam(string direction)
        {
            var acceptableFields = new[] { "desc", "asc" };
            return acceptableFields.Contains(direction.ToLower());
        }

        private async Task SetHttpErrorResponse(HttpContext context, string errorMessage)
        {
            var error = new { error = errorMessage };
            var errorJson = JsonSerializer.Serialize(error);

            // Set the content type of the response to JSON
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(errorJson);
        }
    }
}
