using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Data.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public int Likes { get; set; }
        public double Popularity { get; set; }
        public int Reads { get; set; }
        public List<string> Tags { get; set; }
    }

    public class PostsCollection
    {
        public List<Post> Posts { get; set; }
    }
}
