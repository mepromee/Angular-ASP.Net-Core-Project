using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppServer.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServer.Data.Models;
using AppServer.Data.Services;
using Moq;

namespace AppServer.Domain.Services.Tests
{
    [TestClass()]
    public class PostServiceTests
    {
        private Mock<IThirdPartyDataAccessApiService<Post>> _mockDataAccessApiService;
        private PostService _postService;
        private Post post1 = new Post
        {
            Id = 1,
            Author = "Author1",
            AuthorId = 101,
            Likes = 5,
            Popularity = 0.5,
            Reads = 10,
            Tags = new List<string> { "tag1" }
        };
        private Post post2 = new Post
        {
            Id = 2,
            Author = "Author2",
            AuthorId = 102,
            Likes = 10,
            Popularity = 0.8,
            Reads = 15,
            Tags = new List<string> { "tag1", "tag2" }
        };
        private Post post3 = new Post
        {
            Id = 3,
            Author = "Author3",
            AuthorId = 103,
            Likes = 8,
            Popularity = 0.6,
            Reads = 12,
            Tags = new List<string> { "tag2" }
        };

        [TestInitialize]
        public void Setup()
        {
            _mockDataAccessApiService = new Mock<IThirdPartyDataAccessApiService<Post>>();
            _postService = new PostService(_mockDataAccessApiService.Object);
        }

        [TestMethod]
        public async Task GetPostsAsync_ReturnsPostsSortedByIdAsc()
        {
            // Arrange
            var tags = new List<string> { "tag1", "tag2" };

            var postsFromDbRepository = new List<Post> { post1, post2, post3 };
            _mockDataAccessApiService.Setup(m => m.GetDataAsync("tag1")).ReturnsAsync(postsFromDbRepository);
            _mockDataAccessApiService.Setup(m => m.GetDataAsync("tag2")).ReturnsAsync(postsFromDbRepository);

            // Act
            var sortedPosts = await _postService.GetPostsAsync(tags, "id", "asc");

            // Assert
            Assert.AreEqual(sortedPosts.Count, 3);
            Assert.AreEqual(sortedPosts[0].Id, 1);
            Assert.AreEqual(sortedPosts[1].Id, 2);
            Assert.AreEqual(sortedPosts[2].Id, 3);
        }

        [TestMethod]
        public async Task GetPostsAsync_ReturnsPostsSortedByIdDesc()
        {
            // Arrange
            var tags = new List<string> { "tag1", "tag2" };

            var postsFromDbRepository = new List<Post> { post1, post2, post3 };
            _mockDataAccessApiService.Setup(m => m.GetDataAsync("tag1")).ReturnsAsync(postsFromDbRepository);
            _mockDataAccessApiService.Setup(m => m.GetDataAsync("tag2")).ReturnsAsync(postsFromDbRepository);

            // Act
            var sortedPosts = await _postService.GetPostsAsync(tags, "id", "desc");

            // Assert
            Assert.AreEqual(sortedPosts.Count, 3);
            Assert.AreEqual(sortedPosts[0].Id, 3);
            Assert.AreEqual(sortedPosts[1].Id, 2);
            Assert.AreEqual(sortedPosts[2].Id, 1);
        }

        [TestMethod]
        public async Task GetPostsAsync_ReturnsPostsSortedByReadsAsc()
        {
            // Arrange
            var tags = new List<string> { "tag1", "tag2" };

            var postsFromDbRepository = new List<Post> { post1, post2, post3 };
            _mockDataAccessApiService.Setup(m => m.GetDataAsync("tag1")).ReturnsAsync(postsFromDbRepository);
            _mockDataAccessApiService.Setup(m => m.GetDataAsync("tag2")).ReturnsAsync(postsFromDbRepository);

            // Act
            var sortedPosts = await _postService.GetPostsAsync(tags, "reads", "asc");

            // Assert
            Assert.AreEqual(sortedPosts.Count, 3);
            Assert.AreEqual(sortedPosts[0].Reads, 10);
            Assert.AreEqual(sortedPosts[1].Reads, 12);
            Assert.AreEqual(sortedPosts[2].Reads, 15);
        }

        [TestMethod]
        public async Task GetPostsAsync_ReturnsPostsSortedByReadsDesc()
        {
            // Arrange
            var tags = new List<string> { "tag1", "tag2" };

            var postsFromDbRepository = new List<Post> { post1, post2, post3 };
            _mockDataAccessApiService.Setup(m => m.GetDataAsync("tag1")).ReturnsAsync(postsFromDbRepository);
            _mockDataAccessApiService.Setup(m => m.GetDataAsync("tag2")).ReturnsAsync(postsFromDbRepository);

            // Act
            var sortedPosts = await _postService.GetPostsAsync(tags, "reads", "desc");

            // Assert
            Assert.AreEqual(sortedPosts.Count, 3);
            Assert.AreEqual(sortedPosts[0].Reads, 15);
            Assert.AreEqual(sortedPosts[1].Reads, 12);
            Assert.AreEqual(sortedPosts[2].Reads, 10);
        }

        [TestMethod]
        public async Task GetPostsAsync_ReturnsPostsSortedByLikesAsc()
        {
            // Arrange
            var tags = new List<string> { "tag1", "tag2" };

            var postsFromDbRepository = new List<Post> { post1, post2, post3 };
            _mockDataAccessApiService.Setup(m => m.GetDataAsync("tag1")).ReturnsAsync(postsFromDbRepository);
            _mockDataAccessApiService.Setup(m => m.GetDataAsync("tag2")).ReturnsAsync(postsFromDbRepository);

            // Act
            var sortedPosts = await _postService.GetPostsAsync(tags, "likes", "asc");

            // Assert
            Assert.AreEqual(sortedPosts.Count, 3);
            Assert.AreEqual(sortedPosts[0].Likes, 5);
            Assert.AreEqual(sortedPosts[1].Likes, 8);
            Assert.AreEqual(sortedPosts[2].Likes, 10);
        }

        [TestMethod]
        public async Task GetPostsAsync_ReturnsPostsSortedByLikesDesc()
        {
            // Arrange
            var tags = new List<string> { "tag1", "tag2" };

            var postsFromDbRepository = new List<Post> { post1, post2, post3 };
            _mockDataAccessApiService.Setup(m => m.GetDataAsync("tag1")).ReturnsAsync(postsFromDbRepository);
            _mockDataAccessApiService.Setup(m => m.GetDataAsync("tag2")).ReturnsAsync(postsFromDbRepository);

            // Act
            var sortedPosts = await _postService.GetPostsAsync(tags, "likes", "desc");

            // Assert
            Assert.AreEqual(sortedPosts.Count, 3);
            Assert.AreEqual(sortedPosts[0].Likes, 10);
            Assert.AreEqual(sortedPosts[1].Likes, 8);
            Assert.AreEqual(sortedPosts[2].Likes, 5);
        }

        [TestMethod]
        public async Task GetPostsAsync_ReturnsPostsSortedByPopularityAsc()
        {
            // Arrange
            var tags = new List<string> { "tag1", "tag2" };

            var postsFromDbRepository = new List<Post> { post1, post2, post3 };
            _mockDataAccessApiService.Setup(m => m.GetDataAsync("tag1")).ReturnsAsync(postsFromDbRepository);
            _mockDataAccessApiService.Setup(m => m.GetDataAsync("tag2")).ReturnsAsync(postsFromDbRepository);

            // Act
            var sortedPosts = await _postService.GetPostsAsync(tags, "popularity", "asc");

            // Assert
            Assert.AreEqual(sortedPosts.Count, 3);
            Assert.AreEqual(sortedPosts[0].Popularity, 0.5);
            Assert.AreEqual(sortedPosts[1].Popularity, 0.6);
            Assert.AreEqual(sortedPosts[2].Popularity, 0.8);
        }

        [TestMethod]
        public async Task GetPostsAsync_ReturnsPostsSortedByPopularityDesc()
        {
            // Arrange
            var tags = new List<string> { "tag1", "tag2" };

            var postsFromDbRepository = new List<Post> { post1, post2, post3 };
            _mockDataAccessApiService.Setup(m => m.GetDataAsync("tag1")).ReturnsAsync(postsFromDbRepository);
            _mockDataAccessApiService.Setup(m => m.GetDataAsync("tag2")).ReturnsAsync(postsFromDbRepository);

            // Act
            var sortedPosts = await _postService.GetPostsAsync(tags, "popularity", "desc");

            // Assert
            Assert.AreEqual(sortedPosts.Count, 3);
            Assert.AreEqual(sortedPosts[0].Popularity, 0.8);
            Assert.AreEqual(sortedPosts[1].Popularity, 0.6);
            Assert.AreEqual(sortedPosts[2].Popularity, 0.5);
        }
    }

        
}