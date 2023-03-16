using AppServer.Data.Models;
using AppServer.Data.Services;

namespace AppServer.Domain.Services
{
    public class PostService: IPostService
    {
        private readonly IThirdPartyDataAccessApiService<Post> _postDataAccessService;
        public PostService(IThirdPartyDataAccessApiService<Post> postDataAccessService) 
        {
            _postDataAccessService = postDataAccessService;
        }

        public async Task<List<Post>> GetPostsAsync(List<string> tags, string sortBy, string direction)
        {
            // Create a list of tasks to get posts for each tag parallely
            var tasks = tags.Distinct().Select(tag => _postDataAccessService.GetDataAsync(tag)).ToList();

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);

            // Create a dictionary to store the unique posts
            var postDictionary = new Dictionary<int, Post>();

            // Add each post to the dictionary, using the post ID as the key
            foreach (var task in tasks)
            {
                var posts = await task;
                foreach (var post in posts)
                {
                    if (!postDictionary.ContainsKey(post.Id))
                    {
                        postDictionary.Add(post.Id, post);
                    }
                }
            }

            var sortedPosts = SortPosts(postDictionary.Values.ToList(), sortBy, direction);

            return sortedPosts;
        }

        // Sorts posts according to the sortBy and direction parameter
        private List<Post> SortPosts(List<Post> posts, string sortBy, string direction)
        {
            switch (sortBy.ToLower())
            {
                case "id":
                    if (direction.ToLower() == "asc")
                    {
                        return posts.OrderBy(p => p.Id).ToList();
                    }
                    else
                    {
                        return posts.OrderByDescending(p => p.Id).ToList();
                    }
                    break;
                case "reads":
                    if (direction.ToLower() == "asc")
                    {
                        return posts.OrderBy(p => p.Reads).ToList();
                    }
                    else
                    {
                        return posts.OrderByDescending(p => p.Reads).ToList();
                    }
                    break;
                case "likes":
                    if (direction.ToLower() == "asc")
                    {
                        return posts.OrderBy(p => p.Likes).ToList();
                    }
                    else
                    {
                        return posts.OrderByDescending(p => p.Likes).ToList();
                    }
                    break;
                case "popularity":
                    if (direction.ToLower() == "asc")
                    {
                        return posts.OrderBy(p => p.Popularity).ToList();
                    }
                    else
                    {
                        return posts.OrderByDescending(p => p.Popularity).ToList();
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid sortBy parameter");
            }
        }

    }
}
