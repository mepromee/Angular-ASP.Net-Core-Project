using AppServer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Domain.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetPostsAsync(List<string> tag, string sortBy, string direction);
    }
}
