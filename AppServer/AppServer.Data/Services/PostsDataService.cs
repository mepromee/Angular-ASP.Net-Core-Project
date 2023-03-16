using AppServer.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppServer.Data.Services
{
    public class PostsDataService: IThirdPartyDataAccessApiService<Post>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PostsDataService> _logger;

        public PostsDataService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<PostsDataService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<Post>> GetDataAsync(string tag)
        {
            try
            {
                var dbGetPostsApiUrl = GetPostsApiUrl(tag);
                var httpClient = _httpClientFactory.CreateClient();
                var apiResponse = await httpClient.GetAsync(dbGetPostsApiUrl);
                // Check if the response is successful
                apiResponse.EnsureSuccessStatusCode();

                var responseBodyString = await apiResponse.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var postsCollection = JsonSerializer.Deserialize<PostsCollection>(responseBodyString, options);
                return postsCollection.Posts;
            }
            catch (HttpRequestException ex)
            {
                // Log the error message
                _logger.LogError(ex, "Error occurred while getting data for tag {Tag}: {Message}", tag, ex.Message);
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error deserializing JSON response for tag {Tag}: {Message}", tag, ex.Message);
                return new List<Post>();
            }
        }

        // Reads base url from appsettings.json and constructs full url of get posts.
        private string GetPostsApiUrl(string tag)
        {
            var dbApiBaseUrl = _configuration.GetSection("ThirdPatryApis:ApiAssessmentDbApiBaseUrl").Value;
            var getDbPostsApiUrl = $"{dbApiBaseUrl}/a74fsg46d/posts?tag={tag}";

            return getDbPostsApiUrl;
        }
    }
}
