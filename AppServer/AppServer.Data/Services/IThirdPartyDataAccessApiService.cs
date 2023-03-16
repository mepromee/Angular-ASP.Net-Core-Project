using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Data.Services
{
    // The interface for the service that retrieves data from the third-party API
    public interface IThirdPartyDataAccessApiService<T> where T : class
    {
        Task<List<T>> GetDataAsync(string tag);
    }
}
